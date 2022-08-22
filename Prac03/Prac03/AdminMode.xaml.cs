using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Data.SqlTypes;
using System.Net;

namespace Prac03
{
    /// <summary>
    /// Логика взаимодействия для AdminMode.xaml
    /// </summary>
    public partial class AdminMode : Window
    {
        static string ConnectionString = "Data Source=DESKTOP-F8KAAVC;Initial Catalog=Prac03;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        SqlConnection connection;
        SqlCommand command;
        DataTable dt;
        int LenTable;
        int index = 0;
        List<string> UserList;

        public AdminMode()
        {
            InitializeComponent();
            UserList = GetUsers();
        }

        private void UpdateAdminPassword_Click(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(ConnectionString);

            string Query;
            connection.Open();
            String RealPass = CurrentAdminPassword.Password.ToString();
            String NewPass = NewAdminPassword.Password.ToString();
            String NewPass2 = NewAdminPassword2.Password.ToString();
            if ((RealPass == AdminPassword.Password.ToString()) && (NewPass != "")
            && (NewPass == NewPass2))

            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    Query = "UPDATE MainTable SET Password ='" + NewPass + "'WHERE Login = 'ADMIN'; ";
                    command = new SqlCommand(Query, connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Пароль змінено");
                }
            }
            connection.Close();
        }

        void UpdateDataTable()
        {
            connection = new SqlConnection(ConnectionString);
            connection.Open();
            if (connection.State == System.Data.ConnectionState.Open)
            {
                adapter = new SqlDataAdapter("SELECT Name, Surname, Login, Status, Restriction FROM MainTable", connection);


                dt = new DataTable("Користувачі системи");
                adapter.Fill(dt);
                Data.ItemsSource = dt.DefaultView;
                LenTable = dt.Rows.Count;
            }
            connection.Close();
        }

        private void prev_Click(object sender, RoutedEventArgs e)
        {
            if (index > 0)
            {
                index--;
                UserNameLabel1.Content = dt.Rows[index][0].ToString();
                UserSurnameLabel.Content = dt.Rows[index][1].ToString();
                LoginLabel.Content = dt.Rows[index][2].ToString();
                StatusLabel.Content = dt.Rows[index][3].ToString();
                RestrictionLabel.Content = dt.Rows[index][4].ToString();
            }
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            if (index < LenTable - 1)
            {
                index++;
                UserNameLabel1.Content = dt.Rows[index][0].ToString();
                UserSurnameLabel.Content = dt.Rows[index][1].ToString();
                LoginLabel.Content = dt.Rows[index][2].ToString();
                StatusLabel.Content = dt.Rows[index][3].ToString();
                RestrictionLabel.Content = dt.Rows[index][4].ToString();
            }
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(ConnectionString);
            connection.Open();
            String strQ;
            String UserLogin = NewUserLogin.Text;
            try
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    strQ = "INSERT INTO MainTable (Name, Surname, Login, Status, Restriction) values('', '', '" + UserLogin + "', 1, 0); ";

                    command = new SqlCommand(strQ, connection);
                    command.ExecuteNonQuery();
                }
                UpdateDataTable();
            }
            catch
            {
                MessageBox.Show("Користувача не додано! Можливо такий в базі вже є!");
            }
            connection.Close();
        }

        private void ChangeStatus_Click(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(ConnectionString);
            connection.Open();
            String strQ;
            bool UserStatus = (bool)ChangeActive.IsChecked;
            if (connection.State == System.Data.ConnectionState.Open)
            {
                strQ = "UPDATE MainTable SET Status ='" + UserStatus + "' WHERE Login='" + UsersLogins.SelectedValue.ToString() + "';";
                command = new SqlCommand(strQ, connection);
                command.ExecuteNonQuery();
            }
            connection.Close();
            UpdateDataTable();
        }

        private void ExitFromSystem_Click(object sender, RoutedEventArgs e)
        {
            CurrentAdminPassword.Password = ""; CurrentAdminPassword.IsEnabled = false;
            NewAdminPassword.Password = ""; NewAdminPassword.IsEnabled = false;
            NewAdminPassword2.Password = ""; NewAdminPassword2.IsEnabled = false;
            prev.IsEnabled = false; next.IsEnabled = false;
            UpdateAdminPassword.IsEnabled = false;
            AddUser.IsEnabled = false;
            ChangeStatus.IsEnabled = false;
            ChangeRestriction.IsEnabled = false;
            dt.Clear();
            Data.ItemsSource = dt.DefaultView;
            AdminPassword.Password = "";
            UsersLogins.ItemsSource = "";
            UserNameLabel1.Content = null;
            UserSurnameLabel.Content = null;
            LoginLabel.Content = null;
            StatusLabel.Content = null;
            RestrictionLabel.Content =null;
         
        }

        private void ChangeRestriction_Click(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(ConnectionString);
            connection.Open();
            String strQ;
            bool UserRestriction = (bool)ChangeRestCheck.IsChecked;
            if (connection.State == System.Data.ConnectionState.Open)
            {
                strQ = "UPDATE MainTable SET Restriction ='" + UserRestriction + "' WHERE Login = '" + UsersLogins.SelectedValue.ToString() + "'; ";

                command = new SqlCommand(strQ, connection);
                command.ExecuteNonQuery();
            }
            connection.Close();
            UpdateDataTable();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(ConnectionString);

            connection.Open();
            String RealPass = CurrentAdminPassword.Password.ToString();

            if (connection.State == System.Data.ConnectionState.Open)
            {
                adapter = new SqlDataAdapter("SELECT Password FROM MainTable WHERE Login = 'ADMIN';", connection);


                dt = new DataTable("Користувачі системи");
                adapter.Fill(dt);
                RealPass = dt.Rows[0][0].ToString();
                if (RealPass == AdminPassword.Password)
                {
                    MessageBox.Show("Авторизовано");
                    prev.IsEnabled = true;
                    next.IsEnabled = true;
                    ChangeActive.IsEnabled = true;
                    ChangeRestriction.IsEnabled = true;
                    AddUser.IsEnabled = true;
                    ExitFromSystem.IsEnabled = true;
                    UpdateAdminPassword.IsEnabled = true;
                    ChangeRestCheck.IsEnabled = true;
                    ChangeActive.IsEnabled = true;
                    ChangeStatus.IsEnabled = true;
                    UpdateDataTable();
                    UserNameLabel1.Content = dt.Rows[index][0].ToString();
                    UserSurnameLabel.Content = dt.Rows[index][1].ToString();
                    LoginLabel.Content = dt.Rows[index][2].ToString();
                    StatusLabel.Content = dt.Rows[index][3].ToString();
                    RestrictionLabel.Content = dt.Rows[index][4].ToString();
                    UsersLogins.ItemsSource = UserList;
                }
                else
                {
                    MessageBox.Show("Неправильний пароль");
                }
            }
            connection.Close();

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MW = new MainWindow();
            MW.Show();
            Hide();
        }

        private List<string> GetUsers()
        {
            List<string> list = new List<string>();

            connection = new SqlConnection(ConnectionString);

            connection.Open();
            adapter = new SqlDataAdapter("SELECT Login FROM MainTable;", connection);

            dt = new DataTable();
            adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(dt.Rows[i][0].ToString());
            }

            return list;
        }
    }
}
