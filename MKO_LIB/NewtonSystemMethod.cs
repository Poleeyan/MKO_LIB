using System;
using System.Linq;

namespace MKO_LIB
{
    /// <summary>
    /// Клас для розв'язання систем нелінійних рівнянь методом Ньютона.
    /// Відповідно до блок-схеми:
    /// X^(k+1) = X^(k) - [W(X^(k))]^-1 * F(X^(k))
    /// </summary>
    public class NewtonSystemMethod
    {
        private readonly Func<double[], double[]> _systemFunctions;
        private readonly Func<double[], double[,]> _jacobianMatrix;

        /// <summary>
        /// Конструктор для методу Ньютона (для систем нелінійних рівнянь).
        /// </summary>
        /// <param name="systemFunctions">Функція, що повертає вектор значень системи F(X)</param>
        /// <param name="jacobianMatrix">Функція, що повертає матрицю Якобі W(X)</param>
        public NewtonSystemMethod(Func<double[], double[]> systemFunctions, Func<double[], double[,]> jacobianMatrix)
        {
            _systemFunctions = systemFunctions;
            _jacobianMatrix = jacobianMatrix;
        }

        /// <summary>
        /// Розв'язує систему нелінійних рівнянь методом Ньютона.
        /// </summary>
        /// <param name="x0">Початкове наближення X^(0)</param>
        /// <param name="epsilon">Похибка S</param>
        /// <param name="maxIterations">Максимальна кількість ітерацій Max</param>
        /// <returns>Вектор розв'язку X та кількість виконаних ітерацій</returns>
        public (double[] Root, int Iterations) Solve(double[] x0, double epsilon, int maxIterations = 100)
        {
            int n = x0.Length;
            double[] x = (double[])x0.Clone();
            int k = 0;
            double error = double.MaxValue;

            while (error > epsilon && k < maxIterations)
            {
                // 3: Розрахунок якобіана W(x)
                double[,] W = _jacobianMatrix(x);
                
                // Та вектор значень функцій F(X)
                double[] F = _systemFunctions(x);

                // 4: Розрахунок ітераційної формули: X^(k+1) = X^(k) - (W^-1 * F)
                // Для уникнення пошуку оберненої матриці, розв'язуємо САР: W * \Delta X = F
                // Тоді X^(k+1) = X^(k) - \Delta X
                
                double[,] a = (double[,])W.Clone();
                double[] b = (double[])F.Clone();
                
                GaussForwardPass.ForwardElimination(a, b, n);
                double[] deltaX = GaussForwardPass.BackSubstitution(a, b, n);

                double[] xNext = new double[n];
                for (int i = 0; i < n; i++)
                {
                    xNext[i] = x[i] - deltaX[i];
                }

                // 5: Розрахунок похибки S = ||X^(k+1) - X^(k)||
                error = 0;
                for (int i = 0; i < n; i++)
                {
                    double diff = Math.Abs(xNext[i] - x[i]);
                    if (diff > error)
                    {
                        error = diff; // Використовуємо норму Максимуму (можна також евклідову)
                    }
                }

                x = xNext;
                k++;
            }

            if (k >= maxIterations && error > epsilon)
            {
                // Вихід за число ітерацій
                throw new InvalidOperationException("Досягнуто максимальну кількість ітерацій: метод не зійшовся.");
            }

            return (x, k);
        }
    }
}
