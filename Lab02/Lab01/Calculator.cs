using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Lab01
{
    internal class Calculator
    {
        double First = 0;
        byte Operation;
        Label numlabel = new Label();
        Window Calc = new Window();

        public Calculator()
        {
            initControls();
        }

        public void initControls()
        {
            int C = 6;
            int R = 8;

            Calc.ResizeMode = ResizeMode.NoResize;

            Grid grid = new Grid();
            RowDefinition[] rows = new RowDefinition[R];
            ColumnDefinition[] cols = new ColumnDefinition[C];
            GridLengthConverter gridLengthConverter = new GridLengthConverter();


            for (int c = 0; c < R; c++)
            {
                rows[c] = new RowDefinition();
                rows[c].Height = (GridLength)gridLengthConverter.ConvertFrom(Convert.ToDouble(R / 8) + "*");
                grid.RowDefinitions.Add(rows[c]);
            }
        
            cols[0] = new ColumnDefinition();
            cols[0].Width = (GridLength)gridLengthConverter.ConvertFrom(Convert.ToDouble(C) + "*");
            grid.ColumnDefinitions.Add(cols[0]);
            for (int c = 1; c < C - 1; c++)
            {
                cols[c] = new ColumnDefinition();
                cols[c].Width = (GridLength)gridLengthConverter.ConvertFrom(Convert.ToDouble(C / 5) + "*");
                grid.ColumnDefinitions.Add(cols[c]);
            }

            cols[5] = new ColumnDefinition();
            cols[5].Width = (GridLength)gridLengthConverter.ConvertFrom(Convert.ToDouble(C) + "*");
            grid.ColumnDefinitions.Add(cols[5]);

            Button[,] NumButton = new Button[R, C];
            int count = 1;

            numlabel.FontSize = 18;
            Grid.SetRow(numlabel, 1); ;
            Grid.SetColumn(numlabel, 1);
            grid.Children.Add(numlabel);

            Button PosNeg = new Button();
            PosNeg.Content = "+/-";
            PosNeg.Click += PosNegDo;
            Grid.SetRow(PosNeg, 5); ;
            Grid.SetColumn(PosNeg, 1);
            grid.Children.Add(PosNeg);

            Button Dot = new Button();
            Dot.Content = ",";
            Dot.Click += NumButtonClick;
            Grid.SetRow(Dot, 5); ;
            Grid.SetColumn(Dot, 3);
            grid.Children.Add(Dot);

            Button Zero = new Button();
            Zero.Content = "0";
            Zero.Click += NumButtonClick;
            Grid.SetRow(Zero, 5); ;
            Grid.SetColumn(Zero, 2);
            grid.Children.Add(Zero);

            Button ToMain = new Button();
            ToMain.Content = "Home";
            Grid.SetRow(ToMain, 7);
            Grid.SetColumn(ToMain, 2);
            grid.Children.Add(ToMain);
            ToMain.Click += HomeClick;

            Button Plus = new Button();
            Plus.Content = "+";
            Grid.SetRow(Plus, 5);
            Grid.SetColumn(Plus, 4);
            grid.Children.Add(Plus);

            Button Minus = new Button();
            Minus.Content = "-";
            Grid.SetRow(Minus, 4);
            Grid.SetColumn(Minus, 4);
            grid.Children.Add(Minus);

            Button Multiple = new Button();
            Multiple.Content = "X";
            Grid.SetRow(Multiple, 3);
            Grid.SetColumn(Multiple, 4);
            grid.Children.Add(Multiple);

            Button Divide = new Button();
            Divide.Content = "/";
            Grid.SetRow(Divide, 2);
            Grid.SetColumn(Divide, 4);
            grid.Children.Add(Divide);

            Plus.Click += PlusClick;
            Minus.Click += MinusClick;
            Multiple.Click += MulClick;
            Divide.Click += DivideClick;

            Button Equals = new Button();
            Equals.Content = "=";
            Grid.SetRow(Equals, 1);
            Grid.SetColumn(Equals, 2);
            grid.Children.Add(Equals);
            Equals.Click += EqualsClick;

            Button Clear = new Button();
            Clear.Content = "C";
            Grid.SetRow(Clear, 1);
            Grid.SetColumn(Clear, 3);
            grid.Children.Add(Clear);
            Clear.Click += ClearClick;

            Button Backspace = new Button();
            Backspace.Content = "<X";
            Grid.SetRow(Backspace, 1);
            Grid.SetColumn(Backspace, 4);
            grid.Children.Add(Backspace);
            Backspace.Click += BackspaceCkick;

            for (int i = 2; i < R - 3; i++)
                for (int j = 1; j < C - 2; j++)
                {
                    NumButton[i, j] = new Button();
                    NumButton[i, j].Click += NumButtonClick;
                    NumButton[i, j].Content = count;
                    count++;
                }

            for (int i = 2; i < R - 3; i++)
                for (int j = 1; j < C - 2; j++)
                {
                    Grid.SetRow(NumButton[i, j], i);
                    Grid.SetColumn(NumButton[i, j], j);
                }

            for (int i = 2; i < R - 3; i++)
                for (int j = 1; j < C - 2; j++)
                {
                    grid.Children.Add(NumButton[i, j]);
                }


            Calc.Content = grid;
            Calc.Show();
        }
        private void NumButtonClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            numlabel.Content += btn.Content.ToString();
        }
        private void PosNegDo(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if(Convert.ToDouble(numlabel.Content) > 0)
                numlabel.Content = "-" + numlabel.Content;
            else if(Convert.ToDouble(numlabel.Content) < 0)
                numlabel.Content = numlabel.Content.ToString().Substring(1);
        }
        private void HomeClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Calc.Hide();
        }

        private void PlusClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Operation = 0;
                First = Convert.ToDouble(numlabel.Content);
                numlabel.Content = "";
    
        }
        private void MinusClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Operation = 1;
            First = Convert.ToDouble(numlabel.Content);
            numlabel.Content = "";
        }
        private void MulClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Operation = 2;
            First = Convert.ToDouble(numlabel.Content);
            numlabel.Content = "";
        }
        private void DivideClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Operation = 3;
            First = Convert.ToDouble(numlabel.Content);
            numlabel.Content = "";
        }
        private void EqualsClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if(Operation == 0)
            {
                numlabel.Content = First + Convert.ToDouble(numlabel.Content);
            }
            else if (Operation == 1)
            {
                numlabel.Content = First - Convert.ToDouble(numlabel.Content);
            }
            else if(Operation == 2)
            {
                numlabel.Content = First * Convert.ToDouble(numlabel.Content);
            }
            else if(Operation == 3)
            {
                numlabel.Content = First / Convert.ToDouble(numlabel.Content);
            }

            First = 0;
        }
        private void ClearClick(object sender, RoutedEventArgs e)
        {
            numlabel.Content = "";
        }

        private void BackspaceCkick(object sender, RoutedEventArgs e)
        {
            numlabel.Content = numlabel.Content.ToString().Substring(0, numlabel.Content.ToString().Length - 1);
        }
    }
}
