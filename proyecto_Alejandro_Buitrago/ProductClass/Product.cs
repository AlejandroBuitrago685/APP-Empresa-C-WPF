using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


        public static ObservableCollection<Product> listaProductos = new ObservableCollection<Product>();
        public Product()
        {
            this.referencia = "";
            this.descripcion = "";
            this.medida = "";
            this.precio = 0;
            this.fecha = DateTime.Now;
            this.stock = 0;
            this.tipo = "";
            this.madera = "";
        }
        public Product(string referencia, string descripcion, string medida, float precio, DateTime fecha, int stock, string tipo, string madera)
        {
            this.referencia = referencia;
            this.descripcion = descripcion;
            this.medida = medida;
            this.precio = precio;
            this.fecha = fecha;
            this.stock = stock;
            this.tipo = tipo;
            this.madera = madera;
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
