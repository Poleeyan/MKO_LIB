   namespace MKO_LIB
{
   public class EulersMethod
    {
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