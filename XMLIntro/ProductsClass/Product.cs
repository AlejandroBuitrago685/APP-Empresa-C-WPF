using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLIntro.ProductsClass
{
    public class Product : ICloneable
    {
        public String productRef { set; get; }
        public String category { set; get; }
        public String brand { set; get; }
        public String description { set; get; }
        public String bottleType { set; get; }

        public Product()
        {
        }

        public String GetAllValues()
        {
            return productRef + " " + brand + " " + description + " " + bottleType;
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
