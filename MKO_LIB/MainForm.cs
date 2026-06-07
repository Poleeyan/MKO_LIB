using System.Globalization;

namespace MKO_LIB
{
    public partial class MainForm : Form
    {
        private TextBox? resultsTextBox;
        private Button? clearButton;
        private Button? getButton;
        private TextBox? aInput;
        private TextBox? bInput;
        private TextBox? epsInput;
        private TextBox? x0Input;
        private Label? aLabel;
        private Label? bLabel;
        private Label? epsLabel;
        private Label? x0Label;

        public MainForm()
        {
            InitializeComponent();
            this.Text = "Equation Solvers (Coursework)";
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void InitializeComponent()
        {
            this.Width = 700;
            this.Height = 600;

            aLabel = new Label() { Text = "a=", Location = new System.Drawing.Point(50, 30), Size = new System.Drawing.Size(30, 20) };
            aInput = new TextBox() { Text = "1.0", Location = new System.Drawing.Point(80, 27), Size = new System.Drawing.Size(40, 20) };
            
            bLabel = new Label() { Text = "b=", Location = new System.Drawing.Point(130, 30), Size = new System.Drawing.Size(30, 20) };
            bInput = new TextBox() { Text = "2.0", Location = new System.Drawing.Point(160, 27), Size = new System.Drawing.Size(40, 20) };
            
            epsLabel = new Label() { Text = "eps=", Location = new System.Drawing.Point(210, 30), Size = new System.Drawing.Size(40, 20) };
            epsInput = new TextBox() { Text = "0.01", Location = new System.Drawing.Point(250, 27), Size = new System.Drawing.Size(50, 20) };
            
            x0Label = new Label() { Text = "x0=", Location = new System.Drawing.Point(310, 30), Size = new System.Drawing.Size(30, 20) };
            x0Input = new TextBox() { Text = "2.0", Location = new System.Drawing.Point(340, 27), Size = new System.Drawing.Size(40, 20) };

            getButton = new Button();
            getButton.Text = "Get";
            getButton.Location = new System.Drawing.Point(400, 25);
            getButton.Size = new System.Drawing.Size(80, 26);
            getButton.Click += GetButton_Click;

            clearButton = new Button();
            clearButton.Text = "Clear";
            clearButton.Location = new System.Drawing.Point(560, 25);
            clearButton.Size = new System.Drawing.Size(90, 26);
            clearButton.Click += ClearButton_Click;

            this.Controls.Add(aLabel);
            this.Controls.Add(aInput);
            this.Controls.Add(bLabel);
            this.Controls.Add(bInput);
            this.Controls.Add(epsLabel);
            this.Controls.Add(epsInput);
            this.Controls.Add(x0Label);
            this.Controls.Add(x0Input);
            this.Controls.Add(getButton);
            this.Controls.Add(clearButton);

            resultsTextBox = new TextBox();
            resultsTextBox.Multiline = true;
            resultsTextBox.ReadOnly = true;
            resultsTextBox.ScrollBars = ScrollBars.Both;
            resultsTextBox.WordWrap = true;
            resultsTextBox.Location = new System.Drawing.Point(50, 75);
            resultsTextBox.Size = new System.Drawing.Size(600, 455);
            resultsTextBox.Font = new System.Drawing.Font("Courier New", 10);
            resultsTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(resultsTextBox);
        }

        private void ClearButton_Click(object? sender, EventArgs e)
        {
            if (resultsTextBox != null)
                resultsTextBox.Text = string.Empty;
        }

        private void GetButton_Click(object? sender, EventArgs e)
        {
            try
            {
                if (resultsTextBox == null) return;
                resultsTextBox.Text = string.Empty;

                if (!TryParseInput(aInput, out double a) ||
                    !TryParseInput(bInput, out double b) ||
                    !TryParseInput(epsInput, out double eps) ||
                    !TryParseInput(x0Input, out double x0))
                {
                    MessageBox.Show("Помилка вводу: будь ласка, введіть коректні числові значення. Букви та інші спеціальні символи не допускаються.", "Некоректне значення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (a >= b)
                {
                    MessageBox.Show("Некоректний інтервал: значення 'a' повинно бути менше за значення 'b'.", "Помилка інтервалу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (eps <= 0)
                {
                    MessageBox.Show("Некоректна похибка: 'eps' повинно бути більше за 0.", "Помилка похибки", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                resultsTextBox.Text = Coursework.Run(a, b, eps, x0);
            }
            catch (Exception ex)
            {
                if (resultsTextBox != null)
                    resultsTextBox.Text = $"Error: {ex.Message}\n\n{ex.StackTrace}";
            }
        }

        private static bool TryParseInput(TextBox? input, out double value)
        {
            string raw = input?.Text ?? string.Empty;
            raw = raw.Replace(',', '.');
            return double.TryParse(raw, NumberStyles.Float, CultureInfo.InvariantCulture, out value);
        }
    }
}

