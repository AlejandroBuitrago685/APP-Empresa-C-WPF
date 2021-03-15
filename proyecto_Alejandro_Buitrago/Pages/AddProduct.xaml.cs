using proyecto_Alejandro_Buitrago.Images;
using proyecto_Alejandro_Buitrago.ProductClass;
using proyecto_Alejandro_Buitrago.ProjectDB.MySQLData;
using proyecto_Alejandro_Buitrago.ProjectDB.SQLData.LocalImage;
using proyecto_Alejandro_Buitrago.XML;
using System;
using System.Collections.Generic;
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
        public Product product;
        int posicion;
        private Regex valPrecio = new Regex(@"^\d*\.?\d*$");
        private Regex valStock = new Regex(@"^\d+$");
        private Regex Date = new Regex(@"^(([0-2]\d|[3][0-1])\/([0]\d|[1][0-2])\/[2][0]\d{2})$|^(([0-2]\d|[3][0-1])\/([0]\d|[1][0-2])\/[2][0]\d{2}\s([0-1]\d|[2][0-3])\:[0-5]\d\:[0-5]\d)$");
        public bool modificacion;
        public bool validacion = false;
        bool contiene = false;

        public AddProduct(String tituloPrincipal, ProductHandler productHandler, Product product, int pos)
        {

            InitializeComponent();
            titulo.Text = tituloPrincipal;
            this.productHandler1 = productHandler;
            this.product = product;
            productGrid.DataContext = product;
            this.posicion = pos;
            InitCategoriaCMB();
            modificacion = true;
            Ref.IsEnabled = false;
            Fecha.SelectedDate = product.fecha;
            myImage.Source = ImageHandler.LoadImage(product.referencia);

        }
        public AddProduct()
        {
            InitializeComponent();
            InitCategoriaCMB();
            modificacion = false;
            Fecha.SelectedDate = DateTime.Today;
            myImage.Source = ImageHandler.LoadDefaultImage();
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
                categoryCheck.IsEnabled = true;
            }
            else
            {
                brandBox.Visibility = Visibility.Visible;
                medidaBox.Visibility = Visibility.Visible;
                medidaCheck.IsEnabled = false;
                categoryCheck.IsEnabled = false;
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
                        XMLHandler.ModifyProduct(product);
                        //MainWindow.myNavigationFrame.NavigationService.Navigate(new Inicio());
                        ImageHandler.ModifyImage(product.referencia, (BitmapImage)myImage.Source);
                        MessageBox.Show("Producto modificado correctamente",
                                    "ATENCIÓN", MessageBoxButton.OK, MessageBoxImage.Information);

                    if (product.publish)
                    {
                        projectDBHandler.UpdateDB(product.referencia, product.descripcion, product.medida, product.precio, product.fecha, product.stock, product.tipo, product.madera, (BitmapImage)myImage.Source);
                    }

                    MainWindow.myNavigationFrame.NavigationService.Navigate(new ProductsGrid(productHandler1));

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

                    if(myImage.Source == null)
                    {
                        ImageHandler.LoadDefaultImage();
                    }

                    String descripcion = Descripcion.Text;
                    DateTime fecha = (DateTime)Fecha.SelectedDate;
                    int stock = int.Parse(Stock.Text);
                    float precio = float.Parse(Precio.Text.Replace(".", ","));
                    String referencia = Ref.Text;
                    Product product;

                    MessageBox.Show("Producto añadido correctamente",
                                "ATENCIÓN", MessageBoxButton.OK, MessageBoxImage.Information);

                    MessageBoxResult resultado = MessageBox.Show("¿Quiere añadir el producto a la base de datos?",
                                "ATENCIÓN", MessageBoxButton.YesNo, MessageBoxImage.Warning);


                    switch (resultado)
                    {
                        case MessageBoxResult.Yes:

                            projectDBHandler.AddDataToDB(referencia, descripcion, medida, precio, fecha, stock, tipo, madera, (BitmapImage)myImage.Source);
                            product = new Product(referencia, descripcion, medida, precio, fecha, stock, tipo, madera, true);
                            XMLHandler.AddXMLProduct(product);
                            ImageHandler.AddImage(product.referencia, (BitmapImage)myImage.Source);
                            MessageBox.Show("Añadido con éxito a la base de datos");
                            break;

                        case MessageBoxResult.No:
                            product = new Product(referencia, descripcion, medida, precio, fecha, stock, tipo, madera, false);
                            XMLHandler.AddXMLProduct(product);
                            ImageHandler.AddImage(product.referencia, (BitmapImage)myImage.Source);
                            break;
                    }

                    MainWindow.myNavigationFrame.NavigationService.Navigate(new Inicio());

                }

            }

        }

        public void Validation()
        {

            ObtenerReferencia();
            foreach (UIElement element in productGrid.Children)
                {
                    if (element is TextBox)
                    {
                        TextBox textBox = (TextBox)element;
                        if (textBox.Text.Equals(""))
                        {
                            textBox.BorderBrush = new SolidColorBrush(Colors.Red);
                            validacion = true;
                        }
                        else
                        {
                            textBox.BorderBrush = new SolidColorBrush(Colors.LightGray);
                        }
                    }
                    if (element is ComboBox)
                    {
                        ComboBox combo = (ComboBox)element;
                        if (combo.Text.Equals(""))
                        {
                            combo.BorderBrush = new SolidColorBrush(Colors.Red);
                            validacion = true;
                        }
                        else
                        {
                            combo.BorderBrush = new SolidColorBrush(Colors.LightGray);
                        }
                    }
                }

            if ((bool)categoryCheck.IsChecked & (brandBox.Text.Length == 0 || medidaBox.Text.Length == 0 || categoryBox.Text.Length == 0))
            {
                validacion = true;
            }
            else if ((bool)brandCheck.IsChecked & (TipoCMB.SelectedIndex < 0 || brandBox.Text.Length == 0 || medidaBox.Text.Length == 0))
            {
                validacion = true;
            }
            else if ((bool)medidaCheck.IsChecked & (TipoCMB.SelectedIndex < 0 || MarcaCMB.SelectedIndex < 0 || medidaBox.Text.Length == 0))
            {
                validacion = true;
            }

           else if (Precio.Text.Contains(","))
            {
                validacion = true;
                Warning.Text = "Formato inválido en el precio, introduzca: (X.X)";
                Precio.BorderBrush = new SolidColorBrush(Colors.Red);
            }

            else if (!valPrecio.IsMatch(Precio.Text))
            {
                validacion = true;
                Warning.Text = "Formato inválido en el precio, introduzca valores numéricos";
                Precio.BorderBrush = new SolidColorBrush(Colors.Red);
            }

            else if (!valStock.IsMatch(Stock.Text))
            {
                validacion = true;
                Warning.Text = "Formato inválido en el stock, introduzca valores enteros";
                Stock.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else if (contiene && !modificacion) {
                validacion = true;
                Warning.Text = "Referencia ya existente";
                Ref.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else if (Fecha.Text == "" || !Date.IsMatch(Fecha.Text))
            {
                Warning.Text = "Fecha o formato no válido";
                Fecha.BorderBrush = new SolidColorBrush(Colors.Red);
                
            }

            else
            {
                validacion = false;
            }
        }

        public bool ObtenerReferencia()
        {
            
            var listaCategorias = xml.Root.Elements("Tipo").Elements("Madera").Elements("Producto").Attributes("ProductRef");
            foreach (XAttribute a in listaCategorias)
            {
                if(a.Value == Ref.Text)
                {
                    contiene = true;
                    break;
                }
            }
            return contiene;
        }



        private void añadirImagen_Click(object sender, RoutedEventArgs e)
        {

            BitmapImage bitmapImage = ImageHandler.GetBitmapFromFile();
            if (bitmapImage != null)
            {
                myImage.Source = bitmapImage;
            }

        }

        private void borrarImagen_Click(object sender, RoutedEventArgs e)
        {
            if (myImage.Source != null)
            {
                MessageBoxResult resultado = MessageBox.Show("¿Seguro que quiere borrar la imagen del producto?",
                               "ATENCIÓN", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                switch (resultado)
                {
                    case MessageBoxResult.Yes:

                        LocalImageDBHandler.removeDataFromDB(product.referencia);
                        myImage.Source = ImageHandler.LoadDefaultImage();
                        break;

                }
            }
        }
    }
}
