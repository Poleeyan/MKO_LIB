using System;

public class ChordMethod
{
    private readonly Func<double, double> _function;

    public ChordMethod(Func<double, double> function)
    {
        _function = function;
    }

    public ChordResult Solve(double a, double b, double epsilon)
    {
        // Крок 2: f(a)*f(b) < 0 у випадку не переходу до наступного кроку кінець
        if (_function(a) * _function(b) >= 0)
        {
            throw new ArgumentException("Функція повинна мати різні знаки на кінцях відрізка f(a)*f(b) < 0!");
        }

        double x = 0;
        int iterations = 0;

        while (true)
        {
            iterations++;

            double fa = _function(a);
            double fb = _function(b);

            // Крок 3: x = a - f(a)*(b-a)/(f(b)-f(a)) (коректна формула методу хорд)
            x = a - (fa * (b - a)) / (fb - fa);

            double fx = _function(x);

            // Крок 4: |f(x)| < epsilon, якщо ні - повертається до кроку 2 (з оновленням меж)
            if (Math.Abs(fx) < epsilon)
            {
                break; // досягнута задана точність, кінець
            }

            // Оновлення початкових точок, щоб шуканий корінь залишався на інтервалі
            if (fa * fx < 0)
            {
                b = x;
            }
            else
            {
                a = x;
            }
        }

        // Крок 5: вивід x
        return new ChordResult
        {
            Root = x,
            FunctionValue = _function(x),
            Iterations = iterations
        };
    }
    public ChordResult SolveAlternative(double a, double b, double epsilon)
    {
        // Крок 2: x0 = b (припускаємо, що h у вашій блок-схемі означає другу межу відрізка b)
        double aFixed = a;
        double fa = _function(aFixed);
        double xn = b; 
        double xn1 = xn;
        int iterations = 0;

        while (true)
        {
            iterations++;
            double fxn = _function(xn);

            // Крок 3: xn+1 = xn - (f(xn)*(xn-a))/(f(xn)-f(a))
            xn1 = xn - (fxn * (xn - aFixed)) / (fxn - fa);

            // Крок 4: |xn+1 - xn| < epsilon, при невиконанні повертається до кроку 3
            if (Math.Abs(xn1 - xn) < epsilon)
            {
                break; // Точність досягнута
            }

            xn = xn1; // перехід до наступної ітерації
        }

        // Крок 5: вивід xn+1
        return new ChordResult
        {
            Root = xn1,
            FunctionValue = _function(xn1),
            Iterations = iterations
        };
    }
}

public class ChordResult
{
    public double Root { get; set; }
    public double FunctionValue { get; set; }
    public int Iterations { get; set; }
}
