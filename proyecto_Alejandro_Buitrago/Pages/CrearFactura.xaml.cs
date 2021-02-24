using proyecto_Alejandro_Buitrago.ProductClass;
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

namespace proyecto_Alejandro_Buitrago.Pages
{
    /// <summary>
    /// Lógica de interacción para CrearFactura.xaml
    /// </summary>
    public partial class CrearFactura : Page
    {

        private Product producto;
        ProductHandler productHandler;
        ObservableCollection<Product> listaFiltrada;

        public CrearFactura(ProductHandler productHandler)
        {
            InitializeComponent();
            this.productHandler = productHandler;
            InitCategoryCombo();

        }
        private void InitCategoryCombo()
        {
            productHandler.UpdateProductList();
            listaFiltrada = new ObservableCollection<Product>(productHandler.productList);
            comboProductos.ItemsSource = productHandler.productList;
            comboProductos.DataContext = productHandler.productList;
            comboProductos.Items.Refresh();
        }

        private void comboProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            producto = (Product)comboProductos.SelectedItem;
        }
    }
}
