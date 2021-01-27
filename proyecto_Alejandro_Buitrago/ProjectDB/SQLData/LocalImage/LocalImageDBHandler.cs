using proyecto_Alejandro_Buitrago.ProjectDB.SQLData.LocalImage.LocalImageDataSet;
using proyecto_Alejandro_Buitrago.ProjectDB.SQLData.LocalImage.LocalImageDataSet.DataSet_Local_ImageTableAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_Alejandro_Buitrago.ProjectDB.SQLData.LocalImage
{
    public class LocalImageDBHandler
    {
        private static ImagesTableAdapter imagesAdapter = new ImagesTableAdapter();
        private static DataSet_Local_Image dataSet = new DataSet_Local_Image();

        public static void AddDataToDB(String idImage, byte[] produtImage)
        {
            imagesAdapter.Insert(idImage, produtImage);
            imagesAdapter.Update(dataSet);
        }
    }
}
