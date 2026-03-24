using System;

/// <summary>
/// Метод простої ітерації (метод послідовних наближень) для розв'язання системи 3 рівнянь з 3 невідомими
/// 
/// Призначення:
/// Знаходить наближений розв'язок системи виду:
/// x1 = G1(x1, x2, x3)
/// x2 = G2(x1, x2, x3)
/// x3 = G3(x1, x2, x3)
/// де G1, G2, G3 - функції ітерації (перезаписані вихідні рівняння)
/// 
/// Умови збіжності:
/// Метод збігається, коли норма матриці Якобі < 1 у деякій околиці розв'язку
/// </summary>
public class SimpleIterationMethod
{
    // Функції ітерації G1, G2, G3: перевизначають вихідну систему рівнянь
    private readonly Func<double, double, double, double> _g1;
    private readonly Func<double, double, double, double> _g2;
    private readonly Func<double, double, double, double> _g3;

    /// <summary>
    /// Конструктор методу простої ітерації
    /// </summary>
    /// <param name="g1">Функція ітерації G1(x1, x2, x3)</param>
    /// <param name="g2">Функція ітерації G2(x1, x2, x3)</param>
    /// <param name="g3">Функція ітерації G3(x1, x2, x3)</param>
    public SimpleIterationMethod(
        Func<double, double, double, double> g1,
        Func<double, double, double, double> g2,
        Func<double, double, double, double> g3)
    {
        _g1 = g1;
        _g2 = g2;
        _g3 = g3;
    }

    /// <summary>
    /// Розв'язує систему рівнянь методом простої ітерації
    /// </summary>
    /// <param name="x10">Початкове наближення для x1</param>
    /// <param name="x20">Початкове наближення для x2</param>
    /// <param name="x30">Початкове наближення для x3</param>
    /// <param name="delta">Допустима похибка (точність) для кожної змінної</param>
    /// <returns>Об'єкт результату з розв'язком та додатковою інформацією</returns>
    public SimpleIterationResult Solve(double x10, double x20, double x30, double delta)
    {
        // Крок 1: Ініціалізація змінних
        double x1 = x10, x2 = x20, x3 = x30;
        double x1_prev, x2_prev, x3_prev;
        int iterations = 0;
        const int maxIterations = 10000; // Захист від нескінченного циклу

        // Крок 2: Основний цикл ітерацій
        do
        {
            // Збереження попередніх значень для перевірки збіжності
            x1_prev = x1;
            x2_prev = x2;
            x3_prev = x3;

            // Крок 3: Обчислення нових наближень за функціями ітерації
            x1 = _g1(x1_prev, x2_prev, x3_prev);
            x2 = _g2(x1_prev, x2_prev, x3_prev);
            x3 = _g3(x1_prev, x2_prev, x3_prev);

            iterations++;

            // Вивід інформації про ітерацію
            double error1 = Math.Abs(x1 - x1_prev);
            double error2 = Math.Abs(x2 - x2_prev);
            double error3 = Math.Abs(x3 - x3_prev);

            Console.WriteLine($"Ітерація {iterations}: x1={x1:F10}, x2={x2:F10}, x3={x3:F10}, " +
                            $"|Δx1|={error1:F10}, |Δx2|={error2:F10}, |Δx3|={error3:F10}");

            // Крок 4: Перевірка умови збіжності
            // Якщо всі три умови виконуються одночасно - розв'язок знайдено
            if (error1 < delta && error2 < delta && error3 < delta)
            {
                Console.WriteLine($"Збіжність досягнута за {iterations} ітерацій");
                break;
            }

            // Захист від занадто міцків ітерацій
            if (iterations > maxIterations)
            {
                Console.WriteLine($"Перевищено максимальну кількість ітерацій ({maxIterations})");
                break;
            }

        } while (true); // Повернення до кроку 2 поки не виконуються умови збіжності

        // Крок 5: Вивід результату
        return new SimpleIterationResult
        {
            X1 = x1,
            X2 = x2,
            X3 = x3,
            Iterations = iterations,
            LastError1 = Math.Abs(x1 - x1_prev),
            LastError2 = Math.Abs(x2 - x2_prev),
            LastError3 = Math.Abs(x3 - x3_prev)
        };
    }
}

/// <summary>
/// Результат розв'язання системи методом простої ітерації
/// </summary>
public class SimpleIterationResult
{
    /// <summary>Розв'язок: x1</summary>
    public double X1 { get; set; }

    /// <summary>Розв'язок: x2</summary>
    public double X2 { get; set; }

    /// <summary>Розв'язок: x3</summary>
    public double X3 { get; set; }

    /// <summary>Кількість виконаних ітерацій</summary>
    public int Iterations { get; set; }

    /// <summary>Остання похибка для x1</summary>
    public double LastError1 { get; set; }

    /// <summary>Остання похибка для x2</summary>
    public double LastError2 { get; set; }

    /// <summary>Остання похибка для x3</summary>
    public double LastError3 { get; set; }
}
