
using proyecto_Alejandro_Buitrago.ProductClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace proyecto_Alejandro_Buitrago.XML
{
    class XMLHandler
    {
        private static XDocument xml;
        private static Product product;
        private static XElement xmlCategory;
        private static XElement xmlMadera;

        private static void LoadXML()
        {
            xml = XDocument.Load("../../XML/xml.xml");
        }

        private static void SaveXML()
        {
            xml.Save("../../XML/xml.xml");
        }
        public static void AddXMLProduct(Product p)
        {

            product = p;
            LoadXML();
            AddTipo();
            AddMadera();
            CrearProducto();
            SaveXML();
            MessageBoxResult resultado = MessageBox.Show("Se ha agregado un nuevo producto: \n" + "Referencia: " + p.referencia + "\n" + "Tipo de madera: " + p.madera + "\n" + "Precio: " + p.precio + "\n" + "Tipo: " + p.tipo + "\n" + "Medida: " + p.medida + "\n" + "Stock: " + p.stock, "Registro de nuevo producto", MessageBoxButton.OK, MessageBoxImage.Information);


        }

        private static void CrearProducto()
        {
            XElement xmlProduct = new XElement("Producto", new XAttribute("ProductRef", product.referencia), new XAttribute("Medida", product.medida),
                 new XAttribute("Descripcion", product.descripcion), new XAttribute("Precio", product.precio.ToString().Replace('.',',')),
                 new XAttribute("Fecha", product.fecha.ToString("dd/MM/yy")), new XAttribute("Stock", product.stock), new XAttribute("Publish", product.publish));
            xmlMadera.Add(xmlProduct);
        }

        public static ObservableCollection<Product> LoadProducts()
        {
            ObservableCollection<Product> productList = new ObservableCollection<Product>();
            LoadXML();
            var listaProductosXML = xml.Root.Elements("Tipo").Elements("Madera").Elements("Producto");
            foreach (XElement productXML in listaProductosXML)
            {
                product = new Product();
                product.referencia = productXML.Attribute("ProductRef").Value;
                product.descripcion = productXML.Attribute("Descripcion").Value;
                product.stock = int.Parse(productXML.Attribute("Stock").Value);
                product.precio = float.Parse(productXML.Attribute("Precio").Value.Replace('.' , ','));
                product.medida = productXML.Attribute("Medida").Value;
                product.tipo = productXML.Parent.Parent.Attribute("IdTipo").Value;
                product.madera = productXML.Parent.Attribute("IdNombre").Value;
                product.fecha = DateTime.Parse(productXML.Attribute("Fecha").Value);
                product.publish = bool.Parse(productXML.Attribute("Publish").Value);
                productList.Add(product);
            }
            return productList;
        }

        private static void AddTipo() //Comprobar si la categoria ya existe
        {
            var listaCategorias = xml.Root.Elements("Tipo").Attributes("IdTipo");
            bool isNewCategory = true;
            foreach (XAttribute categoria in listaCategorias)
            {
                if (categoria.Value.Equals(product.tipo))
                {
                    xmlCategory = categoria.Parent;
                    isNewCategory = false;
                    break;
                }
                else
                {
                    xmlCategory = new XElement("Tipo", new XAttribute("IdTipo", product.tipo));
                    xmlMadera = new XElement("Madera", new XAttribute("IdNombre", product.madera));
                    isNewCategory = true;
                }
            }
            if (isNewCategory)
            {
                xmlCategory.Add(xmlMadera);
                xml.Root.Add(xmlCategory);
            }

        }

        private static void AddMadera()
        {
            bool isNewMadera = true;
            foreach (XAttribute madera in xmlCategory.Elements().Attributes("IdNombre"))
            {
                if (madera.Value.Equals(product.madera))
                {
                    xmlMadera = madera.Parent;
                    isNewMadera = false;
                    break;
                }
                else
                {
                    xmlMadera = new XElement("Madera", new XAttribute("IdNombre", product.madera));
                    isNewMadera = true;
                }
            }
            if (isNewMadera)
            {
                
                AddTipoMadera();
            }
        }

        private static void AddTipoMadera()
        {
            var listaCategorias = xml.Root.Elements().Attributes("IdNombre");
            bool isNewCategory = true;
            foreach (XAttribute categoria in listaCategorias)
            {
                if (categoria.Value.Equals(product.madera))
                {
                    xmlCategory = categoria.Parent;
                    isNewCategory = false;
                    break;
                }
                else
                {
                    xmlMadera = new XElement("Madera", new XAttribute("IdNombre", product.madera));
                    isNewCategory = true;
                }
            }
            if (isNewCategory)
            {
                xmlCategory.Add(xmlMadera);
            }

        }

        public static void DeleteProduct(String ReferenciaProducto)
        {
            LoadXML();
            var listaRefXML = xml.Root.Elements("Tipo").Elements("Madera").Elements("Producto").Attributes("ProductRef");
            foreach(XAttribute referencia in listaRefXML)
            {
                if(ReferenciaProducto == referencia.Value)
                {
                    referencia.Parent.Remove();
                    break;
                }
            }
            SaveXML();
        }

        public static void DeleteType(String NombreTipo)
        {
            LoadXML();
            var listaTipoXML = xml.Root.Elements("Tipo").Attributes("IdTipo");
            foreach (XAttribute tipo in listaTipoXML)
            {
                if (NombreTipo == tipo.Value)
                {
                    tipo.Parent.Remove();
                    break;
                }
            }
            SaveXML();
        }
        public static void ModifyProduct(Product p)
        {
            LoadXML();
            var listaRefXML = xml.Root.Elements("Tipo").Elements("Madera").Elements("Producto").Attributes("ProductRef");
            foreach (XAttribute referencia in listaRefXML)
            {
                if (p.referencia == referencia.Value)
                {
                    referencia.Parent.Remove();
                    break;
                }
            }
            SaveXML();
            AddXMLProduct(p);
        }
    }
   
}
