    namespace MKO_LIB
{
    public class TrapezoidalMethod
    {
        public static double Integrate(Func<double, double> f, double a, double b, double h)
        {
            // 3. Ініціалізація суми з граничними значеннями функції
            double I = f(a) + f(b);

            // 4. Цикл по внутрішнім точкам інтервалу
            // Починаємо з першої внутрішньої точки x = a + h
            for (double x = a + h; x < b; x += h)
            {
                // 5. Додаємо подвоєне значення функції у внутрішньої точці
                I = I + 2 * f(x);
            }

            // 6. Множимо на h/2 для отримання наближеного значення інтеграла
            I = I * h / 2;

            // 7. Повертаємо результат
            return I;
        }
    }
    }