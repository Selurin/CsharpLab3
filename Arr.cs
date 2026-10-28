using System;

internal class Arr
{
    private int[,] _matrix;

    public Arr()
    {

    }

    public Arr(int[,] matrix)
    {
        _matrix = matrix;
    }

    public Arr(int n, int m)
    {
        _matrix = new int[n, m];
        Console.WriteLine($"Введите {n * m} чисел (или 'exit' для досрочного выхода):");
        bool isExit = false;

        for (int j = 0; j < m; j++)
        {
            for (int i = n - 1; i >= 0; i--)
            {
                string? input = Console.ReadLine()?.Trim().ToLower();

                if (input == "exit" || input == "выход")
                {
                    Console.WriteLine("Ввод прерван пользователем.");
                    isExit = true;
                    break;
                }

                if (int.TryParse(input, out int val))
                {
                    _matrix[i, j] = val;
                }
                else
                {
                    Console.WriteLine($"Неверный ввод '{input}'. Ячейка [{i},{j}] осталась пустой (0).");
                }
            }
            if (isExit) break;
        }
        Print();
    }

    public Arr(int n)
    {
        _matrix = new int[n, n];
        int[] oddDigits = { 1, 3, 5, 7, 9 };
        Random rand = new Random();

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                int d1 = oddDigits[rand.Next(oddDigits.Length)];
                int d2 = oddDigits[rand.Next(oddDigits.Length)];
                int d3 = oddDigits[rand.Next(oddDigits.Length)];
                int d4 = oddDigits[rand.Next(oddDigits.Length)];
                _matrix[i, j] = d1 * 1000 + d2 * 100 + d3 * 10 + d4;
            }
        }
        Print();
    }

    public Arr(int n, string pattern)
    {
        _matrix = new int[n, n];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i + j < n - 1)
                {
                    _matrix[i, j] = 0;
                }
                else
                {
                    int layer = (i + j) - (n - 1);

                    if (layer % 2 == 0)
                        _matrix[i, j] = n - j;
                    else
                        _matrix[i, j] = n - i;
                }
            }
        }
        Print();
    }

    public void CalculateAndCount()
    {
        if (_matrix == null)
        {
            Console.WriteLine("Матрица пуста! Сначала создайте массив.");
            return;
        }

        int rows = _matrix.GetLength(0);
        int cols = _matrix.GetLength(1);

        double sumFirstCol = 0;
        for (int i = 0; i < rows; i++)
        {
            sumFirstCol += _matrix[i, 0];
        }
        double avgFirstCol = sumFirstCol / rows;

        Console.WriteLine($"\nСреднее арифметическое 1-го столбца: {avgFirstCol:F2}");

        for (int j = 1; j < cols; j++)
        {
            int count = 0;
            double prevAvg = 0;
            double prevSum = 0;

            for (int i = 0; i < rows; i++)
            {
                prevSum += _matrix[i, j - 1];
            }
            prevAvg = prevSum / rows;

            for (int i = 0; i < rows; i++)
            {
                if (_matrix[i, j] > prevAvg)
                {
                    count++;
                }
            }

            Console.WriteLine($"Столбец {j + 1}: {count} эл. > среднего столбца {j} ({prevAvg:F2})");
        }
    }

    private void Print()
    {
        if (_matrix == null)
        {
            Console.WriteLine("Матрица пуста!");
            return;
        }

        int rows = _matrix.GetLength(0);
        int cols = _matrix.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write($"{_matrix[i, j],-6}");
            }
            Console.WriteLine();
        }
    }
}