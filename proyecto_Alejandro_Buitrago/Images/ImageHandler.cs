using Microsoft.Win32;
using proyecto_Alejandro_Buitrago.ProjectDB.SQLData.LocalImage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace proyecto_Alejandro_Buitrago.Images
{
    public class ImageHandler
    {
        public static BitmapImage GetBitmapFromFile()
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Elige una imagen |*.jpg; *.png";
            BitmapImage bitmapImage = new BitmapImage();
            bool? dialogoOK = opf.ShowDialog();

            if (dialogoOK == true)
            {
                String imageName = opf.FileName;
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(imageName, UriKind.Absolute);
                bitmapImage.EndInit();
                return bitmapImage;
            }

            return null;
        }
        public static void AddImage(string productRef, BitmapImage bitmapImage)
        {
            LocalImageDBHandler.AddDataToDB(productRef, EncodeImage(bitmapImage));
        }

        public static byte[] EncodeImage(BitmapImage bitmapImage)
        {

            byte[] imageByte;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));

            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                imageByte = ms.ToArray();
            }
            return imageByte;
        }
    }

    
}
