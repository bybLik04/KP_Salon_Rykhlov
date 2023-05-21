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

        public static string connectionstr = "Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=12345;";
        public string log = "";
        public string hpass = "";
        public string salt = "";
        public string role = "";
        private void SignIn_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string sql = "Select * from Users WHERE [Login] = '" + LoginBox.Text + "';";
                NpgsqlConnection connection = new NpgsqlConnection(connectionstr);
                connection.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
                NpgsqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    log = dataReader.GetString(1);
                    hpass = dataReader.GetString(2);
                    salt = dataReader.GetString(3);
                    role = dataReader.GetString(4);
                }
                connection.Close();
            }
            catch
            {
                MessageBox.Show("Нет доступа к Базе Данных!");
                return;
            }
            if (LoginBox.Text.Length > 0 & PasswordBox.ToString().Length > 0)
            {
                if (LoginBox.Text == log)
                {
                    if (ComputeHash(PasswordBox.Password.ToString(), new SHA256CryptoServiceProvider(), salt) == hpass)
                    {
                        MainWindow window = new MainWindow();
                        window.Show();
                        this.Hide();
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

            //if (user_data.Items.Count > 1)
            //{
            //    try
            //    {
            //        string sql2 = "Select * from Users WHERE [Login] = '" + LoginBox.Text + "' AND [Password] = '" + PasswordBox.Password.ToString() + "' AND admin;";
            //        DataTable table2 = new DataTable();
            //        OleDbConnection connection2 = new OleDbConnection(connectionstring);
            //        connection2.Open();
            //        OleDbCommand oleDbCommand2 = new OleDbCommand(sql2, connection2);
            //        OleDbDataAdapter dataAdapter2 = new OleDbDataAdapter(oleDbCommand2);
            //        dataAdapter2.Fill(table2);
            //        user_data2.ItemsSource = table2.DefaultView;
            //        connection2.Close();
            //    }
            //    catch
            //    {
            //        MessageBox.Show("Нет доступа к Базе Данных!");
            //        return;
            //    }
            //    if (user_data2.Items.Count == 2)
            //    {
            //        MainWindow mainWindow = new MainWindow();
            //        mainWindow.Show();
            //        this.Hide();
            //    }
            //    else
            //    {
            //        MainWindow mainWindow = new MainWindow();
            //        mainWindow.Show();

                //        mainWindow.Std_num_lbl.Visibility = Visibility.Hidden;
                //        mainWindow.StudentNumText.Visibility = Visibility.Hidden;
                //        mainWindow.New_Lbl.Visibility = Visibility.Hidden;
                //        mainWindow.ChangeText.Visibility = Visibility.Hidden;
                //        mainWindow.UpdateBtn.Visibility = Visibility.Hidden;
                //        mainWindow.DeleteBtn.Visibility = Visibility.Hidden;
                //        mainWindow.AddBtn.Visibility = Visibility.Hidden;
                //        mainWindow.log_lbl.Visibility = Visibility.Hidden;
                //        mainWindow.log_txt.Visibility = Visibility.Hidden;
                //        mainWindow.add_adm_Btn.Visibility = Visibility.Hidden;
                //        mainWindow.del_adm_Btn.Visibility = Visibility.Hidden;
                //        this.Hide();
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("Такого пользователя не существует!");
                //}
        }

        public static string ComputeHash(string input, HashAlgorithm algorithm, string salt2)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] saltBytes = Encoding.UTF8.GetBytes(salt2);
            // Combine salt and input bytes
            Byte[] saltedInput = new Byte[saltBytes.Length + inputBytes.Length];
            saltBytes.CopyTo(saltedInput, 0);
            inputBytes.CopyTo(saltedInput, saltBytes.Length);

            Byte[] hashedBytes = algorithm.ComputeHash(saltedInput);

            return BitConverter.ToString(hashedBytes);
        }

        private void Reg_Button_Click(object sender, RoutedEventArgs e)
        {
            Registration regwindow = new Registration();
            regwindow.Show();
            this.Hide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
