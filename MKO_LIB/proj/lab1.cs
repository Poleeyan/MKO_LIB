using System.Text;

namespace MKO_LIB
{
    public class Lab1
    {
        public static string Run()
        {
            var output = new StringBuilder();

            output.AppendLine("=== Лабораторна робота 1 ===");
            output.AppendLine("=== Операції над матрицями ===\n");

            try
            {
                // 1. Додавання матриць
                output.AppendLine("--- 1. Додавання матриць (C = A + B) ---");
                double[,] A_add = { 
                    { 1.0, 2.0, 3.0 }, 
                    { 4.0, 5.0, 6.0 } 
                };
                
                double[,] B_add = { 
                    { 7.0, 8.0, 9.0 }, 
                    { 10.0, 11.0, 12.0 } 
                };

                PrintMatrix(output, "Матриця A", A_add);
                PrintMatrix(output, "Матриця B", B_add);

                var C_add = MatrixAddition.AddMatrices(A_add, B_add);
                PrintMatrix(output, "Результат додавання (C)", C_add);


                // 2. Множення матриць
                output.AppendLine("--- 2. Множення матриць (C = A * B) ---");
                double[,] A_mult = { 
                    { 1.0, 2.0 }, 
                    { 3.0, 4.0 },
                    { 5.0, 6.0 }
                }; // Розмір: 3x2
                
                double[,] B_mult = { 
                    { 7.0, 8.0, 9.0 }, 
                    { 10.0, 11.0, 12.0 } 
                }; // Розмір: 2x3

                PrintMatrix(output, "Матриця A (3x2)", A_mult);
                PrintMatrix(output, "Матриця B (2x3)", B_mult);

                var C_mult = MatrixMultiplication.MultiplyMatrices(A_mult, B_mult);
                PrintMatrix(output, "Результат множення (C розміром 3x3)", C_mult);
            }
            catch (Exception ex)
            {
                output.AppendLine($"Помилка: {ex.Message}");
            }

            return output.ToString();
        }

        private static void PrintMatrix(StringBuilder sb, string title, double[,] matrix)
        {
            sb.AppendLine($"{title}:");
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    sb.Append($"{matrix[i, j],5} ");
                }
                sb.AppendLine();
            }
            sb.AppendLine();
        }
    }
}
