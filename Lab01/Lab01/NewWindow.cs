using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Lab01
{
    class NewWindow
    {
        Window NW = new Window();

        public NewWindow()
        {
            initControls();
        }

        public void initControls()
        {
            NW.ResizeMode = ResizeMode.NoResize;

            Grid grid = new Grid();
            RowDefinition[] rows = new RowDefinition[3];
            ColumnDefinition[] cols = new ColumnDefinition[3];

            for(int i =0;i < 3; i++)
            {
                rows[i] = new RowDefinition();
                cols[i] = new ColumnDefinition();
                grid.ColumnDefinitions.Add(cols[i]);
                grid.RowDefinitions.Add(rows[i]);
            }
        
           

            Label label = new Label();
            
            label.Content = "Нікіша Андрій Олександрович\nКП-11\n2022";
            Grid.SetColumn(label, 1);
            Grid.SetRow(label, 1);
            grid.Children.Add(label);
            label.FontSize = 25;



            Button BackButton = new Button();
            BackButton.Content = "Home";
            Grid.SetRow(BackButton, 2);
            Grid.SetColumn(BackButton, 2);
            grid.Children.Add(BackButton);
            BackButton.Click += BackButtonDo;
            NW.Content = grid;
            NW.Show();
        }
        private void BackButtonDo(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            MainWindow mw = new MainWindow();
            mw.Show();
            NW.Hide();
        }
    }
}
