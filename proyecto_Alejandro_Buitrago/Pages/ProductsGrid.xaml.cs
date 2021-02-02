using proyecto_Alejandro_Buitrago.Images;
using proyecto_Alejandro_Buitrago.ProductClass;
using proyecto_Alejandro_Buitrago.ProjectDB.MySQLData;
using proyecto_Alejandro_Buitrago.ProjectDB.SQLData.LocalImage;
using proyecto_Alejandro_Buitrago.XML;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml.Linq;

namespace proyecto_Alejandro_Buitrago.Pages
{
    /// <summary>
    /// Lógica de interacción para ProductsGrid.xaml
    /// </summary>
    public partial class ProductsGrid : Page
    {

        ProductHandler productHandler;
        private XDocument xml = XDocument.Load("../../XML/xml.xml");
        ObservableCollection<Product> listaFiltrada;
        public Product product;

        public ProductsGrid(ProductHandler productHandler)
        {
            InitializeComponent();
            this.productHandler = productHandler;
            UpdateProductList();
            InitCategoryCombo();
        }

        private void InitCategoryCombo()
        {
            categoryCMB.Items.Add("Todas...");
            var listaCategoriasXML = xml.Root.Elements("Tipo").Attributes("IdTipo");
            foreach (XAttribute categoriaXML in listaCategoriasXML)
            {
                categoryCMB.Items.Add(categoriaXML.Value);
            }
            categoryCMB.SelectedIndex = 0;
        }


        private void UpdateProductList()
        {
            productHandler.UpdateProductList();
            categoryCMB.SelectedIndex = 0;
            busquedaTextBoz.Text = "";
            listaFiltrada = new ObservableCollection<Product>(productHandler.productList);
            MyDataGrid.ItemsSource = productHandler.productList;
            MyDataGrid.DataContext = productHandler.productList;
            MyDataGrid.Items.Refresh();

        }

        private void categoryCMB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (categoryCMB.SelectedIndex == 0)
            {
                UpdateProductList();
            }
            else
            {
                listaFiltrada.Clear();

                foreach (Product product in productHandler.productList)
                {
                    if (product.tipo.Equals((string)categoryCMB.SelectedItem))
                    {
                        listaFiltrada.Add(product);
                    }
                }
                MyDataGrid.ItemsSource = listaFiltrada;
                MyDataGrid.DataContext = listaFiltrada;
                MyDataGrid.Items.Refresh();
            }

        }

        private void busquedaTextBoz_TextChanged(object sender, TextChangedEventArgs e)
        {
            ObservableCollection<Product> nuevaListaFiltrada = new ObservableCollection<Product>();
            foreach (Product product in listaFiltrada)
            {
                if (product.GetAllValues().Contains(busquedaTextBoz.Text))
                {
                    nuevaListaFiltrada.Add(product);
                }
            }
            MyDataGrid.ItemsSource = nuevaListaFiltrada;
            MyDataGrid.DataContext = nuevaListaFiltrada;
            MyDataGrid.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateProductList();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Product product = (Product)MyDataGrid.SelectedItem;
            MainWindow.myNavigationFrame.NavigationService.Navigate(new ModydDelete(productHandler));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Product product = (Product)MyDataGrid.SelectedItem;
            XMLHandler.DeleteProduct(product.referencia);
            UpdateProductList();
            LocalImageDBHandler.removeDataFromDB(product.referencia);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

            MessageBoxResult resultado = MessageBox.Show("¿Está seguro de que quiere borrar todo este tipo?",
                                "ATENCIÓN", MessageBoxButton.YesNo, MessageBoxImage.Warning);


            switch (resultado)
            {
                case MessageBoxResult.Yes:
                    
                    Product product = (Product)MyDataGrid.SelectedItem;
                    XMLHandler.DeleteType(product.tipo);
                    UpdateProductList();
                    MessageBox.Show("Tipo borrado con éxito");
                    break;

                case MessageBoxResult.Cancel:
                    break;
            }
        
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

            Product product = (Product)MyDataGrid.SelectedItem;
            if (product.publish == false) 
            {
                MessageBoxResult resultado = MessageBox.Show("¿Quiere añadir el producto a la base de datos?",
                                "ATENCIÓN", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                projectDBHandler.AddDataToDB(product.referencia, product.descripcion, product.medida, product.precio, product.fecha, product.stock, product.tipo, product.madera, product.imagen);
                product.publish = true;
                XMLHandler.ModifyProduct(product);
                UpdateProductList();
                MessageBox.Show("Añadido con éxito a la base de datos");
            }
            else
            {
                MessageBoxResult resultado = MessageBox.Show("¿Seguro que quiere borrar el producto de la base de datos?",
                                "ATENCIÓN", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                switch (resultado)
                {
                    case MessageBoxResult.Yes:
                        //LocalImageDBHandler.removeDataFromDB(product.referencia);
                        projectDBHandler.removeDataFromDB(product.referencia, product.descripcion, product.medida, product.precio, product.fecha, product.stock, product.tipo, product.madera, product.imagen);
                        product.publish = false;
                        XMLHandler.ModifyProduct(product);
                        UpdateProductList();
                        MessageBox.Show("Borrado con éxito de la base de datos");
                        break;

                    case MessageBoxResult.No:
                        break;
                }
            }

            
        }
    }
    
    
}
