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

namespace Lab01
{
    /// <summary>
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        double Num;
        byte Operation;
        public Window3()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Hide();
            mw.Show();
        }

        private void _1_Click(object sender, RoutedEventArgs e)
        {
           CNumber.Content += "1";
        }

        private void _2_Click(object sender, RoutedEventArgs e)
        {
            CNumber.Content += "2";
        }

        private void _3_Click(object sender, RoutedEventArgs e)
        {
            CNumber.Content += "3";
        }

        private void _4_Click(object sender, RoutedEventArgs e)
        {
            CNumber.Content += "4";
        }

        private void _5_Click(object sender, RoutedEventArgs e)
        {
            CNumber.Content += "5";
        }

        private void _6_Click(object sender, RoutedEventArgs e)
        {
            CNumber.Content += "6";
        }

        private void _7_Click(object sender, RoutedEventArgs e)
        {
            CNumber.Content += "7";
        }

        private void _8_Click(object sender, RoutedEventArgs e)
        {
            CNumber.Content += "8";
        }

        private void _9_Click(object sender, RoutedEventArgs e)
        {
            CNumber.Content += "9";
        }

        private void Point_Click(object sender, RoutedEventArgs e)
        {
            CNumber.Content += ".";
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            if (CNumber.Content != "")
            {
                if (Convert.ToDouble(CNumber.Content) > 0)
                    CNumber.Content = "-" + CNumber.Content;
                else if (Convert.ToDouble(CNumber.Content) < 0)
                    CNumber.Content = CNumber.Content.ToString().Substring(1);
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            CNumber.Content = "";
        }

        private void _0_Click(object sender, RoutedEventArgs e)
        {
            CNumber.Content += "0";
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (CNumber.Content != "")
                CNumber.Content = CNumber.Content.ToString().Substring(0, CNumber.Content.ToString().Length-1);
        }

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            Num = Convert.ToDouble(CNumber.Content);
            CNumber.Content = "";
            Operation = 1;
        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            Num = Convert.ToDouble(CNumber.Content);
            CNumber.Content = "";
            Operation = 2;
        }

        private void Mult_Click(object sender, RoutedEventArgs e)
        {
            Num = Convert.ToDouble(CNumber.Content);
            CNumber.Content = "";
            Operation = 3;
        }

        private void Divide_Click(object sender, RoutedEventArgs e)
        {
            Num = Convert.ToDouble(CNumber.Content);
            CNumber.Content = "";
            Operation = 4;
        }

        private void Equal_Click(object sender, RoutedEventArgs e)
        {
            double Num2 = Convert.ToDouble(CNumber.Content);
            if (Operation == 1)
                CNumber.Content = Num + Num2;
            else if (Operation == 2)
                CNumber.Content = Num - Num2;
            else if (Operation == 3)
                CNumber.Content = Num * Num2;
            else if (Operation == 4)
                CNumber.Content = Num / Num2;
        }
    }
}
