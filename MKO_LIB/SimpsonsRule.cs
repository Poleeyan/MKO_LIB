using System;

namespace MKO_LIB
{
    /// <summary>
    /// Simpson's Rule (Simpson's 1/3 Rule) - метод чисельного інтегрування
    /// Використовується для наближеного обчислення визначеного інтеграла
    /// </summary>
    public class SimpsonsRule
    {
        public static double Calculate(Func<double, double> f, double a, double b, int n)
        {
            if (f == null)
                throw new ArgumentNullException(nameof(f), "Функція не може бути null");
            if (a >= b)
                throw new ArgumentException("Нижня межа має бути менша за верхню");
            if (n <= 0)
                throw new ArgumentException("Кількість інтервалів має бути більшою за нуль");

            if (n % 2 != 0) 
            {
                n += 1; 
            }

            double h = (b - a) / n;
            double I = f(a) + f(b);

            for (int i = 1; i < n; i++)
            {
                double x = a + i * h;
                I += (i % 2 == 0) ? 2 * f(x) : 4 * f(x);
            }

            I *= h / 3.0;

            return I;
        }

        public static double Calculate(string functionExpression, double a, double b, int n)
        {
            throw new NotImplementedException("Використовуйте перегружену версію з Func<double, double>");
        }
    }
}
