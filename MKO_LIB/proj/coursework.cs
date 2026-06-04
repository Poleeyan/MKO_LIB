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
            
            Func<double, double> f = CourseworkEquations.F;
            Func<double, double> df = CourseworkEquations.Df;

            output.AppendLine($"\nІнтервал для дійсного кореня f({a}) = {f(a):F5}, f({b}) = {f(b):F5} : [{a}, {b}]");
            output.AppendLine($"Задана похибка: {epsilon}");

            // 1. Метод половинного ділення
            output.AppendLine("\n--- Метод половинного ділення ---");
            try
            {
                var bisectionMethod = new BisectionMethod(f);
                var bisectionResult = bisectionMethod.Solve(a, b, epsilon);
                
                output.AppendLine(string.Format("{0,-6} | {1,-12} | {2,-12} | {3,-12} | {4,-12} | {5,-12}",
                    "Крок", "a", "b", "c", "f(c)", "Ширина"));
                output.AppendLine(new string('-', 73));
                foreach (var step in bisectionResult.Steps)
                {
                    output.AppendLine(string.Format("{0,-6} | {1,-12:F6} | {2,-12:F6} | {3,-12:F6} | {4,-12:F6} | {5,-12:F6}",
                        step.Iteration, step.A, step.B, step.C, step.FC, step.Precision));
                }
                output.AppendLine(new string('-', 73));
                output.AppendLine($"Дійсний корінь: {bisectionResult.Root:F6}");
                output.AppendLine($"Кількість кроків (ітерацій): {bisectionResult.Iterations}");
                output.AppendLine($"f(x*) = {bisectionResult.FunctionValue:E6}");
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
                
                output.AppendLine(string.Format("{0,-6} | {1,-12} | {2,-12} | {3,-12} | {4,-12}",
                    "Крок", "a", "b", "x", "f(x)"));
                output.AppendLine(new string('-', 59));
                foreach (var step in chordResult.Steps)
                {
                    output.AppendLine(string.Format("{0,-6} | {1,-12:F6} | {2,-12:F6} | {3,-12:F6} | {4,-12:F6}",
                        step.Iteration, step.A, step.B, step.X, step.FX));
                }
                output.AppendLine(new string('-', 59));
                output.AppendLine($"Дійсний корінь: {chordResult.Root:F6}");
                output.AppendLine($"Кількість кроків (ітерацій): {chordResult.Iterations}");
                output.AppendLine($"f(x*) = {chordResult.FunctionValue:E6}");
            }
            catch (Exception ex)
            {
                output.AppendLine($"Помилка: {ex.Message}");
            }

            // 3. Метод дотичних (Ньютона)
            output.AppendLine("\n--- Метод дотичних (Ньютона) ---");
            try
            {
                var newtonMethod = new NewtonMethod(f, df);
                var newtonResult = newtonMethod.Solve(x0, epsilon);
                
                output.AppendLine($"Початкове наближення x0: {x0}");
                output.AppendLine(string.Format("{0,-6} | {1,-12} | {2,-12} | {3,-12} | {4,-12} | {5,-12}",
                    "Крок", "x_{k-1}", "f(x_{k-1})", "f'(x_{k-1})", "x_k", "Похибка"));
                output.AppendLine(new string('-', 73));
                foreach (var step in newtonResult.Steps)
                {
                    output.AppendLine(string.Format("{0,-6} | {1,-12:F6} | {2,-12:F6} | {3,-12:F6} | {4,-12:F6} | {5,-12:E4}",
                        step.Iteration, step.XPrev, step.FXPrev, step.DFxPrev, step.XCurr, step.Precision));
                }
                output.AppendLine(new string('-', 73));
                output.AppendLine($"Дійсний корінь: {newtonResult.Root:F6}");
                output.AppendLine($"Кількість кроків (ітерацій): {newtonResult.Iterations}");
                output.AppendLine($"f(x*) = {newtonResult.FunctionValue:E6}");
            }
            catch (Exception ex)
            {
                output.AppendLine($"Помилка: {ex.Message}");
            }

            // 4. Комплексні корені
            output.AppendLine("\n--- Комплексні корені (модифікований Метод Ньютона) ---");
            
            Func<Complex, Complex> fc = CourseworkEquations.Fc;
            Func<Complex, Complex> dfc = CourseworkEquations.Dfc;
            Complex[] initialGuesses =
            [
                new Complex(0.5, 1.0),
                new Complex(0.5, -1.0),
                new Complex(-1.0, 0.5),
                new Complex(-1.0, -0.5)
            ];

            for (int i = 0; i < initialGuesses.Length; i++)
            {
                var complexResult = NewtonMethod.SolveComplex(fc, dfc, initialGuesses[i], epsilon);
                
                output.AppendLine($"\nКомплексний корінь {i + 1} (початкове наближення z0 = {initialGuesses[i]}):");
                output.AppendLine(string.Format("{0,-6} | {1,-30} | {2,-30} | {3,-12}",
                    "Крок", "z_{k-1}", "z_k", "Похибка"));
                output.AppendLine(new string('-', 85));
                
                foreach (var step in complexResult.Steps)
                {
                    string zPrevStr = $"{step.ZPrev.Real:F5} {(step.ZPrev.Imaginary >= 0 ? "+" : "-")} {Math.Abs(step.ZPrev.Imaginary):F5}i";
                    string zCurrStr = $"{step.ZCurr.Real:F5} {(step.ZCurr.Imaginary >= 0 ? "+" : "-")} {Math.Abs(step.ZCurr.Imaginary):F5}i";
                    
                    output.AppendLine(string.Format("{0,-6} | {1,-30} | {2,-30} | {3,-12:E4}",
                        step.Iteration, zPrevStr, zCurrStr, step.Precision));
                }
                output.AppendLine(new string('-', 85));
                
                Complex root = complexResult.Root;
                string sign = root.Imaginary >= 0 ? "+" : "-";
                double imagAbs = Math.Abs(root.Imaginary);
                
                output.AppendLine($"Результат: {root.Real:F6} {sign} {imagAbs:F6}i (кроків: {complexResult.Iterations})");
            }

            return output.ToString();
        }
    }

    public static class CourseworkEquations
    {
        public static double F(double x)
        {
            return 0.5 * Math.Pow(x, 5) - 0.005 * x - 1.0;
        }

        public static double Df(double x)
        {
            return 2.5 * Math.Pow(x, 4) - 0.005;
        }

        public static Complex Fc(Complex z)
        {
            return 0.5 * Complex.Pow(z, 5) - 0.005 * z - 1.0;
        }

        public static Complex Dfc(Complex z)
        {
            return 2.5 * Complex.Pow(z, 4) - 0.005;
        }
    }
}