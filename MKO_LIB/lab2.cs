using System;

namespace MKO_LIB
{
    public class Lab2
    {
        public static void Run()
        {
            // Функція f(x) = x + ln(x) = 0
            // При a₁ = a₂ = a₃ = 1: a₁*x + a₂*ln(a₃*x) = 0 => x + ln(x) = 0
            Func<double, double> f = x => 
            {
                if (x <= 0) throw new ArgumentException("x повинен бути > 0 для ln(x)");
                return x + Math.Log(x);
            };

            // Параметри для методу бісекції
            Console.WriteLine("=== Розв'язання рівняння: x + ln(x) = 0 ===");
            Console.WriteLine("Де: a₁ = a₂ = a₃ = 1\n");

            Console.WriteLine("=== Метод бісекції ===");
            double a = 0.1;      // ліва межа
            double b = 1.0;      // права межа
            double delta = 0.0001; // точність

            Console.WriteLine($"a = {a}, b = {b}, delta = {delta}");

            try
            {
                // Виклик методу бісекції
                var solver = new BisectionMethod(f);
                var result = solver.Solve(a, b, delta);

                // Вивід результату
                Console.WriteLine("\n=== Результат (Метод бісекції) ===");
                Console.WriteLine($"Корінь x ≈ {result.Root:F10}");
                Console.WriteLine($"f({result.Root:F10}) ≈ {result.FunctionValue:F10}");
                Console.WriteLine($"Кількість ітерацій: {result.Iterations}");
                Console.WriteLine($"Остаточна точність: {result.Precision:F10}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }

            // Метод Ньютона
            Console.WriteLine("\n=== Метод Ньютона ===");

            double h = 0.001;    // крок для числової похідної
            double x = 0.5;      // початкове наближення
            double deltaNm = 0.0001; // точність

            Console.WriteLine($"x₀ = {x}, h = {h}, delta = {deltaNm}");

            try
            {
                // Виклик методу Ньютона (січних - з числовою похідною)
                var newtonSolver = new NewtonMethod(f);
                var newtonResult = newtonSolver.SolveSecant(x, h, deltaNm);

                // Вивід результату
                Console.WriteLine("\n=== Результат (Метод Ньютона - січних) ===");
                Console.WriteLine($"Корінь x ≈ {newtonResult.Root:F10}");
                Console.WriteLine($"f({newtonResult.Root:F10}) ≈ {newtonResult.FunctionValue:F10}");
                Console.WriteLine($"Кількість ітерацій: {newtonResult.Iterations}");
                Console.WriteLine($"Остаточна точність: {newtonResult.Precision:F10}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }
    }
}
