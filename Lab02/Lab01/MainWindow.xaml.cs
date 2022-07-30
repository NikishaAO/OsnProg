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

namespace Lab01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

      

        private void NewButton1_Click(object sender, RoutedEventArgs e)
        {
            Calculator NewWin1 = new Calculator();
            Hide();
        }

        private void ToLabel_Click(object sender, RoutedEventArgs e)
        {
            NewWindow NW = new NewWindow();
            Hide();
        }

        private void Window2_Click(object sender, RoutedEventArgs e)
        {
            TicTacToe TTT = new TicTacToe();
            Hide();
        }

        private void ToSB_Click(object sender, RoutedEventArgs e)
        {
            StudentBase SB = new StudentBase();
            Hide();
        }
    }
}
