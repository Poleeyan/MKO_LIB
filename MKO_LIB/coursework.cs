using System;
using System.Numerics;
using System.Text;

namespace MKO_LIB
{
    public class Coursework
    {
        public static string Run(double a = 1.0, double b = 2.0, double epsilon = 0.01, double x0 = 2.0)
        {
            var output = new StringBuilder();
            
            output.AppendLine("=== Завдання на курсову роботу №2 ===");
            output.AppendLine("Рівняння: 0.5x^5 - 0.005x - 1 = 0");
            
            Func<double, double> f = x => 0.5 * Math.Pow(x, 5) - 0.005 * x - 1.0;
            Func<double, double> df = x => 2.5 * Math.Pow(x, 4) - 0.005;

            // These were previously hardcoded:
            // double a = 1.0;
            // double b = 2.0;
            // double epsilon = 0.01;

            output.AppendLine($"\nІнтервал для дійсного кореня f({a}) = {f(a):F5}, f({b}) = {f(b):F5} : [{a}, {b}]");
            output.AppendLine($"Задана похибка: {epsilon}");

            // 1. Метод половинного ділення
            output.AppendLine("\n--- Метод половинного ділення ---");
            try
            {
                var bisectionMethod = new BisectionMethod(f);
                var bisectionResult = bisectionMethod.Solve(a, b, epsilon);
                output.AppendLine($"Дійсний корінь: {bisectionResult.Root:F5}");
                output.AppendLine($"Кількість кроків (ітерацій): {bisectionResult.Iterations}");
            }
            catch (Exception ex)
            {
                output.AppendLine($"Помилка: {ex.Message}");
            }

            // 2. Метод хорд
            output.AppendLine("\n--- Метод хорд ---");
            try
            {
                var chordMethod = new ChordMethod(f);
                var chordResult = chordMethod.Solve(a, b, epsilon);
                output.AppendLine($"Дійсний корінь: {chordResult.Root:F5}");
                output.AppendLine($"Кількість кроків (ітерацій): {chordResult.Iterations}");
            }
            catch (Exception ex)
            {
                output.AppendLine($"Помилка: {ex.Message}");
            }

            // 3. Метод дотичних (Ньютона)
            output.AppendLine("\n--- Метод дотичних (Ньютона) ---");
            try
            {
                // Початкове наближення передається аргументом
                var newtonMethod = new NewtonMethod(f, df);
                var newtonResult = newtonMethod.Solve(x0, epsilon);
                output.AppendLine($"Початкове наближення x0: {x0}");
                output.AppendLine($"Дійсний корінь: {newtonResult.Root:F5}");
                output.AppendLine($"Кількість кроків (ітерацій): {newtonResult.Iterations}");
            }
            catch (Exception ex)
            {
                output.AppendLine($"Помилка: {ex.Message}");
            }

            // 4. Комплексні корені
            output.AppendLine("\n--- Комплексні корені (модифікований Метод Ньютона) ---");
            // Знаходимо комплексні корені.
            // Рівняння 5-го степеня має 5 коренів. Один дійсний ми вже знайшли.
            // Залишається 4 комплексних корені (2 спряжені пари).
            Complex[] initialGuesses = new Complex[]
            {
                new Complex(0.5, 1.0),
                new Complex(0.5, -1.0),
                new Complex(-1.0, 0.5),
                new Complex(-1.0, -0.5)
            };

            for (int i = 0; i < initialGuesses.Length; i++)
            {
                var (root, iterations) = FindComplexRootNewton(initialGuesses[i], epsilon);
                
                string sign = root.Imaginary >= 0 ? "+" : "-";
                double imagAbs = Math.Abs(root.Imaginary);
                
                output.AppendLine($"Комплексний корінь {i + 1}: {root.Real:F5} {sign} {imagAbs:F5}i (кроків: {iterations})");
            }

            return output.ToString();
        }

        // Локальна реалізація методу Ньютона, що підтримує комплексні числа (System.Numerics.Complex)
        private static (Complex Root, int Iterations) FindComplexRootNewton(Complex z0, double epsilon)
        {
            Complex z = z0;
            int iterations = 0;
            double precision;

            do
            {
                iterations++;
                Complex z_prev = z;
                
                // Значення функції f(z) = 0.5 * z^5 - 0.005 * z - 1
                Complex fz = 0.5 * Complex.Pow(z, 5) - 0.005 * z - 1.0;
                
                // Значення похідної f'(z) = 2.5 * z^4 - 0.005
                Complex dfz = 2.5 * Complex.Pow(z, 4) - 0.005;

                // Метод Ньютона: z_new = z - f(z) / f'(z)
                z = z - fz / dfz;

                precision = Complex.Abs(z - z_prev);
            } while (precision > epsilon && iterations < 100);

            return (z, iterations);
        }
    }
}