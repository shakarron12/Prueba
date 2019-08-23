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
    class clsAbc
    {
        clsRN objRN;
        
        public clsAbc() 
        {
            objRN = new clsRN();
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

        internal List<EmpleadoABC> BuscarEmpleado(int iNumEmpleado) 
        {
            return objRN.BuscarEmpleado(iNumEmpleado);
        }

        internal bool ValidarControlesVacios(UIElementCollection uiControles)
        {
            bool bRegresa = true;
            
            foreach (UIElement Control in uiControles)
            {
                if (Control is TextBox)
                {
                    var textbox = Control as TextBox;
                    if (textbox != null)
                    {
                        if (textbox.Text == "")
                        {
                            bRegresa = false;
                        }
                    }
                }
                else if (Control is DatePicker)
                {
                    var textbox = Control as DatePicker;
                    if (textbox != null)
                    {
                        if (textbox.Text == "")
                        {
                            bRegresa = false;
                        }
                    }
                }
                else if (Control is ComboBox)
                {
                    var textbox = Control as ComboBox;
                    if (textbox != null)
                    {
                        if (textbox.Text == "")
                        {
                            bRegresa = false;
                        }
                    }
                }
                else if (Control is Border)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(Control, 0);
                    var Grid = child as Grid;
                    bRegresa = ValidarControlesVacios(Grid.Children);
                }

            }
            return bRegresa;
        }

        internal bool EliminarEmpleado(int iNumEmpleado) 
        {
            return objRN.EliminarEmpleado(iNumEmpleado);
        }

        internal bool GuardarEmpleado(EmpleadoABC empleado) 
        {
           return objRN.GuardarEmpleado(empleado);
        }

        internal bool ActualizarEmpleado(EmpleadoABC empleado)
        {
            return objRN.ActualizarEmpleado(empleado);
        }

        internal List<string> RetornarRoles()
        {
            return objRN.llenarComboRol();
        }

        internal List<string> RetornarTipos()
        {
            return objRN.llenarComboTipo();
        }

        internal int RetornarIdRol(string sNombreRol) 
        {
            return objRN.retornaridRol(sNombreRol);
        }

        internal int RetornarIdTipo(string sNombreTipo)
        {
            return objRN.retornaridTipo(sNombreTipo);
        }
    }
}
