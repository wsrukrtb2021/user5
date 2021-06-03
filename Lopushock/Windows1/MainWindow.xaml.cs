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
using System.IO;
using System.Data.SqlClient;
using Lopushock.Class1;
using Lopushock.Usercontrol1;


namespace Lopushock
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
        internal void Load_Data(string a)
        {
            paper_list.Children.Clear();
            using (SqlConnection connection = new SqlConnection(Connect.String))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($@"SELECT        Product.ID, Product.Title AS Expr1, MaterialType.Title AS Expr2, Product.ArticleNumber, Product.Description, Product.Image, Product.ProductionPersonCount, Product.ProductionWorkshopNumber, Product.MinCostForAgent, 
                                                                      Material.Cost,Material.Title
                                                                     FROM            Product INNER JOIN
                                                                     Material ON Product.ID = Material.ID INNER Join 
						ProductType on Product.ProductTypeID = ProductType.ID INNER JOIN 
						MaterialType on Material.MaterialTypeID = MaterialType.ID 
where (Product.Title like '%{Search.Text}%' OR MaterialType.Title like '%{Search.Text}%')" + a, connection);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    while (reader.Read())
                    {
                        PaperUC paper = new PaperUC();
                        paper.type_label.Content = reader[2];
                        paper.name_label.Content = reader[1].ToString();
                        paper.article_label.Content = reader[3];
                        try
                        {
                            paper.image_logo.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\" + reader[5].ToString().Replace("jpg","jpeg")));
                        }
                        catch
                        {
                        }
                        paper.person_label.Content = reader[6];
                        paper.workshop_label.Content = reader[7];
                        paper.minimum.Content = reader[8];
                        paper.descriprion_label.Content = reader[4];
                        paper.material_label.Content =reader[10];
                        paper.cost_label.Content = reader[9];
                        paper.label_id.Content = reader[0];
                        paper_list.Children.Add(paper);
                    }
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(Filtr.SelectedItem != null)
            Load_Data("");
        }

        private void pageupbutton_Click(object sender, RoutedEventArgs e)
        {
            paper_scroll.PageUp();
        }

        private void pagedonwbutton_Click(object sender, RoutedEventArgs e)
        {
            paper_scroll.PageDown();
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            Load_Data("");
        }

        private void Filtr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Load_Data("");
        }

        private void Sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Load_Data("");
        }
    }
}
