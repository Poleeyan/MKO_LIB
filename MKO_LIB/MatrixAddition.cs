namespace MKO_LIB
{
    /// <summary>
    /// Клас для виконання операцій додавання матриць
    /// </summary>
    public class MatrixAddition
    {
        /// <summary>
        /// Додає дві матриці A та B
        /// </summary>
        /// <param name="A">Матриця A розміру n×m</param>
        /// <param name="B">Матриця B розміру n×m</param>
        /// <returns>Матриця C = A + B</returns>
        public static double[,] AddMatrices(double[,] A, double[,] B)
        {
            // Отримуємо розмір матриці A: n - кількість рядків, m - кількість стовпців
            int n = A.GetLength(0);
            int m = A.GetLength(1);

            // Перевірка, що матриці мають однаковий розмір
            if (B.GetLength(0) != n || B.GetLength(1) != m)
            {
                throw new ArgumentException("Матриці повинні мати однаковий розмір");
            }

            // Створюємо матрицю результату
            double[,] C = new double[n, m];

            // Зовнішній цикл - перебираємо рядки (i від 0 до n-1)
            for (int i = 0; i < n; i++)
            {
                // Внутрішній цикл - перебираємо стовпці (j від 0 до m-1)
                for (int j = 0; j < m; j++)
                {
                    // Складаємо елементи матриць А та B
                    C[i, j] = A[i, j] + B[i, j];
                }
            }

            return C;
        }
    }
}
