using proyecto_Alejandro_Buitrago.Images;
using proyecto_Alejandro_Buitrago.ProductClass;
using proyecto_Alejandro_Buitrago.ProjectDB.MySQLData.projectdbDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace proyecto_Alejandro_Buitrago.ProjectDB.MySQLData
{
    public class projectDBHandler
    {

        private static publishabTableAdapter productAdapter = new publishabTableAdapter();
        private static projectdbDataSet dataSet = new projectdbDataSet();


       public static void removeDataFromDB(string referencia, string descripcion, string medida, float precio, DateTime fecha, int stock, string tipo, string madera, BitmapImage imagen)
        {
            productAdapter.Delete(referencia, descripcion, medida, precio, fecha, stock, tipo, madera, imagen);
            productAdapter.Update(dataSet);
        }

        //string p2, string p3, decimal p4, System.DateTime p5, int p6, string p7, string p8, byte[] p9, string p1

        public static void UpdateDB(string referencia, string descripcion, string medida, float precio, DateTime fecha, int stock, string tipo, string madera, BitmapImage imagen)
        {
            productAdapter.actualizarDB(descripcion, medida, Convert.ToDecimal(precio), fecha, stock, tipo, madera, ImageHandler.EncodeImage(imagen), referencia);
            productAdapter.Update(dataSet);
        }


        internal static void AddDataToDB(string referencia, string descripcion, string medida, float precio, DateTime fecha, int stock, string tipo, string madera, BitmapImage imagen)
        {
            productAdapter.Insert(referencia, descripcion, medida, precio, fecha, stock, tipo, madera, ImageHandler.EncodeImage(imagen));
            productAdapter.Update(dataSet);
        }


       /* internal static void PublishProductToDB(Product product)
        {
            productAdapter.Insert(product);
            productAdapter.Update(dataSet);
        } */



    }
}
