using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using XMLIntro.ProductsClass;

namespace XMLIntro.XML
{
    class XMLHandler
    {
        private static XDocument xml;
        private static Product product;
        private static XElement xmlCategory;
        private static void LoadXML()
        {
            xml = XDocument.Load("../../XML/XMLFile1.xml");
        }

        private static void SaveXML()
        {
            xml.Save("../../XML/XMLFile1.xml");
        }
        public static void AddXMLProduct(Product p)
        {
            product = p;
            LoadXML();
            AddCategory();
            SaveXML();
        }

        private static void AddCategory() //Comprobar si la categoria ya existe
        {
            var listaCategorias = xml.Root.Elements("Categoria").Attributes("IdCategoria");
            bool isNewCategory = true;
            foreach(XAttribute categoria in listaCategorias)
            {
                if (categoria.Value.Equals(product.category))
                {
                    xmlCategory = categoria.Parent;
                    isNewCategory = false;
                    break;
                } else
                {
                    xmlCategory = new XElement("Categoria", new XAttribute("IdCategoria", product.category));
                    isNewCategory = true;
                }
            }
            if (isNewCategory)
            {
                xml.Root.Add(xmlCategory);
            }
        
        }
        public static ObservableCollection<Product> LoadProducts()
        {
            ObservableCollection<Product> productList = new ObservableCollection<Product>();
            LoadXML();
            var listaProductosXML = xml.Root.Elements("Categoria").Elements("Marca").Elements("Producto");
            foreach (XElement productXML in listaProductosXML)
            {
                product = new Product();
                product.productRef = productXML.Attribute("ProductRef").Value;
                product.bottleType = productXML.Attribute("BottleType").Value;
                product.description = productXML.Attribute("Description").Value;
                product.brand = productXML.Parent.Attribute("Nombre").Value;
                product.category = productXML.Parent.Parent.Attribute("IdCategoria").Value;
                productList.Add(product);
            }
            return productList;
        }
    }
}
