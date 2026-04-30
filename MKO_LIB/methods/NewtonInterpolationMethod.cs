   namespace MKO_LIB
{
   public class NewtonInterpolationMethod
    {
        public static double CalculateFirstFormula(double[] X, double[] Y, double h, double x)
        {
            if (X.Length != Y.Length)
            {
                throw new ArgumentException("Масиви X та Y повинні мати однакову довжину.");
            }

            int n = X.Length;
            
            // Крок 3: d = 1, P = Y[0]
            double d = 1;
            double P = Y[0];

            // Крок 4: Цикл i = 1; i < n; i++ (у блок-схемі могло бути i < n або i <= n - 1)
            for (int i = 1; i < n; i++)
            {
                // Крок 5: Pr = 1
                double Pr = 1;

                // Крок 6: Цикл j = 0; j < i; j++
                for (int j = 0; j < i; j++)
                {
                    // Крок 7: Pr = Pr * (x - X[j])
                    Pr = Pr * (x - X[j]);
                }

                // Крок 8: d = d * i * h
                d = d * i * h;

                // Крок 9: P = P + (Right(Y, i, 0) * Pr / d)
                double rightDiff = FiniteDifferenceMethod.Right(Y, i, 0);
                P = P + (rightDiff * Pr / d);
            }

            // Крок 10: Виведення P (повертаємо результат)
            return P;
        }
    }
    }