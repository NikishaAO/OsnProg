using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Prac03
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connection;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ToAdminMode_Click(object sender, RoutedEventArgs e)
        {
            AdminMode adminMode = new AdminMode();
            adminMode.Show();
            Hide();
        }

        private void ToUserMode_Click(object sender, RoutedEventArgs e)
        {
            UserMode UM = new UserMode();
            UM.Show();
            Hide();
        }

        private void ToAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutDeveloper AD = new AboutDeveloper();
            AD.Show();
            Hide();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
