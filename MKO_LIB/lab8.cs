using System;
using System.Text;

namespace MKO_LIB
{
    public class Lab8
    {
        public static string Run()
        {
            var output = new StringBuilder();

            output.AppendLine("=== Лабораторна робота 8 ===");
            output.AppendLine("=== Побудова сплайнів третього порядку ===\n");

            try
            {
                // Табличні дані
                double[] X = { 3, 6, 9, 12 };
                double[] Y = { 12, 16, 10, 21 };

                output.AppendLine("Вхідні дані (таблиця):");
                for (int i = 0; i < X.Length; i++)
                {
                    output.Append($"{X[i],5} ");
                }
                output.AppendLine();
                for (int i = 0; i < Y.Length; i++)
                {
                    output.Append($"{Y[i],5} ");
                }
                output.AppendLine("\n");

                // Точки для знаходження наближеного значення функції
                double[] testPoints = { 0.5, 1.5, 2.5 };

                foreach (var xTarget in testPoints)
                {
                    // Обчислення кубічним сплайном
                    double splineResult = CubicSplineInterpolationMethod.Calculate(X, Y, xTarget);

                    output.AppendLine($"Шукана точка X* = {xTarget}");
                    output.AppendLine($"Наближене значення Y*: {splineResult:F4}");
                    output.AppendLine(new string('-', 40));
                }
            }
            catch (Exception ex)
            {
                output.AppendLine($"Помилка: {ex.Message}");
            }

            return output.ToString();
        }
    }
}