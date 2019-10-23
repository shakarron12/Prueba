using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace abcCompleto
{
    /// <summary>
    /// Lógica de interacción para frmCam.xaml
    /// </summary>
    public partial class frmCam : Window
    {
        internal Image imgRetorna;
        public frmCam()
        {
            InitializeComponent();
        }

        private void frmCamara_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {


            OpenFileDialog openFile = new OpenFileDialog();

            BitmapImage b = new BitmapImage();

            openFile.Title = "Seleccione la Imagen a Mostrar";

            openFile.Filter = "Imagenes|*.jpg;*.gif;*.png;*.bmp";

            if (openFile.ShowDialog() == true)
            {

                b.BeginInit();

                b.UriSource = new Uri(openFile.FileName);

                b.EndInit();

                imgFoto.Stretch = Stretch.Fill;

                imgFoto.Source = b;

            }
        }

        private void btnCapturar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void frmCamara_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            imgRetorna = imgFoto;
        }
    }
}
