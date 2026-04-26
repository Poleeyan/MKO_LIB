using System;

namespace MKO_LIB
{
    public class FiniteDifferenceMethod
    {
        /// <summary>
        /// Рекурсивний алгоритм визначення скінченних (правих) різниць.
        /// </summary>
        /// <param name="y">Масив значень функції</param>
        /// <param name="p">Порядок різниці</param>
        /// <param name="i">Індекс елемента</param>
        /// <returns>Значення скінченної різниці порядку p для індексу i</returns>
        public static double Right(double[] y, int p, int i)
        {
            // Перевірка, щоб уникнути виходу за межі масиву
            if (i + 1 >= y.Length || (p > 1 && i + p >= y.Length))
            {
                throw new ArgumentOutOfRangeException(nameof(i), "Індекс виходить за межі масиву для заданого порядку різниці.");
            }

            // Згідно з блок-схемою: якщо p == 1
            if (p == 1)
            {
                return y[i + 1] - y[i];
            }
            else // Якщо p != 1 (Hi)
            {
                return Right(y, p - 1, i + 1) - Right(y, p - 1, i);
            }
        }
    }
}
