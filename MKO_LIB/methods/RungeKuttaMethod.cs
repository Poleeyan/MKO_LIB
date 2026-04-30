 namespace MKO_LIB
{
  public class RungeKuttaMethod
    {
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