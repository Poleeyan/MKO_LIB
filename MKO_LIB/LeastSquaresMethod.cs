using System;

namespace MKO_LIB
{
    /// <summary>
    /// Результати апроксимації методом найменших квадратів
    /// </summary>
    public class LeastSquaresResult
    {
        /// <summary>
        /// Коефіцієнти апроксимуючого поліному: c0 + c1*x + c2*x^2 + ... + cm*x^m
        /// </summary>
        public double[] Coefficients { get; set; } = Array.Empty<double>();

        /// <summary>
        /// Залишкова середньоквадратична похибка
        /// </summary>
        public double RmsError { get; set; }

        /// <summary>
        /// Обчислює значення апроксимуючої функції в точці x
        /// </summary>
        public double GetValue(double x)
        {
            double result = 0;
            for (int i = 0; i < Coefficients.Length; i++)
            {
                result += Coefficients[i] * Math.Pow(x, i);
            }
            return result;
        }
    }

    /// <summary>
    /// Метод найменших квадратів для апроксимації функцій за заданою блок-схемою.
    /// </summary>
    public class LeastSquaresMethod
    {
        /// <summary>
        /// Виконує апроксимацію набору точок поліномом заданого степеня.
        /// </summary>
        /// <param name="x">Вектор значень X</param>
        /// <param name="y">Вектор значень Y</param>
        /// <param name="degree">Степінь апроксимуючого полінома</param>
        /// <returns>Результат з коефіцієнтами та похибкою</returns>
        public static LeastSquaresResult Approximate(double[] x, double[] y, int degree)
        {
            int n = x.Length;
            if (n != y.Length)
                throw new ArgumentException("Масиви x та y повинні мати однакову довжину.");
            
            if (n <= degree)
                throw new ArgumentException("Кількість точок повинна бути більшою за степінь полінома.");

            // 2. Визначення виду апроксимувальної функції (Поліном степеня degree)
            // 3. Складання системи рівнянь
            
            int m = degree + 1; // Кількість невідомих коефіцієнтів
            
            // 4. Отримання системи рівнянь у матричному вигляді
            double[,] a = new double[m, m];
            double[] b = new double[m];

            // 5. Обчислення елементів матриці за МНК
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    double sumA = 0;
                    for (int k = 0; k < n; k++)
                    {
                        sumA += Math.Pow(x[k], i + j);
                    }
                    a[i, j] = sumA;
                }

                double sumB = 0;
                for (int k = 0; k < n; k++)
                {
                    sumB += y[k] * Math.Pow(x[k], i);
                }
                b[i] = sumB;
            }

            // 6. Розв'язання системи рівнянь (Використовуємо метод Гаусса з проекту)
            double[] coefficients = GaussForwardPass.Solve(a, b);

            // 7. Отримання апроксимувальної функції
            var result = new LeastSquaresResult { Coefficients = coefficients };

            // 8. Визначення залишкової середньоквадратичної похибки
            double errorSum = 0;
            for (int k = 0; k < n; k++)
            {
                double approxY = result.GetValue(x[k]);
                double diff = approxY - y[k];
                errorSum += diff * diff;
            }
            
            result.RmsError = Math.Sqrt(errorSum / n);

            // 9. Кінець
            return result;
        }
    }
}
