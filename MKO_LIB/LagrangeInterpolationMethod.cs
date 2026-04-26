using System;

namespace MKO_LIB
{
    /// <summary>
    /// Метод інтерполяції за допомогою полінома Лагранжа.
    /// </summary>
    public class LagrangeInterpolationMethod
    {
        /// <summary>
        /// Обчислює значення полінома Лагранжа для заданого значення x, згідно з наданою блок-схемою.
        /// </summary>
        /// <param name="X">Масив вузлів інтерполяції</param>
        /// <param name="Y">Масив значень функції у вузлах</param>
        /// <param name="x">Точка, в якій потрібно знайти наближене значення</param>
        /// <returns>Приблизне значення функції в точці x</returns>
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
