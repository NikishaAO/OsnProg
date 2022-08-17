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
using System.Configuration;
using System.Data.SqlClient;
using System.Data;



namespace ConnectToSQLServer
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        string connectionString = null;
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;
        List<string> list = new List<string>();
        public Window1()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //MessageBox.Show(connectionString);
            GetComissions();
            GetComissionMembers(0);
            list = GD();
            CB.ItemsSource = list;
        }

        public List<string> GD()
        {
            List<string> list = new List<string>();

            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand("SELECT Comissions.ComissionName FROM Comissions;", connection);
            adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            for(int i = 0; i < 37; i++)
            list.Add(dt.Rows[i].ItemArray[0].ToString());
            connection.Close();

            return list;
        }

        private void GetData(string SQLQuery, DataGrid dataGrid)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand(SQLQuery, connection);
            adapter = new SqlDataAdapter(command);
            DataTable Table = new DataTable();
            adapter.Fill(Table);
            dataGrid.ItemsSource = Table.DefaultView;
            connection.Close();
        }

        private void GetComissions()
        {
            string Query = "Select Comissions.ComissionID AS [No], ComissionName as [Назва], MemberName as [Прізвище ІБ] FROM Comissions INNER JOIN ComissionHeads ON ComissionHeads.ComissionID = Comissions.ComissionID INNER JOIN RadaMembers ON MemberID = ID WHERE IsCurrent = 1;";
            try
            {
                GetData(Query, ComissionList);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void GetComissionMembers(int i)
        {
            string Query = $"SELECT MemberName AS [Прізвище ІБ], IsCurrent AS [Голова] FROM RadaMembers FULL OUTER JOIN MembersComissions ON MemberID = ID FULL OUTER JOIN ComissionHeads ON ComissionHeads.MemberID = MembersComissions.MemberID WHERE IsActive = 1 AND ComID = {i} ;";
            try
            {
                GetData(Query, ComMembersList);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Hide();
        }

        private void CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetComissionMembers(CB.SelectedIndex + 1);
        }
    }
}
