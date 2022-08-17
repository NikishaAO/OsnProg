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
    /// Логика взаимодействия для Meetings.xaml
    /// </summary>
    public partial class Meetings : Window
    {
        string connectionString = null;
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;
        List<string> list = new List<string>();
        public Meetings()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            list = GD();
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

        private void GetMeetings(string date1, string date2, int i)
        {
        
            string Query = $"SELECT Meetings.MeetingID AS [ID], Comissions.ComissionName AS [Назва], Meetings.MeetingDate AS [Дата], Meetings.MeetingPlace AS [Мiсце проведення] FROM Meetings INNER JOIN Comissions ON Comissions.ComissionID = Meetings.ComissionID WHERE MeetingDate > {date1} AND MeetingDate < {date2} AND Meetings.ComissionID = {i};";
            try
            {
                GetData(Query, Meetings1);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void GetSkips(string date1, string date2, int i)
        {

            string Query = $"SELECT RadaMembers.MemberName AS [Прізвище ІП], Meetings.MeetingDate AS [Дата], Comissions.ComissionName AS [Комісія], Present AS [присутній] FROM MembersMeetings INNER JOIN RadaMembers ON ID = MembersMeetings.MembID INNER JOIN Meetings ON MeetID = Meetings.MeetingID INNER JOIN Comissions ON Comissions.ComissionID = Meetings.ComissionID WHERE Present = 0 AND  MeetingDate > {date1} AND MeetingDate < {date2} AND Meetings.ComissionID = {i};";
            try
            {
                GetData(Query, Skips);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            string date1 = Date1.SelectedDate.ToString();
            string date2 = Date2.SelectedDate.ToString();

            date1 = "'" + date1.Substring(6,4) + "/" + date1.Substring(3,2) + "/" + date1.Substring(0,2) + "'";
            date2 = "'" + date2.Substring(6,4) + "/" + date2.Substring(3,2) + "/" + date2.Substring(0,2) + "'";

            GetMeetings(date1, date2, CB.SelectedIndex + 1);
            GetSkips(date1, date2, CB.SelectedIndex + 1);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MW = new MainWindow();
            MW.Show();
            Hide();
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
            for (int i = 0; i < 37; i++)
                list.Add(dt.Rows[i].ItemArray[0].ToString());
            connection.Close();

            return list;
        }

        private void CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
