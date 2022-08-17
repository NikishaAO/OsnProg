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
    /// Логика взаимодействия для MembersList.xaml
    /// </summary>
    public partial class MembersList : Window
    {
        string connectionString = null;
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;
        List<string> list = new List<string>();
        public MembersList()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //MessageBox.Show(connectionString);

            GetMembers();
            List<string> list = GetDBData();
            CB.ItemsSource = list;
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

        private void GetMembers()
        {
            string sqlQ = "SELECT ID as [№], " +
                      "MemberName as [Прізвище ІБ], " +
                      "MemberAdress as [Адреса], " +
                      "MemberWorkPhone as [Робочий номер], " +
                      "MemberHomePhone as [Домашній номер]" +
                   "FROM RadaMembers;";
            try
            {
                GetData(sqlQ, MembersList1);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetMemComissions(CB.SelectedIndex + 1);

        }

        private void GetMemComissions(int i)
        {
            string Query = $"SELECT RadaMembers.ID as [No], RadaMembers.MemberName as [Прізвище ІБ], ComissionName AS [Назва комісії] FROM MembersComissions INNER JOIN RadaMembers ON MemberID = ID INNER JOIN Comissions ON ComissionID = ComID WHERE MemberID = {i}; ";

            try
            {
                GetData(Query, MembersComissions);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private List<string> GetDBData()
        {
            List<string> list = new List<string>();

            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand("SELECT MemberName FROM RadaMembers;", connection);
            adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            for (int i = 0; i < 100; i++)
                list.Add(dt.Rows[i].ItemArray[0].ToString());
            connection.Close();

            return list;
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MW = new MainWindow();
            MW.Show();
            Hide();
        }
    }
}
