using System.Text;

namespace MKO_LIB
{
    public class Lab8
    {
        public static string Run()
        {
            var output = new StringBuilder();
            output.AppendLine("=== Лабораторна робота 8 ===");
            output.AppendLine("=== Побудова сплайнів третього порядку ===");
            output.AppendLine("=== Варіант 7 ===\n");

            try
            {
                // Табличні дані
                double[] X = { -2, 1, 3, 5, 7 };
                double[] Y = { 10, 2, 6, -6, 12 };
                double xTarget = 2; // Точка інтерполяції

                output.AppendLine("Вхідні дані (таблиця):");
                output.AppendLine($"x:  {string.Join("  ", X.Select(x => $"{x,3}"))}");
                output.AppendLine($"y:  {string.Join("  ", Y.Select(y => $"{y,3}"))}");
                output.AppendLine($"Точка інтерполяції x* = {xTarget}\n");

                int n = X.Length - 1; // Кількість інтервалів (3)
                int numEq = 4 * n;    // Кількість рівнянь (12)
                double[,] A = new double[numEq, numEq];
                double[] B = new double[numEq];
                string[] eqDescriptions = new string[numEq];

                // 1. Умови проходження через вузли (2n рівнянь)
                int row = 0;
                for (int i = 0; i < n; i++)
                {
                    int colOffset = 4 * i;
                    double xL = X[i];
                    A[row, colOffset + 0] = Math.Pow(xL, 3);
                    A[row, colOffset + 1] = Math.Pow(xL, 2);
                    A[row, colOffset + 2] = xL;
                    A[row, colOffset + 3] = 1;
                    B[row] = Y[i];
                    eqDescriptions[row] = $"S_{i+1}({xL}) = {Y[i]} => {A[row, colOffset + 0]}*A_{i+1} + {A[row, colOffset + 1]}*B_{i+1} + {A[row, colOffset + 2]}*C_{i+1} + D_{i+1} = {Y[i]}";
                    row++;

                    double xR = X[i + 1];
                    A[row, colOffset + 0] = Math.Pow(xR, 3);
                    A[row, colOffset + 1] = Math.Pow(xR, 2);
                    A[row, colOffset + 2] = xR;
                    A[row, colOffset + 3] = 1;
                    B[row] = Y[i + 1];
                    eqDescriptions[row] = $"S_{i+1}({xR}) = {Y[i + 1]} => {A[row, colOffset + 0]}*A_{i+1} + {A[row, colOffset + 1]}*B_{i+1} + {A[row, colOffset + 2]}*C_{i+1} + D_{i+1} = {Y[i + 1]}";
                    row++;
                }

                // 2. Рівність перших похідних у внутрішніх вузлах (n - 1 рівнянь)
                for (int i = 0; i < n - 1; i++)
                {
                    double xNode = X[i + 1];
                    int colOffset1 = 4 * i;
                    int colOffset2 = 4 * (i + 1);

                    A[row, colOffset1 + 0] = 3 * Math.Pow(xNode, 2);
                    A[row, colOffset1 + 1] = 2 * xNode;
                    A[row, colOffset1 + 2] = 1;

                    A[row, colOffset2 + 0] = -3 * Math.Pow(xNode, 2);
                    A[row, colOffset2 + 1] = -2 * xNode;
                    A[row, colOffset2 + 2] = -1;

                    B[row] = 0;
                    eqDescriptions[row] = $"S'_{i+1}({xNode}) = S'_{i+2}({xNode}) => {A[row, colOffset1 + 0]}*A_{i+1} + {A[row, colOffset1 + 1]}*B_{i+1} + C_{i+1} - {Math.Abs(A[row, colOffset2 + 0])}*A_{i+2} - {Math.Abs(A[row, colOffset2 + 1])}*B_{i+2} - C_{i+2} = 0";
                    row++;
                }

                // 3. Рівність других похідних у внутрішніх вузлах (n - 1 рівнянь)
                for (int i = 0; i < n - 1; i++)
                {
                    double xNode = X[i + 1];
                    int colOffset1 = 4 * i;
                    int colOffset2 = 4 * (i + 1);

                    A[row, colOffset1 + 0] = 6 * xNode;
                    A[row, colOffset1 + 1] = 2;

                    A[row, colOffset2 + 0] = -6 * xNode;
                    A[row, colOffset2 + 1] = -2;

                    B[row] = 0;
                    eqDescriptions[row] = $"S''_{i+1}({xNode}) = S''_{i+2}({xNode}) => {A[row, colOffset1 + 0]}*A_{i+1} + 2*B_{i+1} - {Math.Abs(A[row, colOffset2 + 0])}*A_{i+2} - 2*B_{i+2} = 0";
                    row++;
                }

                // 4. Природні крайові умови (2 рівняння)
                double xStart = X[0];
                A[row, 0] = 6 * xStart;
                A[row, 1] = 2;
                B[row] = 0;
                eqDescriptions[row] = $"S''_1({xStart}) = 0 => {A[row, 0]}*A_1 + 2*B_1 = 0";
                row++;

                double xEnd = X[X.Length - 1];
                int lastSplineOffset = 4 * (n - 1);
                A[row, lastSplineOffset + 0] = 6 * xEnd;
                A[row, lastSplineOffset + 1] = 2;
                B[row] = 0;
                eqDescriptions[row] = $"S''_{n}({xEnd}) = 0 => {A[row, lastSplineOffset + 0]}*A_{n} + 2*B_{n} = 0";
                row++;

                // Виведення списку рівнянь
                output.AppendLine($"Крок 1: Складання системи з {numEq} рівнянь:");
                for (int i = 0; i < numEq; i++)
                {
                    output.AppendLine($"{i + 1,2}) {eqDescriptions[i]}");
                }
                output.Append('\n');

                // Виведення матриці системи
                output.AppendLine("Крок 2: Розширена матриця системи [A | b]:");
                output.Append(" №|");
                for (int i = 0; i < n; i++)
                {
                    output.Append($"{ "A" + (i + 1),5}");
                    output.Append($"{ "B" + (i + 1),5}");
                    output.Append($"{ "C" + (i + 1),5}");
                    output.Append($"{ "D" + (i + 1),5}");
                }
                output.AppendLine(" |  b");
                output.AppendLine(new string('-', 3 + 5 * numEq + 5));
                for (int i = 0; i < numEq; i++)
                {
                    output.Append($"{i + 1,2}|");
                    for (int j = 0; j < numEq; j++)
                    {
                        output.Append($"{A[i, j],5:0}");
                    }
                    output.AppendLine($" | {B[i],2}");
                }
                output.Append('\n');

                // Розв'язання системи методом Гаусса
                double[] sol = GaussForwardPass.Solve(A, B);

                // Отримання коефіцієнтів
                output.AppendLine("Крок 3: Результати розв'язання СЛАР (коефіцієнти сплайнів):");
                for (int i = 0; i < n; i++)
                {
                    int offset = 4 * i;
                    output.AppendLine($"Інтервал {i + 1} [{X[i]}; {X[i + 1]}]:");
                    output.AppendLine($"  A{i + 1} = {sol[offset + 0]:F6}");
                    output.AppendLine($"  B{i + 1} = {sol[offset + 1]:F6}");
                    output.AppendLine($"  C{i + 1} = {sol[offset + 2]:F6}");
                    output.AppendLine($"  D{i + 1} = {sol[offset + 3]:F6}");
                }
                output.Append('\n');

                // Виведення аналітичного вигляду сплайнів
                output.AppendLine("Крок 4: Формули сплайн-функцій по інтервалах:");
                for (int i = 0; i < n; i++)
                {
                    int offset = 4 * i;
                    string aSign = sol[offset + 0] >= 0 ? "" : "- ";
                    string bSign = sol[offset + 1] >= 0 ? "+ " : "- ";
                    string cSign = sol[offset + 2] >= 0 ? "+ " : "- ";
                    string dSign = sol[offset + 3] >= 0 ? "+ " : "- ";

                    output.AppendLine($"S_{i + 1}(x) = {aSign}{Math.Abs(sol[offset + 0]):F4}*x^3 {bSign}{Math.Abs(sol[offset + 1]):F4}*x^2 {cSign}{Math.Abs(sol[offset + 2]):F4}*x {dSign}{Math.Abs(sol[offset + 3]):F4}  на [{X[i]}; {X[i + 1]}]");
                }
                output.Append('\n');

                // Обчислення значення у точці xTarget
                int targetIntervalIndex = -1;
                for (int i = 0; i < n; i++)
                {
                    if (xTarget >= X[i] && xTarget <= X[i + 1])
                    {
                        targetIntervalIndex = i;
                        break;
                    }
                }

                if (targetIntervalIndex == -1)
                {
                    throw new InvalidOperationException($"Точка x* = {xTarget} лежить поза межами заданої області інтерполяції.");
                }

                int sOffset = 4 * targetIntervalIndex;
                double a_val = sol[sOffset + 0];
                double b_val = sol[sOffset + 1];
                double c_val = sol[sOffset + 2];
                double d_val = sol[sOffset + 3];

                double term3 = a_val * Math.Pow(xTarget, 3);
                double term2 = b_val * Math.Pow(xTarget, 2);
                double term1 = c_val * xTarget;
                double term0 = d_val;
                double splineValue = term3 + term2 + term1 + term0;

                output.AppendLine($"Крок 5: Обчислення наближеного значення у точці x* = {xTarget}:");
                output.AppendLine($"Оскільки x* = {xTarget} належить інтервалу [{X[targetIntervalIndex]}; {X[targetIntervalIndex + 1]}], використовуємо формулу S_{targetIntervalIndex + 1}(x):");
                output.AppendLine($"S_{targetIntervalIndex + 1}({xTarget}) = A_{targetIntervalIndex + 1}*({xTarget})^3 + B_{targetIntervalIndex + 1}*({xTarget})^2 + C_{targetIntervalIndex + 1}*({xTarget}) + D_{targetIntervalIndex + 1}");
                output.AppendLine($"S_{targetIntervalIndex + 1}({xTarget}) = {a_val:F6}*{Math.Pow(xTarget, 3)} + {b_val:F6}*{Math.Pow(xTarget, 2)} + {c_val:F6}*{xTarget} + {d_val:F6}");
                output.AppendLine($"S_{targetIntervalIndex + 1}({xTarget}) = {term3:F4} + ({term2:F4}) + ({term1:F4}) + ({term0:F4})");
                output.AppendLine($"S({xTarget}) ≈ {splineValue:F4}\n");

                output.AppendLine("Висновок:");
                output.AppendLine("У ході лабораторної роботи було досліджено метод побудови кубічних сплайнів третього порядку.");
                output.AppendLine($"Для заданих вузлів з відповідними значеннями було динамічно складено систему з {numEq} лінійних рівнянь");
                output.AppendLine("та знайдено коефіцієнти кубічних поліномів за допомогою методу Гаусса.");
                output.AppendLine($"У точці інтерполяції x* = {xTarget} отримано значення S({xTarget}) ≈ {splineValue:F4}, що забезпечує");
                output.AppendLine("гладку інтерполяційну криву на всьому заданому відрізку.");
            }
            catch (Exception ex)
            {
                output.AppendLine($"Помилка при виконанні: {ex.Message}");
                output.AppendLine(ex.StackTrace);
            }

            return output.ToString();
        }
    }
}