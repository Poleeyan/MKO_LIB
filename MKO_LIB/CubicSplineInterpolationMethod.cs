using System;

namespace MKO_LIB
{
    /// <summary>
    /// Метод інтерполяції кубічними сплайнами (поліномами третього порядку).
    /// </summary>
    public class CubicSplineInterpolationMethod
    {
        /// <summary>
        /// Обчислює значення функції в точці x за допомогою кубічного сплайну, 
        /// реалізовано згідно з наданою блок-схемою.
        /// </summary>
        /// <param name="X">Масив вузлів інтерполяції</param>
        /// <param name="Y">Масив значень функції у вузлах</param>
        /// <param name="targetX">Точка, в якій потрібно знайти наближене значення</param>
        /// <returns>Приблизне значення функції в точці targetX</returns>
        public static double Calculate(double[] X, double[] Y, double targetX)
        {
            // Крок 1: Початок
            if (X.Length != Y.Length || X.Length < 3)
            {
                throw new ArgumentException("Масиви X та Y повинні мати однакову довжину і містити хоча б 3 точки.");
            }

            int n = X.Length;
            
            // Крок 2, 3: Складання рівнянь зі спряження в вузлових точках 
            // та за умови рівності перших та других похідних.
            // Крок 4: Складання рівнянь значень других похідних в крайніх точках.
            // Крок 5: Отримання системи рівнянь у матричному вигляді
            double[,] A = new double[n, n];
            double[] B = new double[n];
            
            // Заповнення матриці системи (для других похідних M)
            // Природні крайові умови: M_0 = 0, M_{n-1} = 0 (друга похідна на кінцях дорівнює нулю)
            A[0, 0] = 1.0;
            B[0] = 0.0;
            
            A[n - 1, n - 1] = 1.0;
            B[n - 1] = 0.0;
            
            // Формування тридіагональної матриці для внутрішніх вузлів
            for (int i = 1; i < n - 1; i++)
            {
                double h_i = X[i] - X[i - 1];
                double h_i1 = X[i + 1] - X[i];
                
                A[i, i - 1] = h_i / 6.0;
                A[i, i] = (h_i + h_i1) / 3.0;
                A[i, i + 1] = h_i1 / 6.0;
                
                B[i] = (Y[i + 1] - Y[i]) / h_i1 - (Y[i] - Y[i - 1]) / h_i;
            }

            // Крок 6: Розв'язання системи рівнянь
            // Використовуємо існуючий метод Гаусса з MKO_LIB для знаходження значень других похідних
            GaussForwardPass.ForwardElimination(A, B, n);
            double[] M = GaussForwardPass.BackSubstitution(A, B, n);

            // Крок 7: Отримання системи рівнянь для розв'язання задачі інтерполяції сплайнами
            // Знаходимо проміжок [X[i], X[i+1]], в який потрапляє точка targetX
            int intervalIndex = 0;
            for (int i = 0; i < n - 1; i++)
            {
                if (targetX >= X[i] && targetX <= X[i + 1])
                {
                    intervalIndex = i;
                    break;
                }
            }
            
            // Якщо targetX виходить за межі масиву (екстраполяція), беремо крайній проміжок
            if (targetX < X[0]) intervalIndex = 0;
            if (targetX > X[n - 1]) intervalIndex = n - 2;

            double xi = X[intervalIndex];
            double xi1 = X[intervalIndex + 1];
            double yi = Y[intervalIndex];
            double yi1 = Y[intervalIndex + 1];
            double hi = xi1 - xi;
            
            double mi = M[intervalIndex];
            double mi1 = M[intervalIndex + 1];

            // Формула інтерполяційного кубічного сплайну:
            // S(x) = M_i * (x_{i+1} - x)^3 / (6h) + M_{i+1} * (x - x_i)^3 / (6h) +
            //        + (y_i - M_i*h^2/6) * (x_{i+1} - x)/h +
            //        + (y_{i+1} - M_{i+1}*h^2/6) * (x - x_i)/h
            
            double term1 = mi * Math.Pow(xi1 - targetX, 3) / (6.0 * hi);
            double term2 = mi1 * Math.Pow(targetX - xi, 3) / (6.0 * hi);
            double term3 = (yi - mi * Math.Pow(hi, 2) / 6.0) * (xi1 - targetX) / hi;
            double term4 = (yi1 - mi1 * Math.Pow(hi, 2) / 6.0) * (targetX - xi) / hi;
            
            double result = term1 + term2 + term3 + term4;

            // Крок 8: Кінець
            return result;
        }
    }
}
