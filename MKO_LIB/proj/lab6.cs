using System.Text;

namespace MKO_LIB
{
    public class Lab6
    {
        public static string Run()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=== Лабораторна робота 6: Розв'язання системи нелінійних рівнянь методом Ньютона ===");
            sb.AppendLine("Система:");
            sb.AppendLine("1) x1^2 + x2^2 + 2x3^2 = 4");
            sb.AppendLine("2) x1*x2 + x2*x3 + x1*x3 = 3");
            sb.AppendLine("3) 2x1*x2^2 + x2*x3^3 + 3x1^4 = 6");
            sb.AppendLine();
            sb.AppendLine("Початкове наближення: x10 = 0.5, x20 = 0.5, x30 = 0.5");
            sb.AppendLine("Похибка: epsilon = 1e-5");
            sb.AppendLine();

            Func<double[], double[]> systemFunctions = (x) => new double[3]
            {
                x[0] * x[0] + x[1] * x[1] + 2 * x[2] * x[2] - 4,
                x[0] * x[1] + x[1] * x[2] + x[0] * x[2] - 3,
                2 * x[0] * x[1] * x[1] + x[1] * Math.Pow(x[2], 3) + 3 * Math.Pow(x[0], 4) - 6
            };

            Func<double[], double[,]> jacobianMatrix = (x) => new double[3, 3]
            {
                { 2 * x[0], 2 * x[1], 4 * x[2] },
                { x[1] + x[2], x[0] + x[2], x[1] + x[0] },
                { 2 * x[1] * x[1] + 12 * Math.Pow(x[0], 3), 4 * x[0] * x[1] + Math.Pow(x[2], 3), 3 * x[1] * x[2] * x[2] }
            };

            NewtonSystemMethod newtonMethod = new NewtonSystemMethod(systemFunctions, jacobianMatrix);

            double[] x0 = new double[] { 0.5, 0.5, 0.5 };
            double epsilon = 1e-5;

            try
            {
                var result = newtonMethod.Solve(x0, epsilon);

                sb.AppendLine("Результат розв'язання:");
                for (int i = 0; i < result.Root.Length; i++)
                {
                    sb.AppendLine($"x{i + 1} = {result.Root[i]:F6}");
                }
                sb.AppendLine($"Кількість ітерацій: {result.Iterations}");
            }
            catch (Exception ex)
            {
                sb.AppendLine($"Помилка при розв'язанні: {ex.Message}");
            }

            return sb.ToString();
        }
    }
}
