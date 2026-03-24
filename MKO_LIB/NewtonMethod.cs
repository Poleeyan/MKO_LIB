using System;

/// <summary>
/// Реалізація методу Ньютона та методу січних для знаходження коренів функції.
/// 
/// Метод дотичних (Ньютона) використовує похідну f'(x).
/// Метод січних використовує числову похідну.
/// </summary>
public class NewtonMethod
{
    private readonly Func<double, double> _function;
    private readonly Func<double, double>? _derivative;

    /// <summary>
    /// Конструктор для методу дотичних (Ньютона) з похідною.
    /// </summary>
    /// <param name="function">Функція f(x), для якої шукаємо корінь</param>
    /// <param name="derivative">Похідна функції f'(x)</param>
    public NewtonMethod(Func<double, double> function, Func<double, double> derivative)
    {
        _function = function;
        _derivative = derivative;
    }

    /// <summary>
    /// Конструктор для методу січних без похідної.
    /// </summary>
    /// <param name="function">Функція f(x), для якої шукаємо корінь</param>
    public NewtonMethod(Func<double, double> function)
    {
        _function = function;
        _derivative = null;
    }

    /// <summary>
    /// Розв'язує рівняння f(x) = 0 методом дотичних (Ньютона) з аналітичною похідною.
    /// 
    /// Алгоритм:
    /// 1. Приймаємо початкове наближення x
    /// 2. Запам'ятовуємо попереднє значення x0 = x
    /// 3. Обчислюємо нове наближення: x = x0 - f(x0)/f'(x0)
    /// 4. Перевіряємо умову збіжності: |x - x0| ≤ delta
    /// 5. Якщо умова не виконується, повторюємо з кроку 2
    /// </summary>
    /// <param name="x">Початкове наближення кореня</param>
    /// <param name="delta">Точність (дельта) — максимально допустима помилка</param>
    /// <returns>Результат розв'язування, що містить корінь та кількість ітерацій</returns>
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

    /// <summary>
    /// Розв'язує рівняння f(x) = 0 методом січних з числовою похідною.
    /// 
    /// Алгоритм:
    /// 1. Приймаємо початкове наближення x та крок h
    /// 2. Запам'ятовуємо попереднє значення x0 = x
    /// 3. Обчислюємо числову похідну: f'(x0) ≈ (f(x0+h) - f(x0)) / h
    /// 4. Обчислюємо нове наближення: x = x0 - f(x0) / f'(x0)
    /// 5. Перевіряємо умову збіжності: |x - x0| ≤ delta
    /// 6. Якщо умова не виконується, повторюємо з кроку 2
    /// </summary>
    /// <param name="x">Початкове наближення кореня</param>
    /// <param name="h">Крок для обчислення числової похідної</param>
    /// <param name="delta">Точність (дельта) — максимально допустима помилка</param>
    /// <returns>Результат розв'язування, що містить корінь та кількість ітерацій</returns>
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

/// <summary>
/// Результат розв'язування рівняння методом Ньютона.
/// </summary>
public class NewtonResult
{
    /// <summary>
    /// Знайдене наближення кореня функції
    /// </summary>
    public double Root { get; set; }
    
    /// <summary>
    /// Значення функції в точці кореня f(x)
    /// </summary>
    public double FunctionValue { get; set; }
    
    /// <summary>
    /// Кількість ітерацій, виконаних для сходження
    /// </summary>
    public int Iterations { get; set; }
    
    /// <summary>
    /// Остаточна точність (значення |x - x0| при завершенні)
    /// </summary>
    public double Precision { get; set; }
}
