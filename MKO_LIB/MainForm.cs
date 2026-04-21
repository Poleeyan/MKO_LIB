using System;
using System.Windows.Forms;

namespace MKO_LIB
{
    public partial class MainForm : Form
    {
        private TextBox? resultsTextBox;
        private Button? runLab2Button;
        private Button? runLab3Button;
        private Button? clearButton;

        public MainForm()
        {
            InitializeComponent();
            this.Text = "Equation Solvers (Lab2 & Lab3)";
            this.Width = 700;
            this.Height = 600;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void InitializeComponent()
        {
            // Run Lab2 Button
            runLab2Button = new Button();
            runLab2Button.Text = "Run Lab2";
            runLab2Button.Location = new System.Drawing.Point(50, 20);
            runLab2Button.Size = new System.Drawing.Size(100, 40);
            runLab2Button.Click += RunLab2Button_Click;
            this.Controls.Add(runLab2Button);

            // Run Lab3 Button
            runLab3Button = new Button();
            runLab3Button.Text = "Run Lab3";
            runLab3Button.Location = new System.Drawing.Point(160, 20);
            runLab3Button.Size = new System.Drawing.Size(100, 40);
            runLab3Button.Click += RunLab3Button_Click;
            this.Controls.Add(runLab3Button);

            // Clear Button
            clearButton = new Button();
            clearButton.Text = "Clear";
            clearButton.Location = new System.Drawing.Point(270, 20);
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

        private void RunLab2Button_Click(object? sender, EventArgs e)
        {
            try
            {
                if (resultsTextBox != null)
                    resultsTextBox.Text = Lab2.Run();
            }
            catch (Exception ex)
            {
                if (resultsTextBox != null)
                    resultsTextBox.Text = $"Error: {ex.Message}\n\n{ex.StackTrace}";
            }
        }

        private void RunLab3Button_Click(object? sender, EventArgs e)
        {
            try
            {
                if (resultsTextBox != null)
                    resultsTextBox.Text = Lab3.Run();
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
