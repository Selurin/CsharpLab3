using System;
using System.IO;
using System.Linq;

class BinaryFileProcessor
{
    private const string FilePath = "data.bin";
    private const int NumbersCount = 10;

    public static void GenerateRandomFile(string filePath, int count)
    {
        try
        {
            Random random = new Random();
            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                for (int i = 0; i < count; i++)
                {
                    writer.Write(random.Next(-100, 101));
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

    public static void MainProcess()
    {
        try
        {
            Console.WriteLine("Генерация случайных данных...");
            GenerateRandomFile(FilePath, NumbersCount);

            Console.WriteLine("Вычисление разности максимального и минимального элементов...");
            double difference = CalculateDifference(FilePath);

            Console.WriteLine($"Разность: {difference}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
        finally
        {
            if (File.Exists(FilePath))
            {
                try
                {
                    File.Delete(FilePath);
                    Console.WriteLine("Временный файл удалён.");
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Не удалось удалить файл: {ex.Message}");
                }
            }
        }
    }
}