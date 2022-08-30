using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;


namespace Prac01
{
    /// <summary>
    /// Логика взаимодействия для StudyMode.xaml
    /// </summary>
    public partial class StudyMode : Window
    {
        Stopwatch SW = new Stopwatch();
        double[] Y = new double[8];
        int i;
        int TryMax;
        int TryDone;
        List<double> References = new List<double>();
        double Tt = 2.36;
        public StudyMode()
        {
            InitializeComponent();

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter SW = new StreamWriter("references.txt");

            for (int i = 0; i < References.Count; i++)
                SW.Write(References[i] + " ");
            SW.Close();
            MainWindow Mw = new MainWindow();
            Mw.Show();
            Hide();
        }
        private void Calculate()
        {
            double[] M = new double[Y.Count()];
            double[] S = new double[Y.Count()];
            double sumk = 0;
            double sum2 = 0;
            double tp;

            for (int i = 0; i < Y.Count(); i++)
            {

                for (int k = 0; k < Y.Count(); k++)
                {
                    sumk = 0;
                    sum2 = 0;
                    if (k != i)
                    {
                        sumk += Y[k];
                    }
                }
                M[i] = sumk / 7;

                for (int k = 0; k < Y.Count(); k++)
                {
                    if (k != i)
                    {
                        sum2 += Y[k] - M[i];
                    }
                }
                S[i] = sum2 / 7;

                tp = Math.Abs((Y[i] - M[i]) / (Math.Sqrt(S[i]) / Math.Sqrt(7)));
                if (tp < Tt)
                {
                    References.Add(Y[i]);
                }
            }
        
        
    }

        private void EnterField_TextChanged(object sender, TextChangedEventArgs e)
        {
            CharCount.Content = EnterField.Text.Length; 
            ilabel.Content = TryDone;
            if(i == -1)
            {
                i++;
            }
            else if (i == 0)
            {
                SW = new Stopwatch();
                SW.Start();
                i++;
            }
            else if (i < 8 && EnterField.Text != "длагнитор")
            {
                SW.Stop();
                TimeSpan ts = SW.Elapsed;
                Y[i-1] = ts.Seconds + (ts.Milliseconds * 0.001);
                CheckLabel.Content += (Y[i-1].ToString() + " ");
                i++;
                SW = new Stopwatch();
                SW.Start();
            }
            else
            {
                SW.Stop();
                TimeSpan ts = SW.Elapsed;
                Y[i - 1] = ts.Seconds + (ts.Milliseconds * 0.001);
                CheckLabel.Content += (Y[i - 1].ToString() + " ");
                i = -1;
                if (EnterField.Text == "длагнитор")
                {

                    CheckLabel.Content += "\n";
                    MessageBox.Show("текст вірний");
                    EnterField.Text = "";
                    TryDone++;
                    for(int j = 0; j < Y.Length; j++)
                    References.Add(Y[j]);
                    // for(int i = 0; i < References.Count;i++)
                    // ilabel.Content += " " +  References[i].ToString(); 
                    if (TryDone == TryMax)
                    {
                        EnterField.IsEnabled = false;
                      
                    }

                }
                else
                {
                    MessageBox.Show("Невірний текст");
                    EnterField.Text = "";

                }

            }

        }

        private void EnterField_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            // i = 0;
            //EnterField.Text = "";
            //CheckLabel.Content += "\n";
            ilabel.Content = "";
            foreach (double D in References)
                ilabel.Content += " " + D;
        }

        private void TriesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TryMax = TriesList.SelectedIndex + 1;
        }
        
    }
}
