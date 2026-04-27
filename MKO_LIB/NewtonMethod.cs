using System;

public class NewtonMethod
{
    private readonly Func<double, double> _function;
    private readonly Func<double, double>? _derivative;
    public NewtonMethod(Func<double, double> function, Func<double, double> derivative)
    {
        _function = function;
        _derivative = derivative;
    }
    public NewtonMethod(Func<double, double> function)
    {
        _function = function;
        _derivative = null;
    }
    public NewtonResult Solve(double x, double delta)
    {
        if (_derivative == null)
        {
            throw new InvalidOperationException("Похідна не визначена. Використовуйте конструктор з похідною для методу Ньютона.");
        }

        double x0;
        double fDerivative;
        int iterations = 0;
        double precision;

        do
        {
            // Крок 2: x0 = x (запам'ятовуємо попереднє значення)
            x0 = x;

            // Крок 3: x = x0 - f(x0) / f'(x0) (формула методу Ньютона)
            fDerivative = _derivative(x0);
            
            // Перевіка на близькість похідної до нуля
            if (Math.Abs(fDerivative) < 1e-10)
            {
                throw new InvalidOperationException("Похідна близька до нуля. Метод дотичних не збігається.");
            }

            x = x0 - _function(x0) / fDerivative;

            iterations++;
            precision = Math.Abs(x - x0);

            // Крок 4: перевіка умови збіжності |x - x0| ≤ delta
            // Якщо умова не виконується, цикл повторюється (повернення до кроку 2)
        } while (precision > delta);

        // Крок 5: вивід результату x
        return new NewtonResult
        {
            Root = x,
            FunctionValue = _function(x),
            Iterations = iterations,
            Precision = precision
        };
    }
    public NewtonResult SolveSecant(double x, double h, double delta)
    {
        double x0;
        double fDerivative;
        int iterations = 0;
        double precision;

        do
        {
            // Крок 2: x0 = x (запам'ятовуємо попереднє значення)
            x0 = x;

            // Крок 3: F = (f(x0+h) - f(x0)) / h (числова похідна)
            fDerivative = (_function(x0 + h) - _function(x0)) / h;

            // Перевіка на близькість похідної до нуля
            if (Math.Abs(fDerivative) < 1e-10)
            {
                throw new InvalidOperationException("Числова похідна близька до нуля. Метод січних не збігається.");
            }

            // Крок 4: x = x0 - f(x0) / F (метод січних)
            x = x0 - _function(x0) / fDerivative;

            iterations++;
            precision = Math.Abs(x - x0);

            // Крок 5: перевіка умови збіжності |x - x0| ≤ delta
            // Якщо умова не виконується, цикл повторюється (повернення до кроку 2)
        } while (precision > delta);

        // Крок 6: вивід результату x
        return new NewtonResult
        {
            Root = x,
            FunctionValue = _function(x),
            Iterations = iterations,
            Precision = precision
        };
    }
}
public class NewtonResult
{
    public double Root { get; set; }
    public double FunctionValue { get; set; }
    public int Iterations { get; set; }
    public double Precision { get; set; }
}
