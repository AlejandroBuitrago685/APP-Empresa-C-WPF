using proyecto_Alejandro_Buitrago.ProductClass;
using proyecto_Alejandro_Buitrago.XML;
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
using System.Xml.Linq;

namespace proyecto_Alejandro_Buitrago.Pages
{
    /// <summary>
    /// Lógica de interacción para AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Page
    {

       
        private XDocument xml = XDocument.Load("../../XML/xml.xml");
        public ProductHandler productHandler1;
        public Product product1;
        int posicion;
        public bool modificacion;
        public bool validacion = false;

        public AddProduct(String tituloPrincipal, ProductHandler productHandler, Product product, int pos)
        {
            
            InitializeComponent();
            titulo.Text = tituloPrincipal;
            this.productHandler1 = productHandler;
            this.product1 = product;
            productGrid.DataContext = product;
            this.posicion = pos;
            InitCategoriaCMB();
            Ref.Text = product.referencia;
            Descripcion.Text = product.descripcion;
            TipoCMB.Text = product.tipo;
            MarcaCMB.Text = product.madera;
            MedidaCMB.Text = product.medida;
            modificacion = true;
            Ref.IsEnabled = false;
            brandCheck.IsEnabled = false;
            medidaCheck.IsEnabled = false;
            categoryCheck.IsEnabled = false;

        }
        public AddProduct()
        {
            InitializeComponent();
            InitCategoriaCMB();
            modificacion = false;
        }

        private void InitCategoriaCMB()
        {
            var listaCategorias = xml.Root.Elements("Tipo").Attributes("IdTipo");
            for (int i = 0; i < listaCategorias.Count(); i++)
            {
                TipoCMB.Items.Add(listaCategorias.ElementAt(i).Value);
            }
        }

        private void TipoCMB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MarcaCMB.Items.Clear();
            var listaMarcas = xml.Root.Elements("Tipo").ElementAt(TipoCMB.SelectedIndex).Elements().Attributes("IdNombre");
            for (int i = 0; i < listaMarcas.Count(); i++)
            {
                MarcaCMB.Items.Add(listaMarcas.ElementAt(i).Value);
            }

        }

        private void MarcaCMB_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            MedidaCMB.Items.Clear();
            var listaMarcas = xml.Root.Elements("Tipo").ElementAt(TipoCMB.SelectedIndex).Elements("Madera").Elements("Producto").Attributes("Medida");
            for (int i = 0; i < listaMarcas.Count(); i++)
            {
                MedidaCMB.Items.Add(listaMarcas.ElementAt(i).Value);
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (categoryBox.IsVisible)
            {
                categoryBox.Visibility = Visibility.Hidden;
                brandBox.Visibility = Visibility.Hidden;
                medidaBox.Visibility = Visibility.Hidden;
                brandCheck.IsEnabled = true;
                medidaCheck.IsEnabled = true;
            }
            else
            {
                categoryBox.Visibility = Visibility.Visible;
                brandBox.Visibility = Visibility.Visible;
                medidaBox.Visibility = Visibility.Visible;
                brandCheck.IsEnabled = false;
                medidaCheck.IsEnabled = false;
            }
        }

        private void brandCheck_Checked(object sender, RoutedEventArgs e)
        {
            if (brandBox.IsVisible)
            {
                brandBox.Visibility = Visibility.Hidden;
                medidaBox.Visibility = Visibility.Hidden;
                brandCheck.IsEnabled = true;
                medidaCheck.IsEnabled = true;
            }
            else
            {
                brandBox.Visibility = Visibility.Visible;
                medidaBox.Visibility = Visibility.Visible;
                medidaCheck.IsEnabled = false;
            }
        }

        private void medidaCheck_Checked(object sender, RoutedEventArgs e)
        {
            if (medidaBox.IsVisible)
            {
                medidaBox.Visibility = Visibility.Hidden;
            }
            else
            {
                medidaBox.Visibility = Visibility.Visible;
            }
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            Validation();

            if (validacion)
            {
                Warning.Visibility = Visibility.Visible;
            }
            else
            {
                if (modificacion)
                {

                    XMLHandler.ModifyProduct(product1);
                    MainWindow.myNavigationFrame.NavigationService.Navigate(new Inicio());

                }
                else
                {
                    String tipo = null;
                    String madera = null;
                    String medida = null;

                    if ((bool)categoryCheck.IsChecked || (bool)brandCheck.IsChecked || (bool)medidaCheck.IsChecked)
                    {

                        tipo = TipoCMB.Text;
                        madera = MarcaCMB.Text;
                        medida = MedidaCMB.Text;

                        if ((bool)categoryCheck.IsChecked)
                        {
                            tipo = categoryBox.Text;
                            madera = brandBox.Text;
                            medida = medidaBox.Text;


                        }
                        if ((bool)brandCheck.IsChecked)
                        {
                            madera = brandBox.Text;
                            medida = medidaBox.Text;

                        }
                        if ((bool)medidaCheck.IsChecked)
                        {
                            medida = medidaBox.Text;
                        }

                    }
                    else
                    {
                        tipo = TipoCMB.Text;
                        madera = MarcaCMB.Text;
                        medida = MedidaCMB.Text;

                    }


                    String descripcion = Descripcion.Text;
                    DateTime fecha = (DateTime)Fecha.SelectedDate;
                    int stock = int.Parse(Stock.Text);
                    float precio = float.Parse(Precio.Text);
                    String referencia = Ref.Text;
                    Product product = new Product(referencia, descripcion, medida, precio, fecha, stock, tipo, madera);
                    XMLHandler.AddXMLProduct(product);

                }
             
            }
            
        }

        public void Validation()
        {

            if (Ref.Text.Length == 0 || Descripcion.Text.Length == 0 || Precio.Text.Length == 0 || Stock.Text.Length == 0 || Fecha.Text.Length == 0)
            {
                validacion = true;
            }

            else if ((bool)categoryCheck.IsChecked & (brandBox.Text.Length == 0 || medidaBox.Text.Length == 0 || categoryBox.Text.Length == 0))
            {
                validacion = true;
            }
            else if ((bool) brandCheck.IsChecked & (TipoCMB.SelectedIndex < 0 || brandBox.Text.Length == 0 || medidaBox.Text.Length == 0))
            {
                validacion = true;
            }
            else if ((bool) medidaCheck.IsChecked & (TipoCMB.SelectedIndex < 0 || MarcaCMB.SelectedIndex < 0 || medidaBox.Text.Length == 0))
            {
                validacion = true;
            } 
            else if (TipoCMB.SelectedIndex < 0 & MarcaCMB.SelectedIndex < 0 & MedidaCMB.SelectedIndex < 0)
            {
                validacion = true;
            }

            else
            {
                validacion = false;
            }
        }

    }
}
