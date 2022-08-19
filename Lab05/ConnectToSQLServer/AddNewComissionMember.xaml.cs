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
    /// Логика взаимодействия для AddNewComissionMember.xaml
    /// </summary>
    public partial class AddNewComissionMember : Window
    {
        string connectionString = null;
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;
        List<string> listMembers = new List<string>();
        List<string> listComissions = new List<string>();

        public AddNewComissionMember()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            listComissions = GetComissions();
            listMembers = GetMembers();
            CBComissions.ItemsSource = listComissions;
            CBMembers.ItemsSource = listMembers;
           
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Window1 W1 = new Window1();
            W1.Show();
            Hide();
        }

        public List<string> GetComissions()
        {
            List<string> list = new List<string>();

            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand("SELECT Comissions.ComissionName FROM Comissions;", connection);
            adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            for (int i = 0; i < 37; i++)
                list.Add(dt.Rows[i].ItemArray[0].ToString());
            connection.Close();

            return list;
        }

        public List<string> GetMembers()
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

        private void SetData(string SQLQuery)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand(SQLQuery, connection);
            adapter = new SqlDataAdapter(command);
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void AddMember(string date, int Member, int Comission)
        {
            string Query = $"INSERT MembersComissions VALUES ({date},1,NULL,{Member},{Comission})";
            try
            {
                SetData(Query);
                MessageBox.Show("Нового члена успішно додано");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string date1 = AddDate.SelectedDate.ToString();
            date1 = "'" + date1.Substring(6, 4) + "/" + date1.Substring(3, 2) + "/" + date1.Substring(0, 2) + "'";

            AddMember(date1, CBMembers.SelectedIndex + 1, CBComissions.SelectedIndex + 1);
        }
    }
}
