using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Lab01
{
    internal class TicTacToe
    {
        Window TTT = new Window();
        static ComboBox[,] Fields = new ComboBox[3, 3];
        static Label Win = new Label();

        public TicTacToe()
        {
            InitControls();
        }

        public void InitControls()
        {

            TTT.ResizeMode = ResizeMode.NoResize;

            Grid grid = new Grid();
            RowDefinition[] rows = new RowDefinition[5];
            ColumnDefinition[] cols = new ColumnDefinition[7];
            GridLengthConverter gridLengthConverter = new GridLengthConverter();

            //grid.ShowGridLines = true;

            for(int i = 0; i < rows.Length; i++)
            {
                rows[i] = new RowDefinition();
                grid.RowDefinitions.Add(rows[i]);
            }

            for(int j = 0; j < cols.Length; j++)
            {
                cols[j] = new ColumnDefinition();
                grid.ColumnDefinitions.Add(cols[j]);
            }

            for(int i =1; i < 4;i++)
                for(int j = 1; j < 4; j++)
                {
                    Fields[i - 1, j - 1] = new ComboBox();
                    Fields[i - 1, j - 1].ItemsSource = new List<Char> { 'X', 'O' };
                    Fields[i - 1, j - 1].SelectedIndex = -1;
                    Fields[i - 1, j - 1].FontSize = 36;
                    Fields[i - 1, j - 1].SelectionChanged += CheckForWin;
                    Grid.SetRow(Fields[i - 1, j - 1], i);
                    Grid.SetColumn(Fields[i - 1, j - 1], j);
                    grid.Children.Add(Fields[i - 1, j - 1]);
                }

            Button clear = new Button();
            clear.Content = "Clear";
            Grid.SetColumn(clear, 5);
            Grid.SetRow(clear, 2);
            grid.Children.Add(clear);
            clear.Click += Clear_Click;

            Button Home = new Button();
            Home.Content = "Home";
            Grid.SetColumn(Home, 6);
            Grid.SetRow(Home, 4);
            grid.Children.Add(Home);
            Home.Click += Home_Click;

            Grid.SetColumn(Win, 5);
            Grid.SetRow(Win, 1);
            grid.Children.Add(Win);

            TTT.Content = grid;
            TTT.Show();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            TTT.Hide();
            mw.Show();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    Fields[i, j].SelectedIndex = -1;
                }
            Win.Content = "";
        }

        static void CheckForWin(object sender, RoutedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            //[,] Values = new char[3, 3];
            for(int i = 0; i < 3; i++)
                for(int j = 0; j < 3; j++)
                {
                   // Values[i, j] = Fields[i, j].Text;
                }

            if (Fields[0, 0].Text == "X" && Fields[0, 1].Text == "X" && Fields[0, 2].Text == "X")
                Win.Content = "Game over!";
            else if (Fields[0, 0].Text == "X" && Fields[1, 0].Text == "X" && Fields[2, 0].Text == "X")
                Win.Content = "Game over!";
            else if (Fields[0, 0].Text == "X" && Fields[1, 1].Text == "X" && Fields[2, 2].Text == "X")
                Win.Content = "Game over!";
            else if (Fields[1, 0].Text == "X" && Fields[1, 1].Text == "X" && Fields[1, 2].Text == "X")
                Win.Content = "Game over!";
            else if (Fields[2, 0].Text == "X" && Fields[2, 1].Text == "X" && Fields[2, 2].Text == "X")
                Win.Content = "Game over!";
            else if (Fields[0, 1].Text == "X" && Fields[1, 1].Text == "X" && Fields[2, 1].Text == "X")
                Win.Content = "Game over!";
            else if (Fields[1, 2].Text == "X" && Fields[0, 2].Text == "X" && Fields[2, 2].Text == "X")
                Win.Content = "Game over!";
            else if (Fields[0, 2].Text == "X" && Fields[1, 1].Text == "X" && Fields[2, 0].Text == "X")
                Win.Content = "Game over!";
            else if (Fields[0, 0].Text == "O" && Fields[0, 1].Text == "O" && Fields[0, 2].Text == "O")
                Win.Content = "Game over!";
            else if (Fields[0, 0].Text == "O" && Fields[1, 0].Text == "O" && Fields[2, 0].Text == "O")
                Win.Content = "Game over!";
            else if (Fields[0, 0].Text == "O" && Fields[1, 1].Text == "O" && Fields[2, 2].Text == "O")
                Win.Content = "Game over!";
            else if (Fields[1, 0].Text == "O" && Fields[1, 1].Text == "O" && Fields[1, 2].Text == "O")
                Win.Content = "Game over!";
            else if (Fields[2, 0].Text == "O" && Fields[2, 1].Text == "O" && Fields[2, 2].Text == "O")
                Win.Content = "Game over!";
            else if (Fields[0, 1].Text == "O" && Fields[1, 1].Text == "O" && Fields[2, 1].Text == "O")
                Win.Content = "Game over!";
            else if (Fields[1, 2].Text == "O" && Fields[0, 2].Text == "O" && Fields[2, 2].Text == "O")
                Win.Content = "Game over!";
            else if (Fields[0, 2].Text == "O" && Fields[1, 1].Text == "O" && Fields[2, 0].Text == "O")
                Win.Content = "Game over!";
            

           /*if ((Fields[0,0].Text == Fields[0,1].Text) && (Fields[0,0].Text == Fields[0, 2].Text) && Fields[0,0].SelectedIndex != -1)
            {
                Win.Content = "Game over!";
            } else if ((Fields[1, 0].SelectedIndex == Fields[1, 0].SelectedIndex) && (Fields[1, 0].SelectedIndex == Fields[2, 0].SelectedIndex))
            {
                Win.Content = "Game over!";
            }
            else if((Fields[1, 1].SelectedIndex == Fields[0, 1].SelectedIndex) && (Fields[1, 1].SelectedIndex == Fields[2, 1].SelectedIndex))
            {
                Win.Content = "Game over!";
            } else if ((Fields[1, 1].SelectedIndex == Fields[0, 1].SelectedIndex) && (Fields[1, 1].SelectedIndex == Fields[2, 1].SelectedIndex))
            {
                Win.Content = "Game over!";
            }
            else if ((Fields[0, 2].SelectedIndex == Fields[1, 2].SelectedIndex) && (Fields[0, 2].SelectedIndex == Fields[2, 2].SelectedIndex))
            {
                Win.Content = "Game over!";
            }
            else if ((Fields[1, 0].SelectedIndex == Fields[1, 1].SelectedIndex) && (Fields[1, 0].SelectedIndex == Fields[1, 2].SelectedIndex))
            {
                Win.Content = "Game over!";
            }
            else if ((Fields[2, 0].SelectedIndex == Fields[2, 1].SelectedIndex) && (Fields[2, 0].SelectedIndex == Fields[2, 2].SelectedIndex))
            {
                Win.Content = "Game over!";
            }
            else if ((Fields[0, 0].SelectedIndex == Fields[1, 1].SelectedIndex) && (Fields[1, 1].SelectedIndex == Fields[2, 2].SelectedIndex))
            {
                Win.Content = "Game over!";
            }
            else if ((Fields[1, 1].SelectedIndex == Fields[0, 2].SelectedIndex) && (Fields[1, 1].SelectedIndex == Fields[2, 0].SelectedIndex))
            {
                Win.Content = "Game over!";
            }*/



        }
    }
}

