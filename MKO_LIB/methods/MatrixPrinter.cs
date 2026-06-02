using System.Text;

namespace MKO_LIB
{
    public static class MatrixPrinter
    {
        public static void PrintMatrix(StringBuilder sb, string title, double[,] matrix)
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
