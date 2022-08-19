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
    /// Логика взаимодействия для AddNewComission.xaml
    /// </summary>
    public partial class AddNewComission : Window
    {
        string connectionString = null;
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;
        List<string> list = new List<string>();
        List<string> ComCount = new List<string>();
        public AddNewComission()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            list = GetMembers();
            CB.ItemsSource = list;
            ComCount = GetComissions();

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Window1 W1 = new Window1();
            W1.Show();
            Hide();
        }

        private void ToNewMemberButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewComissionMember Add = new AddNewComissionMember();
            Add.Show();
            Hide();
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

        private void AddComission(string Comission)
        {
            string Query = $"INSERT Comissions VALUES ('{Comission}')";
            try
            {
                SetData(Query);
                MessageBox.Show("Нову комісію успішно створено");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void AddComissionHead(int i)
        {

            string date1 = DateTime.Now.ToString();
            date1 = "'" + date1.Substring(6, 4) + "/" + date1.Substring(3, 2) + "/" + date1.Substring(0, 2) + "'";
            string Query = $"INSERT ComissionHeads VALUES ({date1},1,NULL,{i},{ComCount.Count + 1})";
            ComCount.Add(ComNameText.Text);
            try
            {
                SetData(Query);
                MessageBox.Show("Нову комісію успішно створено");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
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

            try
            {
                for (int i = 0; i < 100; i++)
                    list.Add(dt.Rows[i].ItemArray[0].ToString());
            }
            catch { }
            connection.Close();

            return list;
        }

        private void AddButton_Click_1(object sender, RoutedEventArgs e)
        {
            AddComission(ComNameText.Text);
            AddComissionHead(CB.SelectedIndex + 1);
        }
    }
}
