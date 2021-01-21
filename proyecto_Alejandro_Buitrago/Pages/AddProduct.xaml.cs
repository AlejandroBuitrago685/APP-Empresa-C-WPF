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
        public AddProduct()
        {
            InitializeComponent();
            InitCategoriaCMB();
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

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (categoryBox.IsVisible)
            {
                categoryBox.Visibility = Visibility.Hidden;
                brandBox.Visibility = Visibility.Hidden;
                brandCheck.IsEnabled = true;
            }
            else
            {
                categoryBox.Visibility = Visibility.Visible;
                brandBox.Visibility = Visibility.Visible;
                brandCheck.IsEnabled = false;
            }
        }

        private void brandCheck_Checked(object sender, RoutedEventArgs e)
        {
            if (brandBox.IsVisible)
            {
                brandBox.Visibility = Visibility.Hidden;
            }
            else
            {
                brandBox.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //XMLHandler.AddXMLProduct(Product);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow.MyNavigationFrame.NavigationService.Navigate(new Inicio());
        }

    }
}
