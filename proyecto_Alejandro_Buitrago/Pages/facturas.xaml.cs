using proyecto_Alejandro_Buitrago.ProductClass;
using proyecto_Alejandro_Buitrago.Reports;
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
                porFactura.Visibility = Visibility.Hidden;
                porFecha.Visibility = Visibility.Hidden;
                PorCif.Visibility = Visibility.Hidden;
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

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (nCif.Text != "")
            {
                String cif = nCif.Text;
                InformeCIF reportPreview = new InformeCIF();

                bool okConsulta = reportPreview.ObtenerPorCIF(cif);
                if (okConsulta)
                {
                    reportPreview.ObtenerPorCIF(cif);
                    reportPreview.Show();
                }
                else
                {
                    MessageBox.Show("No se ha encontrado registros para ese CIF");
                }
            }
            else
            {
                MessageBox.Show("Escribe un CIF válido");
                nCif.BorderBrush = new SolidColorBrush(Colors.Red);
            }


        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (nFactura.Text != "")
            {
                String factura = nFactura.Text;
                InformeCIF reportPreview = new InformeCIF();

                bool okConsulta = reportPreview.ObtenerPorFactura(factura);
                if (okConsulta)
                {
                    reportPreview.ObtenerPorFactura(factura);
                    reportPreview.Show();
                }
                else
                {
                    MessageBox.Show("No se ha encontrado registros para ese Nº de factura");
                }
            }
            else
            {
                MessageBox.Show("Escribe un nº de factura válido");
                nFactura.BorderBrush = new SolidColorBrush(Colors.Red);
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (fecha1.SelectedDate != null && fecha2.SelectedDate != null)
            {
                string fecha_1 = fecha1.SelectedDate.ToString();
                string fecha_2 = fecha2.SelectedDate.ToString();
                InformeCIF reportPreview = new InformeCIF();

                bool okConsulta = reportPreview.ObtenerPorFechas(DateTime.Parse(fecha_1), DateTime.Parse(fecha_2)); ;
                if (okConsulta)
                {
                    reportPreview.ObtenerPorFechas(DateTime.Parse(fecha_1), DateTime.Parse(fecha_2));
                    reportPreview.Show();
                }
                else
                {
                    MessageBox.Show("No se ha encontrado registros para ese rango de fechas");
                }
            }
            else
            {
                MessageBox.Show("Necesario seleccionar 2 fechas");
                fecha1.BorderBrush = new SolidColorBrush(Colors.Red);
                fecha2.BorderBrush = new SolidColorBrush(Colors.Red);
            }
        }
    }
}
