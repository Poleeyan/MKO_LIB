using System;

namespace MKO_LIB
{
    /// <summary>
    /// Simpson's Rule (Simpson's 1/3 Rule) - метод чисельного інтегрування
    /// Використовується для наближеного обчислення визначеного інтеграла
    /// </summary>
    public class SimpsonsRule
    {
        /// <summary>
        /// Обчислює наближене значення визначеного інтеграла функції f(x) на відрізку [a, b]
        /// за методом Сімпсона з кроком h
        /// </summary>
        /// <param name="f">Функція, яку інтегруємо (делегат Func)</param>
        /// <param name="a">Нижня межа інтегрування</param>
        /// <param name="b">Верхня межа інтегрування</param>
        /// <param name="h">Крок розбиття (дельта x)</param>
        /// <returns>Наближене значення інтеграла</returns>
        public static double Calculate(Func<double, double> f, double a, double b, double h)
        {
            // Перевірка коректності вхідних даних
            if (f == null)
                throw new ArgumentNullException(nameof(f), "Функція не може бути null");
            if (a >= b)
                throw new ArgumentException("Нижня межа має бути менша за верхню");
            if (h <= 0)
                throw new ArgumentException("Крок має бути додатним");

            double I = 0; // Накопичувач для суми

            // Ітерація по всім підвідрізкам [x, x+h] на проміжку [a, b]
            for (double x = a; x < b; x += h)
            {
                double xPlusH = x + h;
                
                // Застосування формули Сімпсона для одного підвідрізка:
                // ∫[x, x+h] f(t)dt ≈ (h/6) * (f(x) + 4*f(x+h/2) + f(x+h))
                I += f(x) + 4 * f(x + h / 2) + f(xPlusH);
            }

            // Множення на коефіцієнт h/6
            I *= h / 6;

            return I;
        }

        /// <summary>
        /// Перегружена версія методу з явним заданням функції як строки (полегшує версія для тестування)
        /// </summary>
        public static double Calculate(string functionExpression, double a, double b, double h)
        {
            // Наслідок - потребує використання бібліотеки для парсингу математичних виразів
            // або аналітичного виразу функції
            throw new NotImplementedException("Використовуйте перегрузену версію з Func<double, double>");
        }
    }
}
