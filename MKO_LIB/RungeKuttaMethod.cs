using System;
using System.Collections.Generic;

namespace MKO_LIB
{
    /// <summary>
    /// Метод Рунге-Кутти (4-го порядку) для розв'язання задачі Коші
    /// dy/dx = f(x, y), з початковою умовою y(a) = y0
    /// </summary>
    public class RungeKuttaMethod
    {
        /// <summary>
        /// Розв'язує диференціальне рівняння класичним методом Рунге-Кутти 4-го порядку.
        /// </summary>
        /// <param name="f">Функція f(x, y) - права частина диференціального рівняння</param>
        /// <param name="a">Перша межа інтегрування (початкова точка x0)</param>
        /// <param name="b">Друга межа інтегрування (кінцева точка)</param>
        /// <param name="h">Крок інтегрування</param>
        /// <param name="y0">Початкове значення y(a)</param>
        /// <returns>Список пар (x, y) - наближеного розв'язку у вузлах сітки</returns>
        public List<(double x, double y)> Solve(Func<double, double, double> f, double a, double b, double h, double y0)
        {
            List<(double x, double y)> solution = new List<(double x, double y)>();
            
            double x = a;
            double y = y0;
            
            solution.Add((x, y));

            while (x < b && Math.Round(b - x, 8) > 0)
            {
                // Запобігаємо переходу за межу b (останній неповний крок, якщо потрібно)
                double currentH = (x + h > b && Math.Round(x + h - b, 8) > 0) ? b - x : h;

                // Обчислення коефіцієнтів Рунге-Кутти
                double k1 = currentH * f(x, y);
                double k2 = currentH * f(x + currentH / 2.0, y + k1 / 2.0);
                double k3 = currentH * f(x + currentH / 2.0, y + k2 / 2.0);
                double k4 = currentH * f(x + currentH, y + k3);

                // Оновлення значення y
                y = y + (1.0 / 6.0) * (k1 + 2 * k2 + 2 * k3 + k4);
                
                // Збільшення x
                x = x + currentH;
                
                solution.Add((x, y));
            }
            
            return solution;
        }
    }
}
