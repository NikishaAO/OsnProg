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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Prac01
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown(); 
        }

        private void StudyMode_Click(object sender, RoutedEventArgs e)
        {
            StudyMode SM = new StudyMode();
            SM.Show();
            Hide();
        }

        private void CheckMode_Click(object sender, RoutedEventArgs e)
        {
            CheckMode CM = new CheckMode();
            CM.Show();
            Hide();
        }
    }
}
