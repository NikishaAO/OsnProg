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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Prac03
{
    /// <summary>
    /// Логика взаимодействия для UserMode.xaml
    /// </summary>
    public partial class UserMode : Window
    {
        string login;
        string password;
        SqlConnection connection;
        SqlDataAdapter adapter;
        DataTable dt;
        SqlCommand command;
        string ConnectionString = "Data Source=DESKTOP-F8KAAVC;Initial Catalog=Prac03;Integrated Security=True";
        int count = 0;

        public UserMode()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MW = new MainWindow();
            MW.Show();
            Hide();
        }
        private void AutorButton_Click(object sender, RoutedEventArgs e)
        {
            login = loginField.Text;
            password = passwdField.Password;
            connection = new SqlConnection(ConnectionString);
            connection.Open();
            if (connection.State == System.Data.ConnectionState.Open)
            {
                string Query = "SELECT * FROM MainTable WHERE Login='" + login + "';";
                adapter = new SqlDataAdapter(Query, connection);
                dt = new DataTable("Користувачі системи");
                adapter.Fill(dt);
                if (dt.Rows.Count == 0)
                    MessageBox.Show("Користувача на знайдено");
                else
                {
                    bool Status = Convert.ToBoolean(dt.Rows[0][4]);
                    if (!Status)
                        MessageBox.Show("Користувач заблокований");
                    
                    else
                    {
                        if ((dt.Rows[0][2].ToString() == login) &&  (dt.Rows[0][3].ToString() == password))

                        {
                            NewNameField.Text = dt.Rows[0][0].ToString();
                            NewSurnameField.Text = dt.Rows[0][1].ToString();
                            NewPasswdField.Password = "";
                            NewPasswdField2.Password = "";
                            NewNameField.IsEnabled = true;
                            NewSurnameField.IsEnabled = true;
                            NewPasswdField.IsEnabled = true;
                            NewPasswdField2.IsEnabled = true;
                            UpdateData.IsEnabled = true;

                        }
                        else
                        {
                            count++;
                            int m = 3 - count;
                            String s = $"Невірно введений пароль. Залишилось {m} спроб";

                            MessageBox.Show(s);
                            if (count == 3)
                                System.Windows.Application.Current.Shutdown();
                        }
                    }
                }
            }
            connection.Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(ConnectionString);
            connection.Open();
            string name = NameField.Text;
            string surname = SurnameField.Text;
            string login = loginRegField.Text;
            string password = passwdRegField.Password;
            string password2 = passwdRegField2.Password;
            string Query;
            if (connection.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    if ((password == password2) && (login != "") && (password != "") && LoginReq(login))

                    {
                        Query = "INSERT INTO MainTable ";
                        Query += "VALUES ('" + name + "', '" + surname + "', '" + login + "', '" + password + "','True', 'False'); ";
                        command = new SqlCommand(Query, connection);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Нового користувача створено");
                    }
                    else
                    {
                        MessageBox.Show("Помилка. Можливо користувач з таким логіном вже існує або паролі не співпадають");
                    }
                }
                catch
                {
                    MessageBox.Show("Помилка створення нового користувача");


                }
            }
            connection.Close();
        }

        bool PasswordReq(string Pass)
        {
            byte r1, r2, r3;
            r1 = r2 = r3= 0;
            for (byte i = 0; i < Pass.Length; i++)
            {
                if ((Convert.ToInt32(Pass[i]) >= 65) && (Convert.ToInt32(Pass[i]) <= 65 + 25))
                    r1++;
                if ((Convert.ToInt32(Pass[i]) >= 97) &&  (Convert.ToInt32(Pass[i]) <= 97 + 25))
                    r2++;
                if ((Pass[i] == '+') || (Pass[i] == '-') || (Pass[i] == '*') || (Pass[i] == '/'))
                    r3++;
            }
            return (r1 * r2 * r3 != 0);
        }

        bool LoginReq(string login)
        {
            bool result = true;
            connection = new SqlConnection(ConnectionString);
            string Query = "SELECT Login from MainTable WHERE Login = '"+ login +"'";
            adapter = new SqlDataAdapter(Query, connection);
            dt = new DataTable();
            adapter.Fill(dt);
            connection.Open();
            if (connection.State == System.Data.ConnectionState.Open)
            {

                if (dt.Rows.Count != 0)
                    {
                        result= false;
                    }
                    

            }
            return result;

        }

        private void UpdateData_Click(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(ConnectionString);
            connection.Open();
            string NewName = NewNameField.Text;
            string NewSurname = NewSurnameField.Text;
            string NewPassword = NewPasswdField.Password;
            string NewPassword2 = NewPasswdField2.Password;
            string Query;

            if (connection.State == System.Data.ConnectionState.Open)
            {
                if ((NewPassword == NewPassword2) && (NewPassword != ""))
                {
                    if (Convert.ToBoolean(dt.Rows[0][5]) == true)
                    {
                        if (PasswordReq(NewPassword) == true)
                        {
                            Query = "UPDATE MainTable SET Name='" + NewName + "', ";
                            Query += "Surname='" + NewSurname + "', ";
                            Query += "Password='" + NewPassword + "' ";
                            Query += "WHERE Login='" + login + "';";
                            command = new SqlCommand(Query, connection);
                            command.ExecuteNonQuery();

                            MessageBox.Show("Пароль змінено");
                        }
                        else
                            MessageBox.Show("У паролі немає літер верхнього та нижнього регістрів, а також арифметичних операцій! Спробуйте знову!");
                    }
                    else
                    {
                        Query = "UPDATE MainTable SET Name='" + NewName + "', ";
                        Query += "Surname='" + NewSurname + "', ";
                        Query += "Password='" + NewPassword + "' ";
                        Query += "WHERE Login='" + login + "';";
                        command = new SqlCommand(Query, connection);
                        command.ExecuteNonQuery();
                    }
                }
                else
                {
                    MessageBox.Show("Введено пустий пароль або новий пароль повторно введено некоректно!");               
                }
            }
            connection.Close();
        }



        private void ExitFromSystem_Click(object sender, RoutedEventArgs e)
        {
            loginField.Text = "";
            passwdField.Password = "";
            UpdateData.IsEnabled = false;
            NewNameField.Text = "";
            NewSurnameField.Text = "";
            NewPasswdField.Password = "";
            NewPasswdField2.Password = "";
            NewNameField.IsEnabled = false;
            NewSurnameField.IsEnabled = false;
            NewPasswdField.IsEnabled = false;
            NewPasswdField2.IsEnabled = false;
        }
    }

}
