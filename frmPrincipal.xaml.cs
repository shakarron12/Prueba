using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ConvertImage;
using System.Runtime.InteropServices;

namespace abcCompleto
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        bool bExiste;
        clsAbc objControlador = new clsAbc();
        public MainWindow()
        {
            InitializeComponent();
            bExiste = false;
            txtNoEmpleado.Focus();
            cbRol.ItemsSource = objControlador.RetornarRoles();
            cbTipo.ItemsSource = objControlador.RetornarTipos();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (bExiste)
            {
                if (objControlador.EliminarEmpleado(Convert.ToInt32(txtNoEmpleado.Text)))
                    MessageBox.Show("Se elimino con exito!...");
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (objControlador.ValidarControlesVacios(GridControles2.Children))
            {

                BitmapImage image = imgUser.Source as BitmapImage;

                EmpleadoABC empleado = new EmpleadoABC()
                {
                    idNumEmpleado = Convert.ToInt32(txtNoEmpleado.Text),
                    nombre = txtNombre.Text,
                    primerap = txtPrimerAp.Text,
                    segundoap = txtSegundoAp.Text,
                    direccion = txtDireccion.Text,
                    curp = txtCurp.Text,
                    fechanac = Convert.ToDateTime(dtFecha.Text),
                    idrol = objControlador.RetornarIdRol(cbRol.SelectedValue.ToString()),
                    idtipo = objControlador.RetornarIdTipo(cbTipo.SelectedValue.ToString()),
                    img_usuario = image == null ? null : objControlador.BitMapImageToArray(image)
                };

                if (bExiste)
                {
                    if (objControlador.ActualizarEmpleado(empleado))
                    {
                        MessageBox.Show("Se guardo con exito!...");
                        Limpiar(true);
                    }
                }
                else
                {
                    if (objControlador.GuardarEmpleado(empleado))
                    {
                        MessageBox.Show("Se guardo con exito!...");
                        Limpiar(true);
                    }
                }
            }
            else
            {
                MessageBox.Show("No debe haber campos vacios.");
                txtNombre.Focus();
            }

        }

        private void txtNoEmpleado_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtNoEmpleado.Text != string.Empty)
            {
                txtNoEmpleado.IsEnabled = false;
                var empleado = objControlador.BuscarEmpleado(Convert.ToInt32(txtNoEmpleado.Text));

                if (empleado.Count > 0)
                {
                    foreach (var datos in empleado)
                    {
                        txtNombre.Text = datos.nombre;
                        txtPrimerAp.Text = datos.primerap;
                        txtSegundoAp.Text = datos.segundoap;
                        txtDireccion.Text = datos.direccion;
                        txtCurp.Text = datos.curp;
                        dtFecha.Text = datos.fechanac.ToString();
                        cbRol.SelectedIndex = datos.idrol - 1;
                        cbTipo.SelectedIndex = datos.idtipo - 1;
                        imgUser.Source = datos.img_usuario != null ? objControlador.ArrayToBitMapImage(datos.img_usuario) : objControlador.CargarImagenDefalut();
                        bExiste = true;
                    }
                    txtNombre.Focus();
                }
                else
                {
                    Limpiar(false);
                }
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar(true);
        }

        private void Limpiar(bool bLimpiarCompleto)
        {
            bExiste = false;
            txtNombre.Clear();
            txtPrimerAp.Clear();
            txtSegundoAp.Clear();
            txtDireccion.Clear();
            txtCurp.Clear();
            dtFecha.Text = string.Empty;
            cbRol.SelectedIndex = -1;
            cbTipo.SelectedIndex = -1;
            imgUser.Source = objControlador.CargarImagenDefalut();
            if (bLimpiarCompleto)
            {
                txtNoEmpleado.Clear();
                txtNoEmpleado.IsEnabled = true;
                txtNoEmpleado.Focus();

            }
        }

        private void frmMenu_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (txtNoEmpleado.Text == string.Empty)
                    this.Close();
                else
                    Limpiar(true);
            }
        }

        private void imgWebCam_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            frmCam objCam = new frmCam();
            objCam.ShowDialog();
            if (objCam.imgRetorna.Source != imgUser.Source)
                imgUser.Source = objCam.imgRetorna.Source;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            objControlador.ValidarControlesVacios(GridControles2.Children);
            //VentanaDePrueba x = new VentanaDePrueba();
            //x.ShowDialog();
        }

        private void txtNoEmpleado_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtNoEmpleado.Text == string.Empty)
            {
                if (e.Key == Key.Tab)
                {
                    e.Handled = true;
                }
                else if (e.Key == Key.F1) 
                {
                    frmBusqueda x = new frmBusqueda();
                    x.ShowDialog();
                    txtNoEmpleado.Text = x.sNumEmpleado;
                    txtNoEmpleado_LostFocus(sender, e);
                }
            }
        }

    }
}
