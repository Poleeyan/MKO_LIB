using System.Text;

namespace MKO_LIB
{
    public static class Lab4
    {
        public static string Run()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=== Лабораторна робота 4: Чисельне інтегрування ===");
            sb.AppendLine("Інтеграл: I = ∫(1 до 10) (lg x) / x^4 dx");
            sb.AppendLine();

            // Функція для інтегрування: lg(x) / x^4  (lg - десятковий логарифм)
            Func<double, double> f = (x) => Math.Log10(x) / Math.Pow(x, 4);

            double a = 1.0;
            double b = 10.0;
            int[] intervals = { 4, 5, 10, 20 };

            sb.AppendLine("Результати:");
            sb.AppendLine(new string('-', 65));
            sb.AppendLine(String.Format("{0,-5} | {1,-25} | {2,-25}", "n", "Метод Трапецій", "Метод Сімпсона"));
            sb.AppendLine(new string('-', 65));

            foreach (int n in intervals)
            {
                double h = (b - a) / n;
                
                double trapResult = TrapezoidalMethod.Integrate(f, a, b, h);
                double simpResult = SimpsonsRule.Calculate(f, a, b, n);

                sb.AppendLine(String.Format("{0,-5} | {1,-25:F8} | {2,-25:F8}", n, trapResult, simpResult));
            }

            sb.AppendLine(new string('-', 65));
            return sb.ToString();
        }
    }
}
