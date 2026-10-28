namespace CsharpLab3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Задание 1 (n x m):");
            Arr arr1 = new Arr();

            Console.WriteLine("\nЗадание 2 (n x n):");
            Arr arr2 = new Arr(4);

            Console.WriteLine("\n=== Задание 3 ===");
            Arr arr3 = new Arr(5, true); // Паттерн как на фото
        }
    }
}