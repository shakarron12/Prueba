using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace abcCompleto
{
    class clsPrincipal : clsEmpleado
    {
        public clsPrincipal() 
        {
        
        }

        internal bool VerificarConexion() 
        {
            return VerificarConexionPrin();
        }

        internal byte[] BitMapImageToArray(BitmapImage image)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }

            return data;
        }

        internal BitmapImage ArrayToBitMapImage(byte[] array)
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = new System.IO.MemoryStream(array);
            image.EndInit();
            return image;
        }

        internal BitmapImage CargarImagenDefalut()
        {
            string fullimagepath = Directory.GetCurrentDirectory() + "\\iconos\\user.ico";
            fullimagepath = fullimagepath.Replace("\\", "/");
            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri(fullimagepath);
            logo.EndInit();

            return logo;
        }

    }
}
