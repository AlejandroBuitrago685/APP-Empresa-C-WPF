using proyecto_Alejandro_Buitrago.ClientClass;
using proyecto_Alejandro_Buitrago.ProductClass;
using proyecto_Alejandro_Buitrago.ProjectDB.SQLData.Clientes;
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
        Client client;
        ProductHandler productHandler;
        ObservableCollection<Product> listaProductos;
        ObservableCollection<Product> listaProductos2;

        public CrearFactura(ProductHandler productHandler)
        {
            InitializeComponent();
            client = new Client();
            this.productHandler = productHandler;
            this.clientGrid.DataContext = client;
            InitCategoryCombo();
            listaProductos2 = new ObservableCollection<Product>();
            MyDataGrid.ItemsSource = listaProductos2;

        }
        private void InitCategoryCombo()
        {
            productHandler.UpdateProductList();
            listaProductos = new ObservableCollection<Product>(productHandler.productList);
            comboProductos.ItemsSource = productHandler.productList;
            comboProductos.DataContext = productHandler.productList;
            comboProductos.Items.Refresh();
        }

        private void comboProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            producto = (Product)comboProductos.SelectedItem;
            Cantidad.DataContext = producto;
        }

        private void Añadir_Click(object sender, RoutedEventArgs e)
        {
            bool productoR = false;
            Product product = (Product)comboProductos.SelectedItem;
            Cantidad.DataContext = product;
            listaProductos2.Add(product);
            
            if(product != null)
            {
                foreach (Product p in listaProductos2)
                {
                    if (p.referencia == product.referencia)
                    {
                        productoR = true;
                        p.cantidad = p.cantidad + int.Parse(Cantidad.Text);
                    }
                }
                if (!productoR)
                {
                    listaProductos2.Add(product);
                }
            }
            comboProductos.SelectedIndex = -1;
            MyDataGrid.Items.Refresh();
        }

        private void Crear_Click(object sender, RoutedEventArgs e)
        {
            if(listaProductos2.Count>0 && nFactura.Text != "" && client != null)
            {
                ClientesDBHandler.AddCliente(client);
                ClientesDBHandler.AddFactura(client, listaProductos2, nFactura.Text);
            }
        }
    }
}
