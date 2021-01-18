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
using XMLIntro.XML;

namespace XMLIntro
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private XDocument xml = XDocument.Load("../../XML/XMLFile1.xml");
        public MainWindow()
        {
            InitializeComponent();
            InitCategoriaCMB();
        }

        private void InitCategoriaCMB()
        {
            var listaCategorias = xml.Root.Elements("Categoria").Attributes("IdCategoria");
            for(int i=0; i < listaCategorias.Count(); i++)
            {
                TipoCMB.Items.Add(listaCategorias.ElementAt(i).Value);
            }      
        }

        private void TipoCMB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MarcaCMB.Items.Clear();
            var listaMarcas = xml.Root.Elements("Categoria").ElementAt(TipoCMB.SelectedIndex).Elements().Attributes("Nombre");
            for (int i = 0; i < listaMarcas.Count(); i++)
            {
                MarcaCMB.Items.Add(listaMarcas.ElementAt(i).Value);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            XElement nuevaCat = new XElement("Categoria", new XAttribute("idCategoria","Familiar"));
            XElement nuevaMarca = new XElement("Marca", new XAttribute("Nombre", "Peugueot"));
            nuevaCat.Add(nuevaMarca);
            xml.Root.Add(nuevaCat);
            xml.Save("../../XML/XMLProducto.xml");
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (categoryBox.IsVisible)
            {
                categoryBox.Visibility = Visibility.Hidden;
                brandBox.Visibility = Visibility.Hidden;
                brandCheck.IsEnabled = true;
            } else
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            XMLHandler.AddXMLProduct(product);
        }
    }
}
