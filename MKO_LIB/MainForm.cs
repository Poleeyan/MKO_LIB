using System;
using System.Windows.Forms;

namespace MKO_LIB
{
    public partial class MainForm : Form
    {
        private TextBox? resultsTextBox;
        private ComboBox? labComboBox;
        private Button? runLabButton;
        private Button? clearButton;

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
            // Lab ComboBox
            labComboBox = new ComboBox();
            labComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            labComboBox.Items.AddRange(new object[] { "Lab1", "Lab2", "Lab3", "Lab4", "Lab5", "Lab6", "Lab7", "Lab8", "Coursework" });
            labComboBox.SelectedIndex = 0;
            labComboBox.Location = new System.Drawing.Point(50, 20);
            labComboBox.Size = new System.Drawing.Size(150, 30);
            this.Controls.Add(labComboBox);

            // Run Lab Button
            runLabButton = new Button();
            runLabButton.Text = "Run Selected Lab";
            runLabButton.Location = new System.Drawing.Point(210, 18);
            runLabButton.Size = new System.Drawing.Size(120, 28);
            runLabButton.Click += RunLabButton_Click;
            this.Controls.Add(runLabButton);

            // Clear Button
            clearButton = new Button();
            clearButton.Text = "Clear";
            clearButton.Location = new System.Drawing.Point(340, 18);
            clearButton.Size = new System.Drawing.Size(100, 28);
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

        private void RunLabButton_Click(object? sender, EventArgs e)
        {
            try
            {
                if (resultsTextBox == null || labComboBox == null || labComboBox.SelectedItem == null)
                    return;
                Dictionary<string, Func<string>> labMethods = new Dictionary<string, Func<string>>()
                {
                    { "Lab1", Lab1.Run },
                    { "Lab2", Lab2.Run },
                    { "Lab3", Lab3.Run },
                    { "Lab4", Lab4.Run },
                    { "Lab5", Lab5.Run },
                    { "Lab6", Lab6.Run },
                    { "Lab7", Lab7.Run },
                    { "Lab8", Lab8.Run },
                    { "Coursework", Coursework.Run }
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
    }
}
