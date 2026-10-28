using System;

internal class Arr
{
    public int[,] Matrix;

    public Arr(int n, int m)
    {
        Matrix = new int[n, m];
        Console.WriteLine($"Введите {n * m} чисел:");

        for (int j = 0; j < m; j++)
        {
            for (int i = n - 1; i >= 0; i--)
            {
                Matrix[i, j] = int.Parse(Console.ReadLine());
            }
        }
        Print();
    }

    public Arr(int n)
    {
        Matrix = new int[n, n];
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
                Matrix[i, j] = d1 * 1000 + d2 * 100 + d3 * 10 + d4;
            }
        }
        Print();
    }

    public Arr(int n, bool pattern)
    {
        Matrix = new int[n, n];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i + j < n - 1)
                {
                    Matrix[i, j] = 0;
                }
                else
                {
                    int layer = (i + j) - (n - 1);

                    if (layer % 2 == 0)
                        Matrix[i, j] = n - j;
                    else
                        Matrix[i, j] = n - i;
                }
            }
        }
        Print();
    }

    private void Print()
    {
        int rows = Matrix.GetLength(0);
        int cols = Matrix.GetLength(1);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(Matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }
}