#if false
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

            Func<double[], double[]> systemFunctions = Lab6Equations.SystemFunctions;
            Func<double[], double[,]> jacobianMatrix = Lab6Jacobian.Calculate;

            NewtonSystemMethod newtonMethod = new NewtonSystemMethod(systemFunctions, jacobianMatrix);

            double[] x0 = new double[] { 0.5, 0.5, 0.5 };
            double epsilon = 1e-5;

            try
            {
                var result = newtonMethod.Solve(x0, epsilon);

                sb.AppendLine("Ітерації методу Ньютона:");
                sb.AppendLine(string.Format("{0,-6} | {1,-12} | {2,-12} | {3,-12} | {4,-12} | {5,-12} | {6,-12} | {7,-12}",
                    "Ітер.", "x1", "x2", "x3", "f1(x)", "f2(x)", "f3(x)", "Похибка"));
                sb.AppendLine(new string('-', 101));

                foreach (var step in result.Steps)
                {
                    sb.AppendLine(string.Format("{0,-6} | {1,-12:F6} | {2,-12:F6} | {3,-12:F6} | {4,-12:F6} | {5,-12:F6} | {6,-12:F6} | {7,-12:E4}",
                        step.IterationNumber,
                        step.X[0], step.X[1], step.X[2],
                        step.F[0], step.F[1], step.F[2],
                        step.Error));
                }
                sb.AppendLine(new string('-', 101));
                sb.AppendLine();

                sb.AppendLine("Результат розв'язання (кінцевий результат):");
                for (int i = 0; i < result.Root.Length; i++)
                {
                    sb.AppendLine($"x{i + 1} = {result.Root[i]:F6}");
                }
                sb.AppendLine($"Кількість ітерацій: {result.Iterations}");
                sb.AppendLine();

                sb.AppendLine("Перевірка (підстановка розв'язку в рівняння системи):");
                double[] check = Lab6Equations.SystemFunctions(result.Root);
                sb.AppendLine($"f1(x*) = {check[0]:E6} (повинно бути ≈ 0)");
                sb.AppendLine($"f2(x*) = {check[1]:E6} (повинно бути ≈ 0)");
                sb.AppendLine($"f3(x*) = {check[2]:E6} (повинно бути ≈ 0)");
            }
            catch (Exception ex)
            {
                sb.AppendLine($"Помилка при розв'язанні: {ex.Message}");
            }

            return sb.ToString();
        }
    }

    public static class Lab6Equations
    {
        public static double[] SystemFunctions(double[] x)
        {
            return new double[3]
            {
                x[0] * x[0] + x[1] * x[1] + 2 * x[2] * x[2] - 4,
                x[0] * x[1] + x[1] * x[2] + x[0] * x[2] - 3,
                2 * x[0] * x[1] * x[1] + x[1] * Math.Pow(x[2], 3) + 3 * Math.Pow(x[0], 4) - 6
            };
        }
    }

    public static class Lab6Jacobian
    {
        public static double[,] Calculate(double[] x)
        {
            return new double[3, 3]
            {
                { 2 * x[0], 2 * x[1], 4 * x[2] },
                { x[1] + x[2], x[0] + x[2], x[1] + x[0] },
                { 2 * x[1] * x[1] + 12 * Math.Pow(x[0], 3), 4 * x[0] * x[1] + Math.Pow(x[2], 3), 3 * x[1] * x[2] * x[2] }
            };
        }
    }
}
#endif
