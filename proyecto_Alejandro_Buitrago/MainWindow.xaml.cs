using proyecto_Alejandro_Buitrago.Pages;
using proyecto_Alejandro_Buitrago.ProductClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace proyecto_Alejandro_Buitrago
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ProductHandler productHandler = new ProductHandler();
        public Product product;
        public static Frame myNavigationFrame;

        public MainWindow()
        {
            InitializeComponent();
            myNavigationFrame = MyNavigationFrame;
            myNavigationFrame.NavigationService.Navigate(new Inicio());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyNavigationFrame.NavigationService.Navigate(new Inicio());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MyNavigationFrame.NavigationService.Navigate(new ProductsGrid(productHandler));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MyNavigationFrame.NavigationService.Navigate(new AddProduct());
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MyNavigationFrame.NavigationService.Navigate(new facturas());
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            MessageBoxResult resultado = MessageBox.Show("¿Está seguro de que quiere salir?",
                               "ATENCIÓN", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            switch (resultado)
            {
                case MessageBoxResult.Yes:
                    
                    break;

                case MessageBoxResult.No:
                    e.Cancel = true;
                    break;
            }
            
                // No cierro el form
               
            base.OnClosing(e);
        }
    }
}
