using System;
using System.Globalization;
using System.Windows.Forms;

namespace MKO_LIB
{
    public partial class MainForm : Form
    {
        private TextBox? resultsTextBox;
        private ComboBox? labComboBox;
        private Button? modeLabButton;
        private Button? modeCourseworkButton;
        private Button? runLabButton;
        private Button? clearButton;
        
        // Fields for coursework
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
            this.Text = "Equation Solvers (Labs)";
            this.Width = 700;
            this.Height = 600;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void InitializeComponent()
        {
            // Mode Lab Button
            modeLabButton = new Button();
            modeLabButton.Text = "Run Lab";
            modeLabButton.Location = new System.Drawing.Point(50, 20);
            modeLabButton.Size = new System.Drawing.Size(120, 30);
            modeLabButton.Click += ModeLabButton_Click;
            this.Controls.Add(modeLabButton);

            // Mode Coursework Button
            modeCourseworkButton = new Button();
            modeCourseworkButton.Text = "Run Coursework";
            modeCourseworkButton.Location = new System.Drawing.Point(180, 20);
            modeCourseworkButton.Size = new System.Drawing.Size(130, 30);
            modeCourseworkButton.Click += ModeCourseworkButton_Click;
            this.Controls.Add(modeCourseworkButton);

            // Clear Button
            clearButton = new Button();
            clearButton.Text = "Clear";
            clearButton.Location = new System.Drawing.Point(320, 20);
            clearButton.Size = new System.Drawing.Size(100, 30);
            clearButton.Click += ClearButton_Click;
            this.Controls.Add(clearButton);

            // Run Lab Button
            runLabButton = new Button();
            runLabButton.Text = "Run";
            runLabButton.Location = new System.Drawing.Point(210, 58);
            runLabButton.Size = new System.Drawing.Size(80, 26);
            runLabButton.Click += RunLabButton_Click;
            this.Controls.Add(runLabButton);

            // Lab ComboBox
            labComboBox = new ComboBox();
            labComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            labComboBox.Items.AddRange(new object[] { "Lab1", "Lab2", "Lab3", "Lab4", "Lab5", "Lab6", "Lab7", "Lab8" });
            labComboBox.Location = new System.Drawing.Point(50, 60);
            labComboBox.Size = new System.Drawing.Size(150, 30);
            this.Controls.Add(labComboBox);
            labComboBox.SelectedIndex = 0;

            // Coursework Labels & Inputs
            aLabel = new Label() { Text = "a=", Location = new System.Drawing.Point(50, 63), Size = new System.Drawing.Size(30, 20) };
            aInput = new TextBox() { Text = "1.0", Location = new System.Drawing.Point(80, 60), Size = new System.Drawing.Size(40, 20) };
            
            bLabel = new Label() { Text = "b=", Location = new System.Drawing.Point(130, 63), Size = new System.Drawing.Size(30, 20) };
            bInput = new TextBox() { Text = "2.0", Location = new System.Drawing.Point(160, 60), Size = new System.Drawing.Size(40, 20) };
            
            epsLabel = new Label() { Text = "eps=", Location = new System.Drawing.Point(210, 63), Size = new System.Drawing.Size(40, 20) };
            epsInput = new TextBox() { Text = "0.01", Location = new System.Drawing.Point(250, 60), Size = new System.Drawing.Size(50, 20) };
            
            x0Label = new Label() { Text = "x0=", Location = new System.Drawing.Point(310, 63), Size = new System.Drawing.Size(30, 20) };
            x0Input = new TextBox() { Text = "2.0", Location = new System.Drawing.Point(340, 60), Size = new System.Drawing.Size(40, 20) };

            getButton = new Button();
            getButton.Text = "Get";
            getButton.Location = new System.Drawing.Point(400, 58);
            getButton.Size = new System.Drawing.Size(80, 26);
            getButton.Click += GetButton_Click;

            this.Controls.Add(aLabel);
            this.Controls.Add(aInput);
            this.Controls.Add(bLabel);
            this.Controls.Add(bInput);
            this.Controls.Add(epsLabel);
            this.Controls.Add(epsInput);
            this.Controls.Add(x0Label);
            this.Controls.Add(x0Input);
            this.Controls.Add(getButton);

            // Initially hide coursework controls
            ToggleCourseworkControls(false);

            // Results TextBox
            resultsTextBox = new TextBox();
            resultsTextBox.Multiline = true;
            resultsTextBox.ReadOnly = true;
            resultsTextBox.ScrollBars = ScrollBars.Both;
            resultsTextBox.WordWrap = true;
            resultsTextBox.Location = new System.Drawing.Point(50, 100);
            resultsTextBox.Size = new System.Drawing.Size(600, 430);
            resultsTextBox.Font = new System.Drawing.Font("Courier New", 10);
            this.Controls.Add(resultsTextBox);
        }

        private void ToggleCourseworkControls(bool show)
        {
            if (labComboBox != null) labComboBox.Visible = !show;
            if (runLabButton != null) runLabButton.Visible = !show;
            if (aLabel != null) aLabel.Visible = show;
            if (aInput != null) aInput.Visible = show;
            if (bLabel != null) bLabel.Visible = show;
            if (bInput != null) bInput.Visible = show;
            if (epsLabel != null) epsLabel.Visible = show;
            if (epsInput != null) epsInput.Visible = show;
            if (x0Label != null) x0Label.Visible = show;
            if (x0Input != null) x0Input.Visible = show;
            if (getButton != null) getButton.Visible = show;
        }

        private void ModeLabButton_Click(object? sender, EventArgs e)
        {
            ToggleCourseworkControls(false);
            if (resultsTextBox != null) resultsTextBox.Text = string.Empty;
            if (modeLabButton != null) modeLabButton.FlatStyle = FlatStyle.Flat;
            if (modeCourseworkButton != null) modeCourseworkButton.FlatStyle = FlatStyle.Standard;
        }

        private void ModeCourseworkButton_Click(object? sender, EventArgs e)
        {
            ToggleCourseworkControls(true);
            if (resultsTextBox != null) resultsTextBox.Text = string.Empty;
            if (modeLabButton != null) modeLabButton.FlatStyle = FlatStyle.Standard;
            if (modeCourseworkButton != null) modeCourseworkButton.FlatStyle = FlatStyle.Flat;
        }

        private void RunLabButton_Click(object? sender, EventArgs e)
        {
            try
            {
                if (resultsTextBox == null || labComboBox == null || labComboBox.SelectedItem == null)
                    return;
                resultsTextBox.Text = string.Empty;
                Dictionary<string, Func<string>> labMethods = new Dictionary<string, Func<string>>()
                {
                    { "Lab1", Lab1.Run },
                    { "Lab2", Lab2.Run },
                    { "Lab3", Lab3.Run },
                    { "Lab4", Lab4.Run },
                    { "Lab5", Lab5.Run },
                    { "Lab6", Lab6.Run },
                    { "Lab7", Lab7.Run },
                    { "Lab8", Lab8.Run }
                };
                string selectedLab = labComboBox.SelectedItem.ToString() ?? "";
                if (labMethods.ContainsKey(selectedLab))
                {
                    resultsTextBox.Text = labMethods[selectedLab]();
                }
                else
                {
                    resultsTextBox.Text = "Lab not implemented or selected properly.";
                }
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
            // Замінюємо кому на крапку, щоб працювало і з точкою, і з комою
            raw = raw.Replace(',', '.');
            return double.TryParse(raw, NumberStyles.Float, CultureInfo.InvariantCulture, out value);
        }
    }
}
