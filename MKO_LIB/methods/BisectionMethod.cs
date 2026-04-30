namespace MKO_LIB
{
public class BisectionMethod
{
    private readonly Func<double, double> _function;

    public BisectionMethod(Func<double, double> function)
    {
        _function = function;
    }

    public BisectionResult Solve(double a, double b, double delta)
    {
        // Перевірка коректності вхідних даних
        if (_function(a) * _function(b) > 0)
        {
            throw new ArgumentException("f(a) та f(b) повинні мати різні знаки!");
        }

        double c;
        int iterations = 0;

        do
        {
            // Крок 2: c = (a+b) / 2
            c = (a + b) / 2;
            iterations++;

            // Крок 3: f(a)*f(c) > 0? якщо так: a=c, якщо ні: b=c
            if (_function(a) * _function(c) > 0)
            {
                a = c;
            }
            else
            {
                b = c;
            }

            // Крок 4: |b-a| >= delta? якщо так: повернення до 2, якщо ні: вивід c
        } while (Math.Abs(b - a) >= delta);

        return new BisectionResult
        {
            Root = c,
            FunctionValue = _function(c),
            Iterations = iterations,
            Precision = Math.Abs(b - a)
        };
    }
}

public class BisectionResult
{
    public double Root { get; set; }
    public double FunctionValue { get; set; }
    public int Iterations { get; set; }
    public double Precision { get; set; }
}
}