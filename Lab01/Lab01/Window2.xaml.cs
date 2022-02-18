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
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        private void ToMW_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Hide();
            mw.Show();
        }

        private void F1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(F1.SelectedIndex == F2.SelectedIndex && F1.SelectedIndex == F3.SelectedIndex)
            {
                GameOver.Content = "Гру закінечено!";
            }
            if (F1.SelectedIndex == F4.SelectedIndex && F1.SelectedIndex == F7.SelectedIndex)
            {
                GameOver.Content = "Гру закінечено!";
            }
            if (F1.SelectedIndex == F5.SelectedIndex && F1.SelectedIndex == F9.SelectedIndex)
            {
                GameOver.Content = "Гру закінечено!";
            }
        }

        private void Clear_Field_Click(object sender, RoutedEventArgs e)
        {
            F1.SelectedItem = null;
            F2.SelectedItem = null;
            F3.SelectedItem = null;
            F4.SelectedItem = null;
            F5.SelectedItem = null;
            F6.SelectedItem = null;
            F7.SelectedItem = null;
            F8.SelectedItem = null;
            F9.SelectedItem = null;
            GameOver.Content = null;
        }

        private void F2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (F1.SelectedIndex == F2.SelectedIndex && F1.SelectedIndex == F3.SelectedIndex)
            {
                GameOver.Content = "Гру закінечено!";
            }
            if (F2.SelectedIndex == F5.SelectedIndex && F2.SelectedIndex == F8.SelectedIndex)
            {
                GameOver.Content = "Гру закінечено!";
            }
        }

        private void F3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (F1.SelectedIndex == F2.SelectedIndex && F1.SelectedIndex == F3.SelectedIndex)
            {
                GameOver.Content = "Гру закінечено!";
            }
            if (F3.SelectedIndex == F5.SelectedIndex && F3.SelectedIndex == F7.SelectedIndex)
            {
                GameOver.Content = "Гру закінечено!";
            }
            if (F3.SelectedIndex == F6.SelectedIndex && F3.SelectedIndex == F9.SelectedIndex)
            {
                GameOver.Content = "Гру закінечено!";
            }
        }

        private void F4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (F1.SelectedIndex == F4.SelectedIndex && F1.SelectedIndex == F7.SelectedIndex)
            {
                GameOver.Content = "Гру закінечено!";
            }
            if (F4.SelectedIndex == F5.SelectedIndex && F4.SelectedIndex == F6.SelectedIndex)
            {
                GameOver.Content = "Гру закінечено!";
            }
        }

        private void F6_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (F3.SelectedIndex == F6.SelectedIndex && F3.SelectedIndex == F9.SelectedIndex)
            {
                GameOver.Content = "Гру закінечено!";
            }
            if (F4.SelectedIndex == F5.SelectedIndex && F4.SelectedIndex == F6.SelectedIndex)
            {
                GameOver.Content = "Гру закінечено!";
            }
        }

        private void F5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (F1.SelectedIndex == F5.SelectedIndex && F1.SelectedIndex == F9.SelectedIndex)
            {
                GameOver.Content = "Гру закінечено!";
            }
            if (F3.SelectedIndex == F5.SelectedIndex && F3.SelectedIndex == F7.SelectedIndex)
            {
                GameOver.Content = "Гру закінечено!";
            }
            if (F2.SelectedIndex == F5.SelectedIndex && F2.SelectedIndex == F8.SelectedIndex)
            {
                GameOver.Content = "Гру закінечено!";
            }
            if (F4.SelectedIndex == F5.SelectedIndex && F4.SelectedIndex == F6.SelectedIndex)
            {
                GameOver.Content = "Гру закінечено!";
            }
        }

        private void F7_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (F7.SelectedIndex == F8.SelectedIndex && F7.SelectedIndex == F9.SelectedIndex)
            {
                GameOver.Content = "Гру закінечено!";
            }
            if (F3.SelectedIndex == F5.SelectedIndex && F3.SelectedIndex == F7.SelectedIndex)
            {
                GameOver.Content = "Гру закінечено!";
            }
            if (F1.SelectedIndex == F4.SelectedIndex && F1.SelectedIndex == F7.SelectedIndex)
            {
                GameOver.Content = "Гру закінечено!";
            }
        }

        private void F8_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (F2.SelectedIndex == F5.SelectedIndex && F2.SelectedIndex == F8.SelectedIndex)
            {
                GameOver.Content = "Гру закінечено!";
            }
            if (F7.SelectedIndex == F8.SelectedIndex && F7.SelectedIndex == F9.SelectedIndex)
            {
                GameOver.Content = "Гру закінечено!";
            }
        }

        private void F9_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (F7.SelectedIndex == F8.SelectedIndex && F7.SelectedIndex == F9.SelectedIndex)
            {
                GameOver.Content = "Гру закінечено!";
            }
            if (F3.SelectedIndex == F6.SelectedIndex && F3.SelectedIndex == F9.SelectedIndex)
            {
                GameOver.Content = "Гру закінечено!";
            }
            if (F1.SelectedIndex == F5.SelectedIndex && F1.SelectedIndex == F9.SelectedIndex)
            {
                GameOver.Content = "Гру закінечено!";
            }
        }
    }
}
