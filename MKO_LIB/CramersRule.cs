using System;

namespace MKO_LIB
{
    public class CramersRule
    {
        /// <summary>
        /// Розв'язує систему лінійних рівнянь за правилом Крамера для матриці N x N
        /// </summary>
        public static double[] Solve(double[,] A, double[] b, out double detMain, out double[] detI)
        {
            int n = b.Length;
            double[] x = new double[n];
            detI = new double[n];

            // Обчислюємо d = det(A)
            detMain = CalculateDeterminant(A);

            if (Math.Abs(detMain) < 1e-10)
            {
                throw new InvalidOperationException("Система не має єдиного рішення (визначник дорівнює нулю)");
            }

            for (int j = 0; j < n; j++)
            {
                // Створюємо копію матриці A із заміненим j-м стовпцем на вектор b
                double[,] A_i = new double[n, n];
                for (int r = 0; r < n; r++)
                {
                    for (int c = 0; c < n; c++)
                    {
                        A_i[r, c] = (c == j) ? b[r] : A[r, c];
                    }
                }

                // d[i] = det(A_i)
                detI[j] = CalculateDeterminant(A_i);

                // x_i = d_i / d
                x[j] = detI[j] / detMain;
            }

            return x;
        }

        public static double[] Solve(double[,] A, double[] b)
        {
            return Solve(A, b, out _, out _);
        }

        /// <summary>
        /// Універсальне обчислення визначника квадратної матриці розміром N x N (розклад за першим рядком)
        /// </summary>
        public static double CalculateDeterminant(double[,] matrix)
        {
            int n = matrix.GetLength(0);
            if (n != matrix.GetLength(1))
                throw new ArgumentException("Матриця повинна бути квадратною!");

            if (n == 1)
                return matrix[0, 0];
            if (n == 2)
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];

            double det = 0;
            for (int p = 0; p < n; p++)
            {
                double[,] subMat = new double[n - 1, n - 1];
                for (int i = 1; i < n; i++)
                {
                    int colCount = 0;
                    for (int j = 0; j < n; j++)
                    {
                        if (j == p) continue;
                        subMat[i - 1, colCount] = matrix[i, j];
                        colCount++;
                    }
                }

                double sign = (p % 2 == 0) ? 1.0 : -1.0;
                det += sign * matrix[0, p] * CalculateDeterminant(subMat);
            }
            return det;
        }

        /// <summary>
        /// Виводить вектор рішення
        /// </summary>
        public static void DisplaySolution(double[] x)
        {
            Console.WriteLine("Рішення системи:");
            for (int i = 0; i < x.Length; i++)
            {
                Console.WriteLine($"x[{i + 1}] = {x[i]:F6}");
            }
        }
    }
}
