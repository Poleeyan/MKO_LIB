using System;
using System.Windows.Forms;

namespace MKO_LIB
{
    public partial class MainForm : Form
    {
        private TextBox? resultsTextBox;
        private Button? runButton;
        private Button? clearButton;

        public MainForm()
        {
            InitializeComponent();
            this.Text = "Lab2 - Equation Solver";
            this.Width = 700;
            this.Height = 600;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void InitializeComponent()
        {
            // Run Button
            runButton = new Button();
            runButton.Text = "Run Lab2";
            runButton.Location = new System.Drawing.Point(50, 20);
            runButton.Size = new System.Drawing.Size(100, 40);
            runButton.Click += RunButton_Click;
            this.Controls.Add(runButton);

            // Clear Button
            clearButton = new Button();
            clearButton.Text = "Clear";
            clearButton.Location = new System.Drawing.Point(200, 20);
            clearButton.Size = new System.Drawing.Size(100, 40);
            clearButton.Click += ClearButton_Click;
            this.Controls.Add(clearButton);

            // Results TextBox
            resultsTextBox = new TextBox();
            resultsTextBox.Multiline = true;
            resultsTextBox.ReadOnly = true;
            resultsTextBox.ScrollBars = ScrollBars.Both;
            resultsTextBox.WordWrap = true;
            resultsTextBox.Location = new System.Drawing.Point(50, 80);
            resultsTextBox.Size = new System.Drawing.Size(600, 450);
            resultsTextBox.Font = new System.Drawing.Font("Courier New", 10);
            this.Controls.Add(resultsTextBox);
        }

        private void RunButton_Click(object? sender, EventArgs e)
        {
            try
            {
                if (resultsTextBox != null)
                    resultsTextBox.Text = RunLab2();
            }
            catch (Exception ex)
            {
                if (resultsTextBox != null)
                    resultsTextBox.Text = $"Error: {ex.Message}\n\n{ex.StackTrace}";
            }
        }

        private void ClearButton_Click(object? sender, EventArgs e)
        {
            if (resultsTextBox != null)
                resultsTextBox.Text = string.Empty;
        }

        private string RunLab2()
        {
            var output = new System.Text.StringBuilder();

            // Функція f(x) = x + ln(x) = 0
            // При a₁ = a₂ = a₃ = 1: a₁*x + a₂*ln(a₃*x) = 0 => x + ln(x) = 0
            Func<double, double> f = x =>
            {
                if (x <= 0) throw new ArgumentException("x повинен бути > 0 для ln(x)");
                return x + Math.Log(x);
            };

            // Параметри для методу бісекції
            output.AppendLine("=== Розв'язання рівняння: x + ln(x) = 0 ===");
            output.AppendLine("Де: a₁ = a₂ = a₃ = 1\n");

            output.AppendLine("=== Метод бісекції ===");
            double a = 0.1;      // ліва межа
            double b = 1.0;      // права межа
            double delta = 0.0001; // точність

            output.AppendLine($"a = {a}, b = {b}, delta = {delta}");

            try
            {
                // Виклик методу бісекції
                var solver = new BisectionMethod(f);
                var result = solver.Solve(a, b, delta);

                // Вивід результату
                output.AppendLine("\n=== Результат (Метод бісекції) ===");
                output.AppendLine($"Корінь x ≈ {result.Root:F10}");
                output.AppendLine($"f({result.Root:F10}) ≈ {result.FunctionValue:F10}");
                output.AppendLine($"Кількість ітерацій: {result.Iterations}");
                output.AppendLine($"Остаточна точність: {result.Precision:F10}");
            }
            catch (ArgumentException ex)
            {
                output.AppendLine($"Помилка: {ex.Message}");
            }

            // Метод Ньютона
            output.AppendLine("\n=== Метод Ньютона ===");

            double h = 0.001;    // крок для числової похідної
            double x = 0.5;      // початкове наближення
            double deltaNm = 0.0001; // точність

            output.AppendLine($"x₀ = {x}, h = {h}, delta = {deltaNm}");

            try
            {
                // Виклик методу Ньютона (січних - з числовою похідною)
                var newtonSolver = new NewtonMethod(f);
                var newtonResult = newtonSolver.SolveSecant(x, h, deltaNm);

                // Вивід результату
                output.AppendLine("\n=== Результат (Метод Ньютона - січних) ===");
                output.AppendLine($"Корінь x ≈ {newtonResult.Root:F10}");
                output.AppendLine($"f({newtonResult.Root:F10}) ≈ {newtonResult.FunctionValue:F10}");
                output.AppendLine($"Кількість ітерацій: {newtonResult.Iterations}");
                output.AppendLine($"Остаточна точність: {newtonResult.Precision:F10}");
            }
            catch (Exception ex)
            {
                output.AppendLine($"Помилка: {ex.Message}");
            }

            return output.ToString();
        }
    }
}
