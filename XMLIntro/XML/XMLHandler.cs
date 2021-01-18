using System;
using System.Collections.Generic;
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
    }
}
