using proyecto_Alejandro_Buitrago.XML;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_Alejandro_Buitrago.ProductClass
{
    
    public class ProductHandler
    {
        public  ObservableCollection<Product> productList { get; set; }

        public ProductHandler()
        {
            this.productList = new ObservableCollection<Product>();
            UpdateProductList();
        }

        public void UpdateProductList() { this.productList = XMLHandler.LoadProducts(); }

        public void AddProduct(Product product) { productList.Add(product); }

    }
}

