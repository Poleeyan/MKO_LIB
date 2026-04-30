   namespace MKO_LIB
{
    public class SimpleIterationMethod
    {
        public static SimpleIterationNResult Solve(double[,] A, double[] b, double delta = 0.0001, int maxIterations = 1000)
        {
            int n = b.Length;
            if (A.GetLength(0) != n || A.GetLength(1) != n)
            {
                throw new ArgumentException("Матриця A повинна бути квадратною і за розміром відповідати вектору b.");
            }

            double[] x = new double[n]; // початкове наближення (нулі)
            double[] x_new = new double[n];
            int iterations = 0;
            double[] lastErrors = new double[n];
            
            do
            {
                for (int i = 0; i < n; i++)
                {
                    if (Math.Abs(A[i, i]) < 1e-12)
                    {
                        throw new InvalidOperationException($"Діагональний елемент A[{i},{i}] дорівнює нулю. Метод не застосовний напряму.");
                    }

                    double sum = 0;
                    for (int j = 0; j < n; j++)
                    {
                        if (i != j)
                        {
                            sum += A[i, j] * x[j];
                        }
                    }
                    x_new[i] = (b[i] - sum) / A[i, i];
                }

                double maxError = 0;
                for (int i = 0; i < n; i++)
                {
                    lastErrors[i] = Math.Abs(x_new[i] - x[i]);
                    maxError = Math.Max(maxError, lastErrors[i]);
                    x[i] = x_new[i];
                }

                iterations++;

                if (maxError < delta)
                {
                    break;
                }

                if (iterations >= maxIterations)
                {
                    break; // Перевищен ліміт (ймовірно, метод розходиться)
                }

            } while (true);

            return new SimpleIterationNResult
            {
                X = x,
                Iterations = iterations,
                LastErrors = lastErrors,
                Converged = iterations < maxIterations
            };
        }
    }

    public class SimpleIterationNResult
    {
        public double[] X { get; set; } = Array.Empty<double>();
        public int Iterations { get; set; }
        public double[] LastErrors { get; set; } = Array.Empty<double>();
        public bool Converged { get; set; }
    }
}