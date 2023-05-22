using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
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
using Npgsql;

namespace KP_Salon_Rykhlov
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private static string connectionstr = "Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=12345;";
        public string log;
        public string hpass;
        public string salt;
        public int staff_id;
        public string role;
        private void SignIn_Button_Click(object sender, RoutedEventArgs e)
        {
            if (LoginBox.Text.Length > 0 & PasswordBox.ToString().Length > 0)
            {
                GetUser();
                if (LoginBox.Text == log)
                {
                    string pass = HashPassword(PasswordBox.Password.ToString(), salt);
                    if (pass == hpass)
                    {
                        Getrole();
                        MainWindow window = new MainWindow(role);
                        Close();
                        window.Show();
            
                    }
                    else
                    {
                        MessageBox.Show("Неверный пароль!");
                    }
                }
                else
                {
                    MessageBox.Show("Такого пользователя не существует!");
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля!");
            }
        }
        public void GetUser()
        {
            try
            {
                string sql = $"Select * from \"Salon\".accounts WHERE username = '{LoginBox.Text}'";
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionstr))
                {
                    connection.Open();
                    try
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand())
                        {
                            cmd.Connection = connection;
                            cmd.CommandText = sql;
                            using (NpgsqlDataReader dataReader = cmd.ExecuteReader())
                            {
                                while (dataReader.Read())
                                {
                                    log = dataReader.GetString(1);
                                    hpass = dataReader.GetString(2);
                                    salt = dataReader.GetString(3);
                                    staff_id = dataReader.GetInt16(4);
                                }
                                connection.Close();
                            }
                        }
                    }
                    catch (Exception ex) {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            catch
            {
                MessageBox.Show("Нет доступа к Базе Данных!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void Getrole()
        {
            try
            {
                string sql = $"select positions_name from \"Salon\".positions p join \"Salon\".staff s on p.position_id = s.position_id where staff_id = {staff_id}";
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionstr))
                {
                    connection.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection))
                    {
                        using (NpgsqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                role = dataReader.GetString(0);
                            }
                            connection.Close();
                        }
                    }
                    
                }
            }
            catch
            {
                MessageBox.Show("Нет доступа к Базе Данных!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static string HashPassword(string password, string salt)
        {
            string combinedString = password + salt;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(combinedString);
                byte[] hash = sha256.ComputeHash(bytes);

                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    stringBuilder.Append(hash[i].ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }
    }
}
