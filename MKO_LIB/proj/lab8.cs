using System.Text;

namespace MKO_LIB
{
    public class Lab8
    {
        public static string Run()
        {
            var output = new StringBuilder();
            output.Append("""
                === Лабораторна робота 8 ===
                === Побудова сплайнів третього порядку ===
                """);

            try
            {
                // Табличні дані
                double[] X = { 3, 6, 9, 12 };
                double[] Y = { 12, 16, 10, 21 };
                double[] testPoints = { 0.5, 1.5, 2.5 }; // Точки для знаходження наближеного значення функції

                output.AppendLine("Вхідні дані (таблиця):");
                
                output.AppendLine(string.Join(" ", X.Select(x => $"{x,5}")));
                output.AppendLine(string.Join(" ", Y.Select(y => $"{y,5}")));
                output.Append('\n');

                foreach (var xTarget in testPoints) // Обчислення кубічним сплайном
                {
                    double splineResult = CubicSplineInterpolationMethod.Calculate(X, Y, xTarget);
                    output.AppendLine($"""
                        Шукана точка X* = {xTarget}
                        Наближене значення Y*: {splineResult:F4}
                        {new string('-', 40)}
                        """);
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