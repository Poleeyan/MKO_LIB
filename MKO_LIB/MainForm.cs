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
            labComboBox.Items.AddRange(new object[] { "Lab2", "Lab3", "Lab4", "Lab5", "Lab6", "Coursework" });
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
                
                string selectedLab = labComboBox.SelectedItem.ToString() ?? "";
                switch (selectedLab)
                {
                    case "Lab2":
                        resultsTextBox.Text = Lab2.Run();
                        break;
                    case "Lab3":
                        resultsTextBox.Text = Lab3.Run();
                        break;
                    case "Lab4":
                        resultsTextBox.Text = Lab4.Run();
                        break;
                    case "Lab5":
                        resultsTextBox.Text = Lab5.Run();
                        break;
                    case "Lab6":
                        resultsTextBox.Text = Lab6.Run();
                        break;
                    case "Coursework":
                        resultsTextBox.Text = Coursework.Run();
                        break;
                    default:
                        resultsTextBox.Text = "Lab not implemented or selected properly.";
                        break;
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
