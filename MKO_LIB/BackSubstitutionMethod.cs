using System;

public class BackSubstitutionMethod
{
    public static double[] Solve(double[][] a, double[] b, int n)
    {
        // Ініціалізуємо вектор розв'язків
        double[] x = new double[n];
        
        // Крок 1: Починаємо з останнього рядка і рухаємось вверх
        for (int i = n - 1; i >= 0; i--)
        {
            // Крок 2: Ініціалізуємо суму D для поточного рядка
            double D = 0;
            
            // Крок 3: Обчислюємо суму D = x[j] * a[i][j] для всіх j > i
            for (int j = n - 1; j > i; j--)
            {
                // Крок 4: Додаємо добуток до суми
                D = D + x[j] * a[i][j];
            }
            
            // Крок 5: Обчислюємо x[i] за формулою: x[i] = (b[i] - D) / a[i][i]
            // Перевіряємо, щоб уникнути ділення на нуль
            if (Math.Abs(a[i][i]) < 1e-10)
            {
                throw new InvalidOperationException($"Діагональний елемент a[{i}][{i}] = 0. Матриця виродилась.");
            }
            
            x[i] = (b[i] - D) / a[i][i];
        }
        
        // Повертаємо вектор розв'язків
        return x;
    }
}
