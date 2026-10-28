using System;
using System.Xml.Serialization;


[Serializable]
public struct Toy
{
    public string Name;
    public double Price;
    public int AgeFrom;
    public int AgeTo;

    public Toy(string name, double price, int ageFrom, int ageTo)
    {
        Name = name;
        Price = price;
        AgeFrom = ageFrom;
        AgeTo = ageTo;
    }
}

class FileProcessor
{
    private const string DataFilePath = "data.bin";
    private const string ToysFilePath = "toys.xml.bin";
    private const string TextFilePath = "numbers.txt";
    private const string TextNumbersFilePath = "numbers_multi.txt";
    private const string SourceTextFilePath = "source_text.txt";
    private const string ResultTextFilePath = "filtered_text.txt";

    public static void TaskData()
    {
        try
        {
            GenerateRandomFile(DataFilePath, 10);
            double diff = CalculateDifference(DataFilePath);
            Console.WriteLine($"Результат: {diff}");
            Console.WriteLine($"Файл сохранен: {DataFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка выполнения задачи: {ex.Message}");
        }
    }

    private static void GenerateRandomFile(string filePath, int count)
    {
        try
        {
            Random random = new Random();
            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                for (int i = 0; i < count; i++)
                {
                    writer.Write((double)random.Next(-100, 101));
                }
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Ошибка при создании файла: {ex.Message}");
            throw;
        }
    }

    private static double CalculateDifference(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Файл не найден", filePath);
            }

            double[] numbers;
            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                int count = (int)(reader.BaseStream.Length / sizeof(double));
                numbers = new double[count];

