using System.Text;

namespace MKO_LIB
{
    public class Lab3
    {
        public static string Run()
        {
            var output = new StringBuilder();

            output.AppendLine("=== Lab3: Розв'язання системи лінійних алгебраїчних рівнянь ===");
            output.AppendLine("Система рівнянь:");
            output.AppendLine(" 2x1 - 3x2 + 3x3 + 2x4 =  3");
            output.AppendLine(" 6x1 + 9x2 - 2x3 -  x4 = -4");
            output.AppendLine("10x1 + 3x2 - 3x3 - 2x4 =  3");
            output.AppendLine(" 8x1 + 6x2 +  x3 + 3x4 = -7");
            output.AppendLine("\n");

            // Задаємо матрицю системи та вектор вільних членів
            double[,] A = {
                { 2, -3, 3, 2 },
                { 6, 9, -2, -1 },
                { 10, 3, -3, -2 },
                { 8, 6, 1, 3 }
            };

            double[] B = { 3, -4, 3, -7 };

            // 1. Метод Гауса
            output.AppendLine("=== 1. Метод Гауса ===");
            try
            {
                double[] gaussResult = GaussForwardPass.Solve(A, B);
                output.AppendLine("Результат (Метод Гауса):");
                for (int i = 0; i < gaussResult.Length; i++)
                {
                    output.AppendLine($"x{i + 1} = {gaussResult[i]:F6}");
                }
            }
            catch (Exception ex)
            {
                output.AppendLine($"Помилка: {ex.Message}");
            }

            // 2. Метод Крамера
            output.AppendLine("\n=== 2. Метод Крамера ===");
            try
            {
                int n = B.Length;
                double[] cramerResult = CramersRule.Solve(A, B, out double detMain, out double[] detI);

                output.AppendLine($"Головний визначник Δ = {detMain:F4}");
                for (int i = 0; i < n; i++)
                {
                    output.AppendLine($"Δ{i + 1} = {detI[i]:F4}");
                }

                output.AppendLine("\nРезультат (Метод Крамера):");
                for (int i = 0; i < n; i++)
                {
                    output.AppendLine($"x{i + 1} = {cramerResult[i]:F6}");
                }
            }
            catch (Exception ex)
            {
                output.AppendLine($"Помилка методу Крамера: {ex.Message}");
            }

            return output.ToString();
        }
    }
}