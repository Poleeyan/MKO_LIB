using System;

namespace MKO_LIB
{
    public class LagrangeInterpolationMethod
    {
        public static double Calculate(double[] X, double[] Y, double x)
        {
            if (X.Length != Y.Length)
            {
                throw new ArgumentException("Масиви X та Y повинні мати однакову довжину.");
            }

            int N = X.Length;
            
            // Крок 3: yx = 0
            double yx = 0;

            // Крок 4: Цикл i = 0; i < N; i++ (індексація зазвичай з 0 до N-1)
            for (int i = 0; i < N; i++)
            {
                // Крок 5: Pr = 1
                double Pr = 1;

                // Крок 6: Цикл j = 0; j < N; j++
                for (int j = 0; j < N; j++)
                {
                    // Крок 7: i != j
                    if (i != j)
                    {
                        // Крок 8: Pr = Pr * (x - X[j]) / (X[i] - X[j])
                        Pr = Pr * (x - X[j]) / (X[i] - X[j]);
                    }
                }

                // Крок 9: yx = yx + Y[i] * Pr
                yx = yx + Y[i] * Pr;
            }

            // Крок 10: Виведення yx (повертаємо результат)
            return yx;
        }
    }
}
