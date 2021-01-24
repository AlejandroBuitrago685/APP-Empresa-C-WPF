using proyecto_Alejandro_Buitrago.ProductClass;
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
    /// Lógica de interacción para ModydDelete.xaml
    /// </summary>
    /// 
   
    public partial class ModydDelete : Page
    {
       
        private Product producto;
        ProductHandler productHandler;
        ObservableCollection<Product> listaFiltrada;
        private XDocument xml = XDocument.Load("../../XML/xml.xml");
        public ModydDelete(ProductHandler productHandler)
        {
            InitializeComponent();
            this.productHandler = productHandler;
            InitCategoryCombo();
            this.DataContext = this;
        }

        private void InitCategoryCombo()
        {
            productHandler.UpdateProductList();
            listaFiltrada = new ObservableCollection<Product>(productHandler.productList);
            comboProduct.ItemsSource = productHandler.productList;
            comboProduct.DataContext = productHandler.productList;
            comboProduct.Items.Refresh();
        }

        public void deleteProduct (int pos)
        {
            listaFiltrada = new ObservableCollection<Product>(productHandler.productList);
            listaFiltrada.RemoveAt(pos);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int pos = comboProduct.SelectedIndex;
            MessageBoxResult resultado = MessageBox.Show("¿Está seguro de que quiere borrar este producto?",
                                "ATENCIÓN", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            switch (resultado)
            {
                case MessageBoxResult.Yes:

                    XMLHandler.DeleteProduct(producto.referencia);
                    deleteProduct(pos);
                    MessageBox.Show("Tipo borrado con éxito");
                    comboProduct.Items.Refresh();
                    break;

                case MessageBoxResult.Cancel:
                    break;
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (comboProduct.SelectedIndex >=0)
            {
                int pos = comboProduct.SelectedIndex;
                MainWindow.myNavigationFrame.NavigationService.Navigate(new AddProduct("MODIFICAR PRODUCTO", productHandler, producto, pos));
            } else
            {
                Warning.Visibility = Visibility.Visible;
            }
        }

        private void comboProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            producto = (Product)comboProduct.SelectedItem;
            this.productGrid.DataContext = producto;
        }
    }
}
