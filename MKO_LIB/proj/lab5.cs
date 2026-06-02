using System.Text;

namespace MKO_LIB
{
    public class Lab5
    {
        public static string Run()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=== Лабораторна робота 5: Розв'язання задачі Коші ===");
            sb.AppendLine("Рівняння: y'(x) = 2xy + 3y");
            sb.AppendLine("Умови: x ∈ [0; 1], y(0) = 0.1, h = 0.2");
            sb.AppendLine();

            // Функція f(x, y) = 2xy + 3y
            Func<double, double, double> f = Lab5Equations.F;

            double a = 0.0;
            double b = 1.0;
            double h = 0.2;
            double y0 = 0.1;

            EulersMethod euler = new EulersMethod();
            RungeKuttaMethod rk4 = new RungeKuttaMethod();

            // Розв'язання прямим методом Ейлера
            List<(double x, double y)> resEuler = euler.Solve(f, a, b, h, y0);
            
            // Розв'язання методом Рунге-Кутти (4-го порядку)
            List<(double x, double y)> resRK4 = rk4.Solve(f, a, b, h, y0);

            sb.AppendLine(String.Format("{0,-10} | {1,-25} | {2,-25}", "x", "y (Прямий Ейлер)", "y (Рунге-Кутта 4)"));
            sb.AppendLine(new string('-', 65));

            int steps = Math.Min(resEuler.Count, resRK4.Count);
            
            for (int i = 0; i < steps; i++)
            {
                // Для 0-ї ітерації виводимо початкову точку
                sb.AppendLine(String.Format("{0,-10:F2} | {1,-25:F8} | {2,-25:F8}", 
                    resEuler[i].x, resEuler[i].y, resRK4[i+ 1].y)); // Додано +1 для RK4
            }

            sb.AppendLine(new string('-', 65));
            return sb.ToString();
        }
    }

    public static class Lab5Equations
    {
        public static double F(double x, double y)
        {
            return 2 * x * y + 3 * y;
        }
    }
}
