using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Converters;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.IO;

namespace Prac01
{
    /// <summary>
    /// Логика взаимодействия для CheckMode.xaml
    /// </summary>
    public partial class CheckMode : Window
    {
        Stopwatch SW;
        int i;
        double[] Y = new double[8];
        int TryDone;
        int TryMax;
        List<double> References = new List<double>();
        List<double> Results = new List<double>();
        List<double> NewResults = new List<double>();
        double Sref;
        double Saut;
        string[] Refs;
        double Tt = 2.36;
        double Ft = 3.41;
        int k;
        double[,] RefsMatrix;

        public CheckMode()
        {
            InitializeComponent();

            StreamReader SR = new StreamReader("references.txt");

            while (!SR.EndOfStream)
                Refs = SR.ReadLine().Split(' ');

            foreach (string Ref in Refs)
                try
                {
                    References.Add(Convert.ToDouble(Ref));
                }
                catch { }

            double[] R = new double[References.Count];

            k = References.Count / 8;

            RefsMatrix = new double[8, k];

            for(int i = 0; i < R.Length; i++)
            {
                R[i] = Convert.ToDouble(References[i]);
            }
 
            References = Calculate(R);

            Sref = FindDispersion(References);

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MW = new MainWindow();
            MW.Show();
            Hide();
        }

        private void EnterField_TextChanged(object sender, TextChangedEventArgs e)
        {
            CharCount.Content = EnterField.Text.Length;
            if (i == -1)
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
                Y[i - 1] = ts.Seconds + (ts.Milliseconds * 0.001);
                i++;
                SW = new Stopwatch();
                SW.Start();
            }
            else
            {
                SW.Stop();
                TimeSpan ts = SW.Elapsed;
                Y[i - 1] = ts.Seconds + (ts.Milliseconds * 0.001);
                i = -1;
                if (EnterField.Text == "длагнитор")
                {

                    MessageBox.Show("текст вірний");
                    EnterField.Text = "";
                    TryDone++;
                    NewResults = Calculate(Y);
                    foreach (double R in NewResults)
                        Results.Add(R);
                    // for(int i = 0; i < References.Count;i++)
                    // ilabel.Content += " " +  References[i].ToString(); 
                    if (TryDone == TryMax)
                    {
                        EnterField.IsEnabled = false;
                        Saut = FindDispersion(Results);
                        FindResults();
                    }

                }
                else
                {
                    MessageBox.Show("Невірний текст");
                    EnterField.Text = "";

                }

            }
        }

        private double FindDispersion(List<double> list)
        {
            double S;
            double sum1 = 0;
            double sum2 = 0;
            for (int i = 0; i < list.Count; i++)
            {
                sum1 += list[i] * list[i];
                sum2 += list[i];
            }
            sum1 = sum1 / list.Count;
            sum2 = sum2 / list.Count;
            sum2 = sum2 * sum2;
            S = sum1 - sum2;

            return S;
        }
        private List<double> Calculate(double[] Y)
        {
            List<double> list = new List<double>();
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
                    list.Add(Y[i]);
                }
            }
            return list;
        }
        private void FindResults()
        {
            double Fp;
            if (Sref > Saut)
                Fp = Sref / Saut;
            else
                Fp = Saut / Sref;

            if (Fp > Ft)
                DispTypeLabel.Content = "Неоднорідна";
            else
                DispTypeLabel.Content = "рівна";




        }

        private void Tries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TryMax = Tries.SelectedIndex + 1;
            k = Tries.SelectedIndex + 1;
        }
    }
}
