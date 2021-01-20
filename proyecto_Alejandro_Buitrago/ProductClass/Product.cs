using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_Alejandro_Buitrago.ProductClass
{
    public class Product : ICloneable
    {
        public String referencia { set; get; }
        public String descripcion { set; get; }
        public String medida { set; get; }
        public float precio { set; get; }
        public DateTime fecha { set; get; }
        public int stock { set; get; }
        public String tipo { set; get; }
        public String madera { set; get; }

        public Product()
        {
        }

        public String GetAllValues()
        {
            return referencia + " " + tipo + " " + descripcion + " " + madera + " " + medida + " " + fecha + " " + precio + " " + stock;
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
