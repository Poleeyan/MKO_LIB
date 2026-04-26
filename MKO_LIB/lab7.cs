using System;
using System.Text;

namespace MKO_LIB
{
    public class Lab7
    {
        public static string Run()
        {
            var output = new StringBuilder();

            output.AppendLine("=== Лабораторна робота 7 ===");
            output.AppendLine("=== Задачі інтерполяції. Метод Лагранжа ===\n");

            try
            {
                // Табличні дані
                double[] X = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                double[] Y = { -1, 2, 17, 50, 107, 194, 317, 482, 695, 962, 1289 };

                output.AppendLine("Вхідні дані (таблиця):");
                for (int i = 0; i < X.Length; i++)
                {
                    output.Append($"{X[i],4} ");
                }
                output.AppendLine();
                for (int i = 0; i < Y.Length; i++)
                {
                    output.Append($"{Y[i],4} ");
                }
                output.AppendLine("\n");

                // Контрольні точки для перевірки (визначає викладач)
                // Оберемо кілька точок між вузлами
                double[] testPoints = { 0.5, 2.5, 5.5, 9.2 };

                foreach (var xTarget in testPoints)
                {
                    // Обчислення методом Лагранжа
                    double lagrangeResult = LagrangeInterpolationMethod.Calculate(X, Y, xTarget);

                    // Справжнє значення функції y = x^3 + 3x^2 - x - 1
                    double exactValue = Math.Pow(xTarget, 3) + 3 * Math.Pow(xTarget, 2) - xTarget - 1;

                    output.AppendLine($"Шукана точка X* = {xTarget}");
                    output.AppendLine($"Метод Лагранжа Y*: {lagrangeResult:F4}");
                    output.AppendLine($"Точне значення Y*:   {exactValue:F4}");
                    output.AppendLine($"Похибка:             {Math.Abs(lagrangeResult - exactValue):E2}");
                    output.AppendLine(new string('-', 40));
                }
            }
            catch (Exception ex)
            {
                output.AppendLine($"\nПомилка виконання: {ex.Message}");
            }

            return output.ToString();
        }
    }
}
