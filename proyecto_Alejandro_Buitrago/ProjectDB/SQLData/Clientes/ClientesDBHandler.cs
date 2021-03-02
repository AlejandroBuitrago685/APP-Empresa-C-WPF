using proyecto_Alejandro_Buitrago.ClientClass;
using proyecto_Alejandro_Buitrago.ProductClass;
using proyecto_Alejandro_Buitrago.ProjectDB.SQLData.Clientes.ClienteDBDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_Alejandro_Buitrago.ProjectDB.SQLData.Clientes
{
    class ClientesDBHandler
    {
        private static facturaTableAdapter facturaAdapter = new facturaTableAdapter();
        private static clienteTableAdapter clienteAdapter = new clienteTableAdapter();
        private static producto_facturaTableAdapter producto_facturaAdapter = new producto_facturaTableAdapter();

        public static void AddCliente(Client cliente)
        {
            clienteAdapter.Insert(cliente.cif, cliente.nombre, cliente.direccion);
        }

        public static void AddFactura(Client client, ObservableCollection<Product> listaproduct, string reffactura)
        {

            facturaAdapter.Insert(reffactura,client.cif, DateTime.Today.Date);
            foreach(Product p in listaproduct)
            {
                producto_facturaAdapter.Insert(p.referencia,reffactura, p.cantidad, p.precio, p.descripcion);
            }
           
        }
    }




}
