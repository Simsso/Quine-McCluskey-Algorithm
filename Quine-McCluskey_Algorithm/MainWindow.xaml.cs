using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace Quine_McCluskey_Algorithm
{
    public partial class MainWindow : Window
    {
        Control control;

        public MainWindow()
        {
            InitializeComponent();

            this.TextBox_Input.Text =
@"C B A X
0 0 0 1
0 0 1 1
0 1 0 1
0 1 1 0
1 0 0 0
1 0 1 1
1 1 0 1
1 1 1 1";

            this.control = new Control(this);
        }

        private void Button_LoadTruthTable_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == true) 
            {
                TextBox_Input.Text = File.ReadAllText(ofd.FileName);
            }
        }

        public void SetOutputLabelText(string text)
        {
            TextBox_Output.Text = text;
        }

        public void SetOutputEquationLabelText(string text)
        {
            TextBox_OutputEquation.Text = text;
        }

        private void Button_Minimize_Click(object sender, RoutedEventArgs e)
        {
            control.ProcessTruthTableString(TextBox_Input.Text);
        }


    }
}
