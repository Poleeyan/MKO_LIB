  namespace MKO_LIB
{
   public class GaussForwardPass
    {
        public static void ForwardElimination(double[,] a, double[] b, int n)
        {
            // Зовнішній цикл - для кожного стовпця (точка 1: i=0, i<n-1, i++)
            for (int i = 0; i < n - 1; i++)
            {
                // Пошук максимального елемента в стовпці для часткового вибору ведучого елемента (pivoting)
                int maxRow = i;
                for (int m = i + 1; m < n; m++)
                {
                    if (Math.Abs(a[m, i]) > Math.Abs(a[maxRow, i]))
                    {
                        maxRow = m;
                    }
                }

                // Перестановка рядків, якщо максимальний елемент не на поточній діагоналі
                if (maxRow != i)
                {
                    for (int k = i; k < n; k++)
                    {
                        double temp = a[i, k];
                        a[i, k] = a[maxRow, k];
                        a[maxRow, k] = temp;
                    }
                    double tempB = b[i];
                    b[i] = b[maxRow];
                    b[maxRow] = tempB;
                }

                // Перевірка діагонального елемента на нульовість
                if (Math.Abs(a[i, i]) < 1e-12)
                {
                    throw new InvalidOperationException($"Систему неможливо розв'язати: нульовий ведучий елемент a[{i},{i}]");
                }

                // Внутрішній цикл - для кожного рядка нижче (точка 2: j=i+1, j<n, j++)
                for (int j = i + 1; j < n; j++)
                {
                    // Обчислення множника для елімінації (точка 3)
                    double d = a[j, i] / a[i, i];

                    // Цикл по усім елементам рядка (точка 4: k=0, k<n, k++)
                    for (int k = 0; k < n; k++)
                    {
                        // Оновлення елемента матриці (точка 5)
                        a[j, k] = a[j, k] - d * a[i, k];
                    }

                    // Оновлення елемента вектора вільних членів (точка 6)
                    b[j] = b[j] - d * b[i];
                }
            }
        }
        public static double[] BackSubstitution(double[,] a, double[] b, int n)
        {
            double[] x = new double[n];

            // Починаємо з останнього рівняння та рухаємось вверх
            for (int i = n - 1; i >= 0; i--)
            {
                // Ініціалізуємо членом вільного елемента
                x[i] = b[i];

                // Віднімаємо суму добутків (для усіх невідомих хj де j > i)
                for (int j = i + 1; j < n; j++)
                {
                    x[i] -= a[i, j] * x[j];
                }

                // Ділимо на коефіцієнт при невідомому
                x[i] /= a[i, i];
            }

            return x;
        }
        public static double[] Solve(double[,] a, double[] b)
        {
            int n = b.Length;

            // Перевірка коректності даних
            if (a.GetLength(0) != n || a.GetLength(1) != n)
            {
                throw new ArgumentException("Матриця має бути квадратною з розміром n x n");
            }

            // Копіюємо матрицю та вектор, щоб не змінювати вихідні дані
            double[,] aCopy = (double[,])a.Clone();
            double[] bCopy = (double[])b.Clone();

            // Виконуємо прямий хід
            ForwardElimination(aCopy, bCopy, n);

            // Виконуємо зворотне підставлення
            return BackSubstitution(aCopy, bCopy, n);
        }
    }
}