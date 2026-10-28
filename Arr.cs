using System;

internal class Arr
{
    private int[,] _matrix;

    public Arr()
    {

    }

    public void Arr1(int n, int m)
    {
        _matrix = new int[n, m];
        Console.WriteLine($"Введите {n * m} чисел:");

        for (int j = 0; j < m; j++)
        {
            for (int i = n - 1; i >= 0; i--)
            {
                _matrix[i, j] = int.Parse(Console.ReadLine());
            }
        }
        Print();
    }

    public void Arr2(int n)
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

    public void Arr3(int n)
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

    private void Print()
    {
        int rows = _matrix.GetLength(0);
        int cols = _matrix.GetLength(1);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(_matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }
}