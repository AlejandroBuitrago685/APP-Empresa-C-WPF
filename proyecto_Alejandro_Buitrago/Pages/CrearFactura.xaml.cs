using proyecto_Alejandro_Buitrago.ClientClass;
using proyecto_Alejandro_Buitrago.ProductClass;
using proyecto_Alejandro_Buitrago.ProjectDB.SQLData.Clientes;
using proyecto_Alejandro_Buitrago.Reports;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private Regex valCantidad = new Regex(@"^\d+$");
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
            Warning.Visibility = Visibility.Hidden;

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
        }

        private void Añadir_Click(object sender, RoutedEventArgs e)
        {
            

            if (valCantidad.IsMatch(Cantidad.Text) && comboProductos.SelectedIndex >= 0)
            {
                Product producto = (Product)comboProductos.SelectedItem;
                if (!listaProductos2.Contains(producto))
                {
                    producto.cantidad = int.Parse(Cantidad.Text);
                    listaProductos2.Add(producto);
                    MyDataGrid.Items.Refresh();
                }
                else
                {
                    producto.cantidad = producto.cantidad + int.Parse(Cantidad.Text);
                    MyDataGrid.Items.Refresh();
                }
            }
            else
            {
                Warning.Visibility = Visibility.Visible;
                Warning.Text = "Seleccione un producto e introduzca un valor numérico";
            }

            
        }

        private void Crear_Click(object sender, RoutedEventArgs e)
        {
            if(Validation())
            {
                string nFactura2 = nFactura.Text;


                if (!ClientesDBHandler.ExisteCIF(client.cif))
                {
                    ClientesDBHandler.AddCliente(client);
                }

                ClientesDBHandler.AddFactura(client, listaProductos2, nFactura.Text);

                ReportPreviewCreado reportPreview = new ReportPreviewCreado();
                bool okQuery = reportPreview.GenerarInforme(nFactura2);

                if (okQuery)
                {
                    MessageBox.Show("Factura creada correctamente correctamente",
                                "ATENCIÓN", MessageBoxButton.OK, MessageBoxImage.Information);

                    reportPreview.Show();

                }

            }
            else
            {
                Warning.Visibility = Visibility.Visible;
            }
        }

        private bool Validation()
        {
            bool validacion = false;

            foreach (UIElement element in clientGrid.Children)
            {
                if (element is TextBox)
                {
                    TextBox textBox = (TextBox)element;
                    if (textBox.Text.Equals(""))
                    {
                        textBox.BorderBrush = new SolidColorBrush(Colors.Red);
                        validacion = false;
                    }
                    else
                    {
                        textBox.BorderBrush = new SolidColorBrush(Colors.LightGray);
                    }
                }
            }

            if (nFactura.Text == "")
            {
                Warning.Text = "Introduzca un número de factura";
                nFactura.BorderBrush = new SolidColorBrush(Colors.Red);
                validacion = false;
            }
            else if (MyDataGrid.Items.Count <= 0)
            {
                Warning.Text = "Seleccione al menos un producto";
                comboProductos.BorderBrush = new SolidColorBrush(Colors.Red);
                Cantidad.BorderBrush = new SolidColorBrush(Colors.Red);
                MyDataGrid.BorderBrush = new SolidColorBrush(Colors.Red);
                validacion = false;
            }
            else if (client == null)
            {
                validacion = false;
            }
            else if (ClientesDBHandler.ExisteFactura(nFactura.Text))
            {
                validacion = false;
                Warning.Text = "Ya existe una factura con esa referencia";
            }
            else
            {
                validacion = true;
            }

            return validacion;
        }
    }
}
