internal class Program
{
    static void Main(string[] args)
    {
        string choice;
        do
        {
            PrintMenu();
            Console.Write("Введите номер задачи (например 1) или 0 для выхода: ");
            choice = Console.ReadLine()?.Trim() ?? "";
            switch (choice)
            {
                case "0":
                    Console.WriteLine("До свидания!");
                    break;

                case "1":
                    Console.WriteLine("Выберите нужный массив. Для выхода введите 0");
                    Console.WriteLine("1 - Массив (n × m): Заполняется вручную с клавиатуры по столбцам снизу вверх");
                    Console.WriteLine("2 - Массив 2 (n × n): Заполняется случайными четырёхзначными числами, состоящими только из нечётных цифр (1, 3, 5, 7, 9)");
                    Console.WriteLine("3 - Массив 3 (n × n): Заполнение по диагоналям");

                    string? subchoice;
                    do
                    {
                        Console.Write("\nВведите номер подзадачи или 0 для возврата: ");
                        subchoice = Console.ReadLine()?.Trim() ?? "";

                        if (subchoice == "1")
                        {
                            if (TryReadInt("Введите число строк: ", out int n))
                            {
                                if (TryReadInt("Введите число столбцов: ", out int m))
                                {
                                    Arr arr = new Arr(n, m);
                                }
                            }
                        }
                        else if (subchoice == "2")
                        {
                            if (TryReadInt("Введите размерность квадратной матрицы: ", out int n))
                            {
                                Arr arr = new Arr(n);
                            }
                        }

                        else if (subchoice == "3")
                        {
                            if (TryReadInt("Введите размерность квадратной матрицы: ", out int n))
                            {
                                Arr arr = new Arr(n, "pattern");
                            }
                        }

                        else if (subchoice != "0")
                        {
                            Console.WriteLine("Неверный выбор! Введите корректный номер массива");
                        }
                    } while (subchoice != "0");
                    break;

                case "2":
                    Arr arr2 = new Arr(5);
                    arr2.CalculateAndCount();
                    break;

                case "4":
                    FileProcessor.TaskData();
                    break;

                case "5":
                    FileProcessor.TaskToys();
                    break;

                default:
                    Console.WriteLine("Неверный выбор! Введите корректный номер задачи");
                    break;

            }
        } while (choice != "0");
    }

    private static void PrintMenu()
    {
        Console.WriteLine("\n--- Меню задач ---");
        Console.WriteLine("1 - Заполнение двумерных массивов");
        Console.WriteLine("2 - Работа с двумерными массивами");
        Console.WriteLine("4 - Бинарные файлы");
        Console.WriteLine("5 - Бинарные файлы и структуры");
        //Console.WriteLine("");
        Console.WriteLine("0 - Выход");
        Console.WriteLine("------------------");
    }

    private static bool TryReadInt(string prompt, out int value)
    {
        Console.Write(prompt);
        if (int.TryParse(Console.ReadLine(), out value))
        {
            return true;
        }
        else
        {
            Console.WriteLine("Неверное значение! Ожидалось целое число.");
            return false;
        }
    }
}