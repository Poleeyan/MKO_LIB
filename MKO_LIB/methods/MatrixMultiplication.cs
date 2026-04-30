  namespace MKO_LIB
{
   public class MatrixMultiplication
    {
        public static double[,] MultiplyMatrices(double[,] A, double[,] B)
        {
            // Крок 1. Отримуємо розміри k, m, l з вхідних матриць
            int k = A.GetLength(0); // кількість рядків у матриці A
            int m = A.GetLength(1); // кількість стовпців у матриці A
            int l = B.GetLength(1); // кількість стовпців у матриці B

            // Перевірка сумісності матриць для множення
            if (B.GetLength(0) != m)
            {
                throw new ArgumentException("Кількість стовпців першої матриці має дорівнювати кількості рядків другої.");
            }

            // Матриця-результат
            double[,] C = new double[k, l];

            // Крок 2. i=0, i<k, i++
            for (int i = 0; i < k; i++)
            {
                // Крок 3. j=0, j<l, j++ 
                // (в описі була можлива помилка: j<e, виправлено на j<l за правилами множення матриць)
                for (int j = 0; j < l; j++)
                {
                    // Крок 4. c[i][j]=0
                    C[i, j] = 0;

                    // Крок 5. d=0, d<m, d++
                    for (int d = 0; d < m; d++)
                    {
                        // Крок 6. c[i][j] = c[i][j] + A[i][d] * B[d][j]
                        // (в описі B[d][i] - це типова помилка при переписуванні блок-схеми)
                        C[i, j] += A[i, d] * B[d, j];
                    }
                }
            }

            return C;
        }
    }
}