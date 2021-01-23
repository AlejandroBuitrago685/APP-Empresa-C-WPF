using proyecto_Alejandro_Buitrago.ProductClass;
using proyecto_Alejandro_Buitrago.XML;
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
    /// Lógica de interacción para ModydDelete.xaml
    /// </summary>
    /// 
   
    public partial class ModydDelete : Page
    {
        public ObservableCollection<Product> listaProductos { get; set;}
        private static Product producto;
        public ModydDelete(Product product)
        {
            InitializeComponent();
            //this.listaProductos = ProductHandler.productList;
            this.DataContext = this;
        }



        public void deleteProduct (int pos)
        {
            //productList.RemoveAt(pos);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            XMLHandler.DeleteProduct(producto.referencia);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int pos = comboProduct.SelectedIndex;
            
        }
    }
}
