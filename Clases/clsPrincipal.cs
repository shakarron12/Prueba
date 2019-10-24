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

        /// <summary>
        /// Verifica que la conexion siga en pie.
        /// </summary>
        /// <returns>Bool</returns>
        internal bool VerificarConexion() 
        {
            return VerificarConexionPrin();
        }

        /// <summary>
        /// Convierte de bitmapimage a arreglo de bytes.
        /// </summary>
        /// <param name="image">Imagen que desea convertir.</param>
        /// <returns>byte[]</returns>
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

        /// <summary>
        /// Convierte de arreglo de bytes a bitmapimage.
        /// </summary>
        /// <param name="array">Arreglo de bytes que desea convertir.</param>
        /// <returns>BitmapImage</returns>
        internal BitmapImage ArrayToBitMapImage(byte[] array)
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = new System.IO.MemoryStream(array);
            image.EndInit();
            return image;
        }

        /// <summary>
        /// Busca el icono de la imagen.
        /// </summary>
        /// <param name="array">Arreglo de bytes que desea convertir.</param>
        /// <returns>BitmapImage</returns>
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
