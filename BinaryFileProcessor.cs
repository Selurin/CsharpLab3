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

    public static void GenerateRandomFile(string filePath, int count)
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

    public static double CalculateDifference(string filePath)
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

    public static void GenerateToysFile(string filePath, int count)
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

    public static void FindExpensiveToys(string filePath, double k)
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
}