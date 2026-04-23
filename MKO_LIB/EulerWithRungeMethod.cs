using System;
using System.Collections.Generic;

namespace MKO_LIB
{
    /// <summary>
    /// Метод Ейлера з оцінкою похибки за правилом Рунге для розв'язання задачі Коші
    /// dy/dx = f(x, y), з початковою умовою y(a) = y0
    /// </summary>
    public class EulerWithRungeMethod
    {
        /// <summary>
        /// Розв'язує диференціальне рівняння методом Ейлера та оцінює похибку
        /// </summary>
        /// <param name="f">Функція f(x, y) - права частина диференціального рівняння</param>
        /// <param name="a">Перша межа інтегрування (початкова точка x0)</param>
        /// <param name="b">Друга межа інтегрування (кінцева точка)</param>
        /// <param name="h">Крок інтегрування</param>
        /// <param name="y0">Початкове значення y(a)</param>
        /// <returns>Список кортежів (x, y, error) - вузли сітки, значення функції та оцінка похибки</returns>
        public List<(double x, double y, double error)> Solve(Func<double, double, double> f, double a, double b, double h, double y0)
        {
            List<(double x, double y, double error)> solution = new List<(double x, double y, double error)>();
            
            // Крок 2: ініціалізація
            double h1 = h / 2.0;
            double y1 = y0;
            double y = y0;
            
            // Крок 3: цикл
            double x = a;

            while (x < b)
            {
                // Крок 4: 
                // Звичайний повний крок (h)
                y = y + h * f(x, y);
                
                // Два половинні кроки (h1 = h/2)
                y1 = y1 + h1 * f(x, y1);
                y1 = y1 + h1 * f(x + h1, y1);
                
                // Обчислення коефіцієнта c та похибки (за блок-схемою)
                double c = (y1 - y) / (Math.Pow(h, 2) - Math.Pow(h1, 2));
                double error = c * Math.Pow(h, 2);
                
                // Збільшення x на крок h
                x = x + h; 
                
                // Крок 5: збереження результату для поточного вузла
                solution.Add((x, y, error));
            }
            
            return solution;
        }

        /// <summary>
        /// Перегружена версія методу без оцінки похибки для сумісності з іншими інтерфейсами
        /// </summary>
        public List<(double x, double y)> SolveWithoutError(Func<double, double, double> f, double a, double b, double h, double y0)
        {
            var resultsWithError = Solve(f, a, b, h, y0);
            List<(double x, double y)> solution = new List<(double x, double y)>();
            
            // Додаємо початкову точку, як в класичних методах
            solution.Add((a, y0));

            foreach (var item in resultsWithError)
            {
                solution.Add((item.x, item.y));
            }
            
            return solution;
        }
    }
}

