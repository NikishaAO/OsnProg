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
    /// Логика взаимодействия для AddNewMeeting.xaml
    /// </summary>
    public partial class AddNewMeeting : Window
    {
        string connectionString = null;
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;
        List<string> listMembers = new List<string>();
        List<string> listComissions = new List<string>();
        List<int> MembersIDs = new List<int>();
        public AddNewMeeting()
        {
            InitializeComponent();

            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            listComissions = GetComissions();
            CB.ItemsSource = listComissions;
        }

        private void CBMembers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CreateNewMeeting_Click(object sender, RoutedEventArgs e)
        {
            AddMeeting(CB.SelectedIndex + 1);
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

        public List<string> GetMembers(int id)
        {
            List<string> list = new List<string>();

            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand($"SELECT MemberName FROM RadaMembers FULL OUTER JOIN MembersComissions ON ID = MemberID WHERE ComID = {id};", connection);
            adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            try
            {
                for (int i = 0; i < 100; i++)
                    list.Add(dt.Rows[i].ItemArray[0].ToString());
                connection.Close();
            }
            catch { }

            return list;
        }

        public List<int> GetMembersID(int id)
        {
            List<int> list = new List<int>();

            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand($"SELECT ID FROM RadaMembers FULL OUTER JOIN MembersComissions ON ID = MemberID WHERE ComID = {id};", connection);
            adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            try
            {
                for (int i = 0; i < 100; i++)
                    list.Add(Convert.ToInt32(dt.Rows[i].ItemArray[0].ToString()));
                connection.Close();
            }
            catch { }

            return list;
        }

        public int GetMeetingID(int id)
        {
            int lastmeeting = 0;
            string date1 = DateP.SelectedDate.ToString();
            date1 = "'" + date1.Substring(6, 4) + "/" + date1.Substring(3, 2) + "/" + date1.Substring(0, 2) + "'";


            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand($"SELECT MeetingID FROM Meetings WHERE ComissionID = {id} AND MeetingDate = {date1};", connection);
            adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            try
            {

                lastmeeting = Convert.ToInt32(dt.Rows[0].ItemArray[0].ToString());
                connection.Close();
            }
            catch { }

            return lastmeeting;
        }

        private void CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listMembers = GetMembers(CB.SelectedIndex + 1);
            CBMembers.ItemsSource = listMembers;
            MembersIDs = GetMembersID(CB.SelectedIndex + 1);

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

        private void AddMeeting(int id)
        {
            string date1 = DateP.SelectedDate.ToString();

            date1 = "'" + date1.Substring(6, 4) + "/" + date1.Substring(3, 2) + "/" + date1.Substring(0, 2) + "'";

            string Query = $"INSERT Meetings VALUES ({date1},'{MPlace.Text}',{id})";
            try
            {
                SetData(Query);
                MessageBox.Show("Нову зустріч успішно створено. Теперь відмітьте присутній");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            for (int i = 0; i < MembersIDs.Count; i++)
            {
                string Q = $"INSERT MembersMeetings VALUES ({MembersIDs[i]},{GetMeetingID(id)},0)";
                try
                {
                    SetData(Q);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        private void SetPresent(int id)
        {
            string date1 = DateP.SelectedDate.ToString();

            date1 = "'" + date1.Substring(6, 4) + "/" + date1.Substring(3, 2) + "/" + date1.Substring(0, 2) + "'";

            string Query = $"UPDATE MembersMeetings SET Present = 1 WHERE MeetID = {GetMeetingID(CB.SelectedIndex + 1)} AND MembID = {id}";
            try
            {
                SetData(Query);
                MessageBox.Show("Нову зустріч успішно створено. Теперь відмітьте присутній");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        private void MarkAsPresent_Click(object sender, RoutedEventArgs e)
        {
            SetPresent(MembersIDs[CBMembers.SelectedIndex]);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Meetings Mt = new Meetings();
            Mt.Show();
            Hide();
        }
    }
}
