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
using Big_Pack.Windows;

namespace Big_Pack
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        internal void Load_Data(string a) // класс доступен из всего проекта
        {
            materials_list.Children.Clear();
            using (SqlConnection connection = new SqlConnection(Connect_bd.String)) // берем строку подключения из класса Connect_bd
            {
                connection.Open();
                SqlCommand command = new SqlCommand($@"SELECT Material.[ID]
                                                             ,MaterialType.Title
                                                             ,Material.[Title]
                                                             ,Material.[CountInPack]
                                                             ,Material.[Unit]
                                                             ,Material.[CountInStock]
                                                             ,Material.[MinCount]
                                                             ,Material.[Description]
                                                             ,Material.[Cost]
                                                             ,Material.[Image]
                                                             ,Supplier.Title
                                                          FROM [dbo].[Material]

                                            INNER JOIN MaterialType ON Material.MaterialTypeID = MaterialType.ID


                                            INNER JOIN Supplier ON Material.ID = Supplier.ID

                   
                                            where (Material.[Title] like '%{Seach.Text}%' or Material.[Description] like '%{Seach.Text}%')
" + a, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Materials_Control materials = new Materials_Control();
                        materials.label_ID.Content = reader[0];
                        materials.label_type_and_title.Content = $"{reader[1]} | {reader[2]}";
                        materials.label_CountInPack.Content = reader[3];
                        materials.label_CountInStock_and_unit.Content = $"Остаток: {reader[5]} шт";
                        materials.label_MinCount_and_unit.Content = $"Минимальное количество: {reader[6]} {reader[4]}";
                        materials.label_Description.Content = reader[7];
                        materials.label_cost.Content = reader[8];
                        materials.image_Logo.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\" + reader[9]));
                        materials.label_Supplier.Text = $"Поставщики: {reader[10]}";
                        materials.Main = this;
                        materials_list.Children.Add(materials);

                    }

                }
                Load_ID("");  //выводим кол-во всех данных
            }

        }
        private void btn_UP_Click(object sender, RoutedEventArgs e)
        {
            page_scroll.PageUp();
        }

        private void btn_DOWN_Click(object sender, RoutedEventArgs e)
        {
            page_scroll.PageDown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Load_Data("");
        }

        private void Seach_SelectionChanged(object sender, RoutedEventArgs e)
        {
            Load_Data("");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Add_Materials material_add = new Add_Materials();
            material_add.Show();
            this.Hide();
        }

        internal void Load_ID(string s)
        {

            using (SqlConnection connection2 = new SqlConnection(Connect_bd.String)) // берем строку подключения из класса Connect_bd
            {
                connection2.Open();
                SqlCommand command2 = new SqlCommand($@"SELECT COUNT([ID]) FROM [dbo].[Material];" +s, connection2);
                SqlDataReader reader2 = command2.ExecuteReader();
                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {
                        Count_id main2 = new Count_id();
                        main2.id_count.Content = reader2[0];
                        id_count_wrap.Children.Add(main2);
                    }
                }

            }
        }

        private void id_count_Loaded(object sender, RoutedEventArgs e)
        {
            Load_ID("");
        }

        private void id_count_wrap_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}