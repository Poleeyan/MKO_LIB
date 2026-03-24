using System;
using System.Collections.Generic;

namespace MKO_LIB
{
    /// <summary>
    /// Метод Ейлера для розв'язання задачі Коші
    /// dy/dx = f(x, y), з початковою умовою y(a) = y0
    /// </summary>
    public class EulersMethod
    {
        /// <summary>
        /// Розв'язує диференціальне рівняння методом Ейлера
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
            
            // Кроки алгоритму:
            double x = a;           // Крок 3: ініціалізація x = a
            double y = y0;          // Крок 2: ініціалізація y = y0
            
            // Крок 3: цикл поки x < b
            while (x < b)
            {
                // Крок 4: обчислення нового значення y за формулою Ейлера
                // y(i+1) = y(i) + h * f(x(i), y(i))
                y = y + h * f(x, y);
                
                // Збільшення x на крок h
                x = x + h;
                
                // Крок 5: збереження результату (x, y)
                solution.Add((x, y));
            }
            
            return solution;
        }
        
        /// <summary>
        /// Перегружена версія методу для комфортнішого використання без анонімної функції
        /// </summary>
        /// <param name="a">Початкова точка</param>
        /// <param name="b">Кінцева точка</param>
        /// <param name="h">Крок інтегрування</param>
        /// <param name="y0">Початкове значення y(a)</param>
        /// <param name="f">Функція f(x, y)</param>
        /// <returns>Масив значень y у вузлах сітки</returns>
        public double[] SolveToArray(double a, double b, double h, double y0, Func<double, double, double> f)
        {
            List<double> results = new List<double> { y0 };
            
            double x = a;
            double y = y0;
            
            while (x < b)
            {
                y = y + h * f(x, y);
                x = x + h;
                results.Add(y);
            }
            
            return results.ToArray();
        }
    }
}
