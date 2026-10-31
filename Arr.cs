using System;
using System.Text;

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
        ToString();
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
        ToString();
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
        ToString();
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

    public Arr Transpose()
    {
        int rows = _matrix.GetLength(0);
        int cols = _matrix.GetLength(1);
        int[,] transposed = new int[cols, rows];

        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                transposed[j, i] = _matrix[i, j];

        Arr result = new Arr();
        result._matrix = transposed;
        return result;
    }

    public Arr Subtract(Arr other)
    {
        int r1 = _matrix.GetLength(0), c1 = _matrix.GetLength(1);
        int r2 = other._matrix.GetLength(0), c2 = other._matrix.GetLength(1);

        if (r1 != r2 || c1 != c2)
        {
            Console.WriteLine("Ошибка: размеры матриц для вычитания не совпадают!");
            return new Arr();
        }

        int[,] res = new int[r1, c1];
        for (int i = 0; i < r1; i++)
            for (int j = 0; j < c1; j++)
                res[i, j] = _matrix[i, j] - other._matrix[i, j];

        Arr result = new Arr();
        result._matrix = res;
        return result;
    }

    public Arr MultiplyByScalar(int scalar)
    {
        int rows = _matrix.GetLength(0);
        int cols = _matrix.GetLength(1);
        int[,] res = new int[rows, cols];

        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                res[i, j] = _matrix[i, j] * scalar;

        Arr result = new Arr();
        result._matrix = res;
        return result;
    }

 
    public Arr Multiply(Arr other)
    {
        int r1 = _matrix.GetLength(0), c1 = _matrix.GetLength(1);
        int r2 = other._matrix.GetLength(0), c2 = other._matrix.GetLength(1);

        if (c1 != r2)
        {
            Console.WriteLine("Ошибка: нельзя умножить (столбцы первой != строки второй)");
            return new Arr();
        }

        int[,] res = new int[r1, c2];
        for (int i = 0; i < r1; i++)
            for (int j = 0; j < c2; j++)
                for (int k = 0; k < c1; k++)
                    res[i, j] += _matrix[i, k] * other._matrix[k, j];

        Arr result = new Arr();
        result._matrix = res;
        return result;
    }

    public static Arr CalculateExpression(Arr A, Arr B, Arr C)
    {
        Arr BT = B.Transpose();
        Console.WriteLine("B транспонированная:");
        Console.WriteLine(BT.ToString());

        Arr diff = BT.Subtract(C);
        Console.WriteLine("(B^T - C):");
        Console.WriteLine(diff.ToString());

        Arr multyA = A.Multiply(diff);
        Console.WriteLine("A * (B^T - C):");
        Console.WriteLine(multyA.ToString());

        Arr result = multyA.MultiplyByScalar(7);
        Console.WriteLine("Итоговый результат (7 * ...):");
        Console.WriteLine(result.ToString());

        return result;
    }


    public override string ToString()
    {
        if (_matrix == null) return "Матрица пуста";

        StringBuilder sb = new StringBuilder();
        int rows = _matrix.GetLength(0);
        int cols = _matrix.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                sb.Append($"{_matrix[i, j],-6}");
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }
}