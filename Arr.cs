using System;

internal class Arr
{
    public Arr(int n)
    {
        int[,] matrix = new int[n, n];

        int[] oddDigits = { 1, 3, 5, 7, 9 };
        Random rand = new Random();

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                int digit1 = oddDigits[rand.Next(oddDigits.Length)];
                int digit2 = oddDigits[rand.Next(oddDigits.Length)];
                int digit3 = oddDigits[rand.Next(oddDigits.Length)];
                int digit4 = oddDigits[rand.Next(oddDigits.Length)];

                matrix[i, j] = digit1 * 1000 + digit2 * 100 + digit3 * 10 + digit4;
            }
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }
}
