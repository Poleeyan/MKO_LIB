    namespace MKO_LIB
{
    public class EulerWithRungeMethod
    {
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