using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace Lab01
{
    internal class StudentBase
    {
        Window Base = new Window();
        List<string> DB = new List<string>();
        TextBox Number = new TextBox();
        TextBox Name = new TextBox();
        TextBox Group = new TextBox();


        public StudentBase()
        {
            InitControls();
        }
        public void InitControls()
        {
            Grid grid = new Grid();
            RowDefinition[] rows = new RowDefinition[7];
            ColumnDefinition[] columns = new ColumnDefinition[5];

            //grid.ShowGridLines = true;

            StreamReader ReadBase = new StreamReader("Database.txt");

            while (!ReadBase.EndOfStream)
            {
                DB.Add(ReadBase.ReadLine());
            }
            ReadBase.Close();

            for (int i = 0; i < rows.Length; i++)
            {
                rows[i] = new RowDefinition();
                grid.RowDefinitions.Add(rows[i]);
            }
            for(int j = 0; j < columns.Length; j++)
            {
                columns[j] = new ColumnDefinition();
                grid.ColumnDefinitions.Add(columns[j]);
            }

            Button Home = new Button();
            Home.Content = "Home";
            Grid.SetColumn(Home, 4);
            Grid.SetRow(Home, 6);
            grid.Children.Add(Home);
            Home.Click += Home_Click;

           

            Grid.SetColumn(Number, 1);
            Grid.SetRow(Number, 1);
            grid.Children.Add(Number);
            Number.FontSize = 18;

            Grid.SetRow(Name, 3);
            Grid.SetColumn(Name, 1);
            grid.Children.Add(Name);
            Name.FontSize = 18;

            Grid.SetRow(Group, 5);
            Grid.SetColumn(Group, 1);
            grid.Children.Add(Group);
            Group.FontSize = 18;

            Label[] Labels = new Label[3];

            int c = 1;
            for (int i = 0; i < Labels.Length; i++)
            {
                Labels[i] = new Label();
                Grid.SetRow(Labels[i], c);
                Grid.SetColumn(Labels[i], 2);
                grid.Children.Add(Labels[i]);
                Labels[i].FontSize = 18;
                c = c + 2;
            }

            Labels[0].Content = "Номер залiкової книжки";
            Labels[1].Content = "ФIO";
            Labels[2].Content = "Група";

            Button Delete = new Button();
            Grid.SetColumn(Delete, 3);
            Grid.SetRow(Delete, 4);
            grid.Children.Add(Delete);
            Delete.FontSize = 18;
            Delete.Content = "Delete by ID";
            Delete.Click += Delete_Click;

            Button Write = new Button();
            Grid.SetColumn(Write, 3);
            Grid.SetRow(Write, 2);
            grid.Children.Add(Write);
            Write.FontSize = 18;
            Write.Content = "Write";
            Write.Click += Write_Click;

            Base.Content = grid;
            Base.Show();
        }

        private void Write_Click(object sender, RoutedEventArgs e)
        {
            DB.Add(Number.Text + " " + Name.Text + " " + Group.Text);
            Number.Text = "";
            Name.Text = "";
            Group.Text = "";

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < DB.Count; i++)
            {
                string[] Line = DB[i].Split(' ');
                if (Number.Text == Line[0])
                    DB.Remove(DB[i]);
            }
            Number.Text = "";
            Name.Text = "";
            Group.Text = "";
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();

            StreamWriter Database = new StreamWriter("Database.txt");
            foreach (string Student in DB)
            {
                Database.WriteLine(Student);
            }
            Database.Close();
            mw.Show();
            Base.Hide();

        }
    }
}
