using proyecto_Alejandro_Buitrago.ProjectDB.SQLData.LocalImage.LocalImageDataSet;
using proyecto_Alejandro_Buitrago.ProjectDB.SQLData.LocalImage.LocalImageDataSet.DataSet_Local_ImageTableAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace proyecto_Alejandro_Buitrago.ProjectDB.SQLData.LocalImage
{
    public class LocalImageDBHandler
    {
        private static ImagesTableAdapter imagesAdapter = new ImagesTableAdapter();
        private static DataSet_Local_Image dataSet = new DataSet_Local_Image();


        public static void removeDataFromDB(String idImage)
        {
            imagesAdapter.Delete(idImage);
            imagesAdapter.Update(dataSet);
        }
        
        
        public static void ModifyDataFromDB(string idImage, byte[] productImage)
        {
            imagesAdapter.UpdateData(productImage, idImage);
            imagesAdapter.Update(dataSet);
        }

        public static void AddDataToDB(String idImage, byte[] produtImage)
        {
            imagesAdapter.Insert(idImage, produtImage);
            imagesAdapter.Update(dataSet);
        }

        public static byte [] GetDataFromDB(String idImage)
        {
            byte[] imageData = null;
            try
            {
                imageData = imagesAdapter.GetImage(idImage).ElementAt(0).PorductImage;
            }

            catch(Exception ex)
            {

            }

            return imageData;
        }

        internal static void UpdateDataFromDB(string IdImage, byte[] productImage)
        {
            byte[] dataImage = GetDataFromDB(IdImage);

            if(dataImage == null)
            {
                AddDataToDB(IdImage, productImage);
            }else
            {
                imagesAdapter.UpdateData(productImage, IdImage);
                imagesAdapter.Update(dataSet);
            }


            
        }
    }
}
