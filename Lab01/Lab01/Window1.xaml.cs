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
using System.IO;
using System.Collections.Generic;

namespace Lab01
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    /// 

    public partial class Window1 : Window
    {

        List<string> DB = new List<string>();


        public Window1()
        {
            InitializeComponent();
            StreamReader ReadBase = new StreamReader("Database.txt");

            while (!ReadBase.EndOfStream)
            {
                DB.Add(ReadBase.ReadLine());
            }
            ReadBase.Close();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Hide();
            mw.Show();
            StreamWriter Database = new StreamWriter("Database.txt");
            foreach(string Student in DB)
            {
                Database.WriteLine(Student);
            }
            Database.Close();

        }



        private void Write_Click(object sender, RoutedEventArgs e)
        {

            DB.Add(IDBox.Text + " " + NameBox.Text + " " + GroupBox.Text);
            IDBox.Text = "";
            NameBox.Text = "";
            GroupBox.Text = "";

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {       
            for(int i =0; i < DB.Count; i++)
            {
                string[] Line = DB[i].Split(' ');
                if (IDBox.Text == Line[0])
                    DB.Remove(DB[i]);
            }
            IDBox.Text = "";
            NameBox.Text = "";
            GroupBox.Text = "";
        }
    }
}
