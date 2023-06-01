using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using Npgsql;

namespace KP_Salon_Rykhlov
{
    public partial class MainWindow : Window
    {
        private string connectionstr;
        public string Role { get; set; }
        private string selected_table;
        private string first_table;
        public MainWindow(string role)
        {
            InitializeComponent();
            this.Role = role;
        }
        public void PermissionSet()
        {
            if (Role == "Системный администратор") 
                connectionstr = "Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=12345;";
            else if (Role == "Диспетчер доставки") {
                connectionstr = "Server=localhost;Port=5432;Database=postgres;User Id=delivery_manager;Password=1488;";
                reg.Visibility = Visibility.Hidden;
                subs_info.Visibility = Visibility.Hidden;
            }
            else if (Role == "Кассир") {
                connectionstr = "Server=localhost;Port=5432;Database=postgres;User Id=cashier;Password=cash228;";
                reg.Visibility = Visibility.Hidden;
            }
            else
                connectionstr = "";
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Cancel.Visibility = Visibility.Hidden;
            Save.Visibility = Visibility.Hidden;
            Load();
            ShowTables("SELECT * FROM \"Salon\"." + first_table);
        }
        private void Load()
        {
            PermissionSet();
            const string sql = "SELECT table_name FROM information_schema.tables WHERE table_schema = 'Salon'";

            try {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionstr))
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(cmd.ExecuteReader());

                        foreach (DataRow row in dataTable.Rows)
                        {
                            Tables.Items.Add(row["table_name"]);
                        }
                        first_table = Tables.Items.GetItemAt(1).ToString();
                        conn.Close();
                    }
                }
            } 
            catch {
                MessageBox.Show("Нет доступа к Базе Данных!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            } 

        }
        private void ShowTables(string sql)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionstr))
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            grid.ItemsSource = dt.DefaultView;
                            conn.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        private void Tables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selected_table = Tables.SelectedValue.ToString();
            ShowTables($"SELECT * FROM \"Salon\".{selected_table}");
        }
        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            var dataTable = ((DataView)grid.ItemsSource)?.Table;
            string sql = $"SELECT * FROM \"Salon\".{selected_table}";

            try
            {
                using (var conn = new NpgsqlConnection(connectionstr))
                {
                    conn.Open();
                    using (var adapter = new NpgsqlDataAdapter())
                    {
                        adapter.SelectCommand = new NpgsqlCommand(sql, conn);

                        var builder = new NpgsqlCommandBuilder(adapter);
                        adapter.InsertCommand = builder.GetInsertCommand();
                        adapter.UpdateCommand = builder.GetUpdateCommand();
                        adapter.DeleteCommand = builder.GetDeleteCommand();

                        adapter.Update(dataTable);

                        MessageBox.Show("Изменения сохранены.");
                        Cancel.Visibility = Visibility.Hidden;
                        Save.Visibility = Visibility.Hidden;

                        ShowTables(sql);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        private void grid_KeyDown(object sender, KeyEventArgs e)
        {
            Save.Visibility = Visibility.Visible;
            Cancel.Visibility = Visibility.Visible;
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            ShowTables($"SELECT * FROM \"Salon\".{selected_table}");
            Cancel.Visibility = Visibility.Hidden;
            Save.Visibility = Visibility.Hidden;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Login window = new Login();
            Close();
            window.Show();
        }

        private void reg_Click(object sender, RoutedEventArgs e)
        {
            Registration window = new Registration(connectionstr, Role);
            if (window.ShowDialog() == true)
            {
                ShowTables("SELECT * FROM \"Salon\".accounts");
                MessageBox.Show("Пользователь успешно зарегистрирован!");
            }
        }

        private void subs_info_Click(object sender, RoutedEventArgs e)
        {
            sub_info window = new sub_info(connectionstr, Role);
            if (window.ShowDialog() == true)
            {
                DataTable dataTable = new DataTable();
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionstr))
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * from \"Salon\".get_subscriber_info(@name::varchar, @surname::varchar)", conn))
                    {
                        cmd.Parameters.AddWithValue("@name", window.name.Text);
                        cmd.Parameters.AddWithValue("@surname", window.surname.Text);

                        NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(cmd);
                        dataAdapter.Fill(dataTable);
                        grid.ItemsSource = dataTable.DefaultView;
                    }
                }
            }   
        }
    }
}
