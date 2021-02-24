using proyecto_Alejandro_Buitrago.ProductClass;
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

namespace proyecto_Alejandro_Buitrago.Pages
{
    /// <summary>
    /// Lógica de interacción para facturas.xaml
    /// </summary>
    public partial class facturas : Page
    {

        public ProductHandler productHandler = new ProductHandler();

        public facturas()
        {
            InitializeComponent();
            Menu.Visibility = Visibility.Hidden;
            porFactura.Visibility = Visibility.Hidden;
            porFecha.Visibility = Visibility.Hidden;
            PorCif.Visibility = Visibility.Hidden;
        }

        private void Crear_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.myNavigationFrame.NavigationService.Navigate(new CrearFactura(productHandler));
        }

        private void Consulta_Click(object sender, RoutedEventArgs e)
        {
           if(Menu.Visibility == Visibility.Hidden)
            {
                Menu.Visibility = Visibility.Visible;
            }
            else
            {
                Menu.Visibility = Visibility.Hidden;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (PorCif.Visibility == Visibility.Hidden)
            {
                PorCif.Visibility = Visibility.Visible;
                porFecha.Visibility = Visibility.Hidden;
                porFactura.Visibility = Visibility.Hidden;
            }
            else
            {
                PorCif.Visibility = Visibility.Hidden;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (porFecha.Visibility == Visibility.Hidden)
            {
                porFecha.Visibility = Visibility.Visible;
                porFactura.Visibility = Visibility.Hidden;
                PorCif.Visibility = Visibility.Hidden;
            }
            else
            {
                porFecha.Visibility = Visibility.Hidden;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (porFactura.Visibility == Visibility.Hidden)
            {
                porFactura.Visibility = Visibility.Visible;
                porFecha.Visibility = Visibility.Hidden;
                PorCif.Visibility = Visibility.Hidden;
            }
            else
            {
                porFactura.Visibility = Visibility.Hidden;
            }
        }

        
    }
}