                for (int i = 0; i < count; i++)
                {
                    numbers[i] = reader.ReadDouble();
                }
            }

            if (numbers.Length == 0)
            {
                throw new InvalidOperationException("Файл пустой");
            }

            double max = numbers.Max();
            double min = numbers.Min();
            return max - min;
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
            throw;
        }
    }

    public static void TaskToys()
    {
        try
        {
            double k = 500;
            GenerateToysFile(ToysFilePath, 5);
            FindExpensiveToys(ToysFilePath, k);
            Console.WriteLine($"Файл сохранен: {ToysFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка выполнения задачи: {ex.Message}");
        }
    }

    private static void GenerateToysFile(string filePath, int count)
    {
        try
        {
            var toys = new List<Toy>();
            Random rand = new Random();
            string[] names = { "Медведь", "Кукла", "Робот", "Машинка", "Конструктор" };

            for (int i = 0; i < count; i++)
                toys.Add(new Toy(
                    $"{names[i % names.Length]} #{i + 1}",
                    Math.Round(rand.NextDouble() * 5000 + 100, 2),
                    rand.Next(0, 8),
                    rand.Next(1, 6) + rand.Next(0, 8)
                ));

            using var fs = new FileStream(filePath, FileMode.Create);
            new XmlSerializer(typeof(List<Toy>)).Serialize(fs, toys);
        }
        catch (IOException ex) { Console.WriteLine($"Ошибка записи игрушек: {ex.Message}"); throw; }
    }

    private static void FindExpensiveToys(string filePath, double k)
    {
        try
        {
            if (!File.Exists(filePath)) throw new FileNotFoundException("Файл игрушек не найден", filePath);

            List<Toy> toys;
            using (var fs = new FileStream(filePath, FileMode.Open))
                toys = (List<Toy>)new XmlSerializer(typeof(List<Toy>)).Deserialize(fs);

            if (toys?.Count == 0) throw new InvalidOperationException("Список пуст");

            double max = toys.Max(t => t.Price);
            Console.WriteLine($"\nМакс. цена: {max:F2} руб. (допуск ±{k:F2})");

            bool found = false;
            foreach (var t in toys.Where(t => max - t.Price <= k))
            {
                Console.WriteLine($"{t.Name} — {t.Price:F2} руб.");
                found = true;
            }
            if (!found) Console.WriteLine("  Нет подходящих игрушек.");
        }
        catch (IOException ex) { Console.WriteLine($"Ошибка чтения игрушек: {ex.Message}"); throw; }
    }

    public static void TaskTextFileMaxCount()
    {
        try
        {
            GenerateRandomTextFile(TextFilePath, 20);

            var result = CountMaxOccurrences(TextFilePath);

            Console.WriteLine($"Максимальный элемент: {result.Max}");
            Console.WriteLine($"Количество вхождений: {result.Count}");
            Console.WriteLine($"Файл сохранен: {TextFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка выполнения задачи: {ex.Message}");
        }
    }

    private static void GenerateRandomTextFile(string filePath, int count)
    {
        try
        {
            Random rand = new Random();
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < count; i++)
                {
                    writer.WriteLine(rand.Next(-50, 51));
                }
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Ошибка записи текстового файла: {ex.Message}");
            throw;
        }
    }

    private static (int Max, int Count) CountMaxOccurrences(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Текстовый файл не найден", filePath);

            List<int> numbers = new List<int>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (string.IsNullOrEmpty(line)) continue;

                    if (int.TryParse(line, out int num))
                    {
                        numbers.Add(num);
                    }
                    else
                    {
                        Console.WriteLine($"️ Пропущена некорректная строка: \"{line}\"");
                    }
                }
            }

            if (numbers.Count == 0)
                throw new InvalidOperationException("Файл не содержит корректных чисел");

            int max = numbers.Max();
            int count = numbers.Count(n => n == max);

            return (max, count);
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Ошибка чтения текстового файла: {ex.Message}");
            throw;
        }
    }

    public static void TaskTextFileEvenCount()
    {
        try
        {
            GenerateMultiNumberTextFile(TextNumbersFilePath, 5, 4);
            int evenCount = CountEvenNumbers(TextNumbersFilePath);

            Console.WriteLine($"Количество чётных элементов: {evenCount}");
            Console.WriteLine($"Файл сохранен: {TextNumbersFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка выполнения задачи: {ex.Message}");
        }
    }
    private static void GenerateMultiNumberTextFile(string filePath, int linesCount, int maxNumbersPerLine)
    {
        try
        {
            Random rand = new Random();
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < linesCount; i++)
                {
                    int numbersInLine = rand.Next(1, maxNumbersPerLine + 1);
                    var lineNumbers = new List<int>();

                    for (int j = 0; j < numbersInLine; j++)
                    {
                        lineNumbers.Add(rand.Next(-100, 101));
                    }

                    writer.WriteLine(string.Join(";", lineNumbers));
                }
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Ошибка записи файла: {ex.Message}");
            throw;
        }
    }

    private static int CountEvenNumbers(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Файл не найден", filePath);

            int evenCount = 0;

            using (StreamReader reader = new StreamReader(filePath))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (string.IsNullOrEmpty(line)) continue;

                    string[] parts = line.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string part in parts)
                    {
                        if (int.TryParse(part.Trim(), out int num))
                        {
                            if (num % 2 == 0)
                                evenCount++;
                        }
                        else
                        {
                            Console.WriteLine($"️ Пропущено некорректное значение: \"{part}\"");
                        }
                    }
                }
            }

            return evenCount;
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Ошибка чтения файла: {ex.Message}");
            throw;
        }
    }

    public static void TaskFilterLinesBySubstring()
    {
        try
        {
            GenerateSourceTextFile(SourceTextFilePath);

            Console.Write("Введите искомую комбинацию символов: ");
            string? substring = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(substring))
            {
                Console.WriteLine("Ошибка: Искомая подстрока не может быть пустой.");
                return;
            }

            int linesCopied = FilterAndCopyLines(SourceTextFilePath, ResultTextFilePath, substring);

            Console.WriteLine($"\nРезультат записан в: {ResultTextFilePath}");
            Console.WriteLine($"Найдено и переписано строк: {linesCopied}");
            Console.WriteLine($"Исходный файл сохранен: {SourceTextFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка выполнения задачи: {ex.Message}");
        }
    }

    private static void GenerateSourceTextFile(string filePath)
    {
        try
        {
            Random rand = new Random();

            string[] beginnings = { "Сегодня ", "Вчера ", "На уроке ", "Студенты ", "Программисты " };
            string[] middles = {
            "изучали информатику и выполняли задания по формам данных, ",
            "работали с деформацией массивов и форматированием вывода, ",
            "решали задачи про метаморфозы в алгоритмах, ",
            "анализировали информационные потоки без особых формальностей, ",
            "просто писали код без всяких дополнительных условий "
        };
            string[] endings = { "и получили хороший результат.", "это было непросто.", "все справились!", "" };

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                int linesCount = rand.Next(8, 13);

                for (int i = 0; i < linesCount; i++)
                {
                    string line = $"{beginnings[rand.Next(beginnings.Length)]}" +
                                  $"{middles[rand.Next(middles.Length)]}" +
                                  $"{endings[rand.Next(endings.Length)]}";

                    writer.WriteLine(line.Trim());
                }
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Ошибка создания файла: {ex.Message}");
            throw;
        }
    }


    private static int FilterAndCopyLines(string sourcePath, string resultPath, string substring)
    {
        try
        {
            if (!File.Exists(sourcePath))
                throw new FileNotFoundException("Исходный файл не найден", sourcePath);

            int count = 0;

            using (StreamReader reader = new StreamReader(sourcePath))
            using (StreamWriter writer = new StreamWriter(resultPath))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.IndexOf(substring, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        writer.WriteLine(line);
                        count++;
                    }
                }
            }

            return count;
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Ошибка обработки файлов: {ex.Message}");
            throw;
        }
    }
}