using System;
using System.Collections.Generic;
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

namespace KP_Salon_Rykhlov
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }
        public string connectionstring = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + @"C:\Users\user1\source\repos\Lab1_Rykhlov\Колледж(для студентов).mdb;Persist Security Info=False";



        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            Login loginwindow = new Login();
            loginwindow.Show();
            this.Hide();
        }

        private void Reg_Button2_Click(object sender, RoutedEventArgs e)
        {
            //if (LoginBox2.Text.Length > 0 & PasswordBox2.ToString().Length > 0 & PasswordBox2_repeat.ToString().Length > 0)
            //{
            //    if (PasswordBox2.Password == PasswordBox2_repeat.Password)
            //    {
            //        //string sql = "Select * from Users ;"; //WHERE Login = '" + LoginBox2.Text + "'
            //        try
            //        {
            //            OleDbConnection connection = new OleDbConnection(connectionstring);
            //            connection.Open();
            //            OleDbCommand oleDbCommand = connection.CreateCommand();
            //            string sql = $"SELECT * FROM Users WHERE Login = log;";
            //            oleDbCommand.CommandText = sql;
            //            oleDbCommand.Parameters.Add("log", OleDbType.VarChar, 255).Value = LoginBox2.Text;
            //            var exist = oleDbCommand.ExecuteScalar();
            //            //Data_Load(sql);
            //            if (exist is null)
            //            {
            //                try
            //                {
            //                    connection.Close();
            //                    string sql2 = "SELECT * FROM Users;";
            //                    DataTable table2 = new DataTable();
            //                    OleDbConnection connection2 = new OleDbConnection(connectionstring);
            //                    connection.Open();
            //                    OleDbCommand oleDbCommand2 = connection2.CreateCommand();
            //                    oleDbCommand.CommandText = "INSERT INTO Users([Login], [Password]) values('" + LoginBox2.Text + "', '" + PasswordBox2.Password.ToString() + "')";
            //                    oleDbCommand.ExecuteNonQuery();
            //                    OleDbCommand command = new OleDbCommand(sql2, connection2);
            //                    OleDbDataAdapter dataAdapter2 = new OleDbDataAdapter(command);
            //                    dataAdapter2.Fill(table2);
            //                    connection2.Close();

            //                }
            //                catch
            //                {
            //                    MessageBox.Show("Нет доступа к Базе Данных!");
            //                    return;
            //                }

            //                MessageBox.Show("Вы успешно зарегистрировались!");

            //                MainWindow mainWindow = new MainWindow();
            //                mainWindow.Show();
            //                mainWindow.Std_num_lbl.Visibility = Visibility.Hidden;
            //                mainWindow.StudentNumText.Visibility = Visibility.Hidden;
            //                mainWindow.New_Lbl.Visibility = Visibility.Hidden;
            //                mainWindow.ChangeText.Visibility = Visibility.Hidden;
            //                mainWindow.UpdateBtn.Visibility = Visibility.Hidden;
            //                mainWindow.DeleteBtn.Visibility = Visibility.Hidden;
            //                mainWindow.AddBtn.Visibility = Visibility.Hidden;
            //                mainWindow.log_lbl.Visibility = Visibility.Hidden;
            //                mainWindow.log_txt.Visibility = Visibility.Hidden;
            //                mainWindow.add_adm_Btn.Visibility = Visibility.Hidden;
            //                mainWindow.del_adm_Btn.Visibility = Visibility.Hidden;
            //                this.Hide();
            //            }
            //            else
            //            {
            //                MessageBox.Show("Пользователь существует");
            //            }
            //        }
            //        catch
            //        {
            //            MessageBox.Show("Нет соединения с Базой Данных");
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Пароли не совпадают");
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Заполните все поля!");
            //}
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
