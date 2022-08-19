using System.Windows;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Windows.Controls;

namespace ConnectToSQLServer
{
    public partial class MainWindow : Window
    {
        string connectionString = null;        
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;

        public MainWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //MessageBox.Show(connectionString);
            
        }

        private void GetData(string SQLQuery, DataGrid dataGrid)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand(SQLQuery, connection);
            adapter = new SqlDataAdapter(command);
            DataTable Table = new DataTable();
            dataGrid.AutoGenerateColumns = true;
            adapter.Fill(Table);
            dataGrid.ItemsSource = Table.DefaultView;
            
            connection.Close();            
        }
        private void ComissionManager_Click(object sender, RoutedEventArgs e)
        {
            Window1 Comissions = new Window1();
            Comissions.Show();
            Hide();
        }

        private void ToMembers_Click(object sender, RoutedEventArgs e)
        {
            MembersList ML = new MembersList();
            ML.Show();
            Hide();
        }

        private void MarksDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Meetings M = new Meetings();
            M.Show();
            Hide();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}