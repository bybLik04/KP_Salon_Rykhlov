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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;

namespace KP_Salon_Rykhlov
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        private static string connectionstr = "Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=12345;";
        public MainWindow()
        {
            InitializeComponent();
            open();
        }
        private void show(string nametable)
        {
            using (NpgsqlConnection conn= new NpgsqlConnection(connectionstr))
            {

                conn.Open();
                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "select * from \"Salon\"." + nametable;
                    using(NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        grid.ItemsSource = dt.DefaultView;
                    }
                }
            }
        }
        private void open()
        {
            show("staff");
        }
    }
}
