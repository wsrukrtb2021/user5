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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.IO;
using Big_Pack.Classes;
using Big_Pack.User_Controls;

namespace Big_Pack.User_Controls
{
    /// <summary>
    /// Логика взаимодействия для Materials_Control.xaml
    /// </summary>
    public partial class Materials_Control : UserControl
    {
        public Materials_Control()
        {
            InitializeComponent();
        }
        public MainWindow Main;
     
        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Connect_bd.String)) // берем строку подключения из класса Connect_bd
            {
                connection.Open();
                SqlCommand command = new SqlCommand($@"DELETE FROM [dbo].[Material] WHERE ID = '{label_ID.Content}'", connection);
                command.ExecuteNonQuery();
                Main.Load_Data("");
            }
        }
    }
}
