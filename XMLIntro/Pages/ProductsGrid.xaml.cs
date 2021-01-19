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
using System.Windows.Shapes;
using System.Xml.Linq;
using XMLIntro.ProductsClass;

namespace XMLIntro.Pages
{
    /// <summary>
    /// Lógica de interacción para ProductsGrid.xaml
    /// </summary>
    public 
       partial class ProductsGrid : Page
    {
        ProductHandler productHandler;
        private XDocument xml = XDocument.Load("../../XML/XMLFile1.xml");
        ObservableCollection<Product> listaFiltrada;

        public ProductsGrid(ProductHandler productHandler)
        {
            InitializeComponent();
            this.productHandler = productHandler;
            UpdateProductList();
            InitCategoryCombo();
        }

        private void InitCategoryCombo()
        {
            categoryCMB.Items.Add("Todo...");
            var listaCategoriasXML = xml.Root.Elements("Categoria").Attributes("IdCategoria");
            foreach(XAttribute categoriaXML in listaCategoriasXML)
            {
                categoryCMB.Items.Add(categoriaXML.Value);
            }
            categoryCMB.SelectedIndex = 0;
        }

        private void UpdateProductList()
        {
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
            } else
            {
                listaFiltrada.Clear();

                foreach (Product product in productHandler.productList)
                {
                    if (product.category.Equals((string)categoryCMB.SelectedItem))
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
            //MainWindow.MyNavigationFrame.NavigationService.Navigate(new PaginationProgressEventArgs de modificar());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Product product = (Product)MyDataGrid.SelectedItem;
            //XMLHandler.RemoveProduct(product.productRef);
            UpdateProductList();
        }
    }
}
