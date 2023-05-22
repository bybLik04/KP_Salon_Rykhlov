using Npgsql;
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
    /// Логика взаимодействия для sub_info.xaml
    /// </summary>
    public partial class sub_info : Window
    {
        public string connectionstr { get; set; }
        public string Role { get; set; }
        public sub_info(string connstr, string role)
        {
            InitializeComponent();
            connectionstr = connstr;
            Role = role;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            //DataTable dataTable = new DataTable();
            //if (surname.Text.Length > 0 & name.Text.Length > 0)
            //{
            //    try
            //    {
            //        using (NpgsqlConnection conn = new NpgsqlConnection(connectionstr))
            //        {
            //            conn.Open();
            //            using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT \"Salon\".get_subscriber_info(@surname::varchar, @name::varchar)", conn))
            //            {
            //                cmd.Parameters.AddWithValue("@surname", surname.Text);
            //                cmd.Parameters.AddWithValue("@name", name.Text);

            //                NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(cmd);
            //                dataAdapter.Fill(dataTable);

            //                using (NpgsqlDataReader reader = cmd.ExecuteReader())
            //                {
            //                    var main = new MainWindow(Role);
            //                    DataTable dt = new DataTable();
            //                    dt.Load(reader);
            //                    main.grid.ItemsSource = dt.DefaultView;
            //                    conn.Close();
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //        return;
            //    }
            //}
        }
    }
}
