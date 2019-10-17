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
using System.Net;

namespace abcCompleto
{
    public struct Conexion
    {
        public static string sIp = string.Empty;
        public static string sUsuario = string.Empty;
        public static string sDB = string.Empty;
        public static string sContraseña = string.Empty;
    }
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        bool bExiste;
        clsPrincipal objControlador;
        clsCalculoMovimiento objControladorMovimientos;
        public MainWindow()
        {
            
            InitializeComponent();
            if (CargarArchivoConfig())
            {
                objControlador = new clsPrincipal();
                if (objControlador.VerificarConexion())
                {
                    objControladorMovimientos = new clsCalculoMovimiento();
                    bExiste = false;
                    txtNoEmpleado.Focus();
                    cbRol.ItemsSource = objControlador.RetornarRoles();
                    if (cbRol.Items.Count > 0)
                    {
                        cbTipo.ItemsSource = objControlador.RetornarTipos();
                    }
                }
                else 
                {
                    MessageBox.Show("No hay una conexion establecida, verificar archivo de configuracion.");
                    this.Close();
                }
            }
            else 
            {
                this.Close();
            }
           
        }

        internal bool CargarArchivoConfig()
        {
            bool bRegresa = false;
            try
            {
                using (StreamReader reader = new StreamReader(@"abcDat.txt"))
                {
                    string[] line = reader.ReadToEnd().Split('\n');

                    if ((Conexion.sIp = ValidaIP(line[0].Trim()) == true ? line[0].Trim() : string.Empty) != string.Empty)
                    {
                        Conexion.sDB = line[1].Trim();
                        Conexion.sUsuario = line[2].Trim();
                        Conexion.sContraseña = line[3].Trim();
                        bRegresa = true;
                    }
                    else 
                    {
                        throw new Exception("La direccion ip no es valida.");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return bRegresa;
        }

        private static bool ValidaIP(string sIP)
        {
            try
            { IPAddress ip = IPAddress.Parse(sIP); }
            catch
            { return false; }

            return true;
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (bExiste)
            {
                if (MessageBox.Show("¿Seguro que quiere eliminar el Empleado?", "Eliminar Empleado", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (objControlador.EliminarEmpleado(Convert.ToInt32(txtNoEmpleado.Text)))
                        if (objControlador.EliminarSalario(Convert.ToInt32(txtNoEmpleado.Text)))
                            MessageBox.Show("Se elimino con exito!...");
                }
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
               
                SalarioABC salario = new SalarioABC()
                {
                    idNumEmpleado = Convert.ToInt32(txtNoEmpleado.Text),
                    salario_mensual = chkSalario.IsChecked == true ? Convert.ToDouble(txtSalario.Text) : 7200.00
                };

                if (bExiste)
                {
                    if (objControlador.ActualizarEmpleado(empleado))
                    {
                        if (objControlador.ActualizarSalario(salario)) 
                        {
                            MessageBox.Show("Se guardo con exito!...");
                            Limpiar(true);
                        }
                    }
                }
                else
                {
                    if (objControlador.GuardarEmpleado(empleado))
                    {
                        if (objControlador.GuardarSalario(salario))
                        {
                            MessageBox.Show("Se guardo con exito!...");
                            Limpiar(true);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No debe haber campos vacios.");
                txtNombre.Focus();
            }

        }
        EmpleadoABC empleadoCargado;
        private void txtNoEmpleado_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtNoEmpleado.Text != string.Empty)
            {
                var empleado = objControlador.BuscarEmpleado(Convert.ToInt32(txtNoEmpleado.Text));
                txtNoEmpleado.IsEnabled = false;

                if (empleado != null)
                {
                    empleadoCargado = empleado;

                    txtNombre.Text = empleado.nombre;
                    txtPrimerAp.Text = empleado.primerap;
                    txtSegundoAp.Text = empleado.segundoap;
                    txtDireccion.Text = empleado.direccion;
                    txtCurp.Text = empleado.curp;
                    dtFecha.Text = empleado.fechanac.ToString();
                    cbRol.SelectedIndex = empleado.idrol - 1;
                    cbTipo.SelectedIndex = empleado.idtipo - 1;
                    imgUser.Source = empleado.img_usuario != null ? objControlador.ArrayToBitMapImage(empleado.img_usuario) : objControlador.CargarImagenDefalut();
                    bExiste = true;
                    btnAgregarMov.IsEnabled = true;
                    /*foreach (var datos in empleado)
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
                        btnAgregarMov.IsEnabled = true;
                    }*/
                    
                    var salario = objControlador.BuscarSalario(Convert.ToInt32(txtNoEmpleado.Text));

                    if (salario.Count > 0)
                    {
                        chkSalario.IsChecked = true;
                        foreach (var datos in salario)
                        {
                            txtSalario.Text = datos.salario_mensual.ToString();
                        }
                        txtNombre.Focus();
                        tiHorarios.IsEnabled = true;
                    }
                }
                else
                {
                    txtNombre.Focus();
                    tiHorarios.IsEnabled = false;
                }
            }
            else
            {
                txtNoEmpleado.Focus();
                tiHorarios.IsEnabled = false;
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
            btnAgregarMov.IsEnabled = false;
            imgUser.Source = objControlador.CargarImagenDefalut();
            txtSalario.Text = "default 7200.00";
            chkSalario.IsChecked = false;
            tiHorarios.IsEnabled = false;
            empleadoCargado = null;
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

        private void txtNoEmpleado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                frmBusqueda x = new frmBusqueda();
                x.ShowDialog();
                txtNoEmpleado.Text = x.sNumEmpleado;
                txtNoEmpleado_LostFocus(sender, e);
            }
            else if (e.Key == Key.Enter) 
            {
                txtNoEmpleado_LostFocus(sender, e);
            }
        }

        private void btnAgregarMov_Click(object sender, RoutedEventArgs e)
        {
            int idrol = objControlador.RetornarIdRol(cbRol.SelectedValue.ToString());
            int idtipo = objControlador.RetornarIdTipo(cbTipo.SelectedValue.ToString());
            frmMovimientos objMovimientos = new frmMovimientos(txtNoEmpleado.Text, txtNombre.Text, Convert.ToInt32(txtNoEmpleado.Text), idrol, idtipo);
            objMovimientos.ShowDialog();
        }

        private void chkSalario_Unchecked(object sender, RoutedEventArgs e)
        {
            txtSalario.IsEnabled = false;
            txtSalario.Text = "default 7200.00";
        }

        private void chkSalario_Checked(object sender, RoutedEventArgs e)
        {
            txtSalario.IsEnabled = true;
            txtSalario.Text = "7200.00";
            var salario = objControlador.BuscarSalario(Convert.ToInt32(txtNoEmpleado.Text));

            if (salario.Count > 0)
            {
                foreach (var datos in salario)
                {
                    txtSalario.Text = datos.salario_mensual.ToString();
                }
            }
        }

        private void txtNoEmpleado_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (txtNoEmpleado.Text == string.Empty && e.NewFocus != tiMovimientos && e.NewFocus != txtBusquedaEmp)
            {
                MessageBox.Show("Ingrese un numero de empleado", "Error", MessageBoxButton.OK);
                e.Handled = true;
            }
            else if (e.NewFocus == txtBusquedaEmp) 
            {
                e.Handled = true;
            }
        }

        //MOVIMIENTOS

        private void tiMovimientos_GotFocus(object sender, RoutedEventArgs e)
        {
            //dtgCalcMovimientos.ItemsSource = null;
            //txtBusquedaEmp.Text = txtNoEmpleado.Text;
            lblNombreCompleto.Content = txtNombre.Text + " " + txtPrimerAp.Text + " " + txtSegundoAp.Text;
            dtgCalcMovimientos.ItemsSource = objControladorMovimientos.traerDatos();
        }

        private void txtBusquedaEmp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) 
            {
                if (txtBusquedaEmp.Text != string.Empty)
                {
                    var empleado = objControlador.BuscarEmpleado(Convert.ToInt32(txtBusquedaEmp.Text));
                    if (empleado.idNumEmpleado != 0)
                    {
                        lblNombreCompleto.Content = empleado.nombre + " " + empleado.primerap + " " + empleado.segundoap;
                        dtgCalcMovimientos.ItemsSource = objControladorMovimientos.traerDatos(Convert.ToInt32(txtBusquedaEmp.Text));
                    }
                    else 
                    {
                        MessageBox.Show("No se encontro empleado.");
                        txtBusquedaEmp.Clear();
                        lblNombreCompleto.Content = "";
                        dtgCalcMovimientos.ItemsSource = objControladorMovimientos.traerDatos();
                    }
                }
                else 
                {
                    lblNombreCompleto.Content = "";
                    dtgCalcMovimientos.ItemsSource = objControladorMovimientos.traerDatos();
                }
                
            }
            else if (e.Key == Key.F1)
            {
                frmBusqueda x = new frmBusqueda();
                x.ShowDialog();
                txtBusquedaEmp.Text = x.sNumEmpleado;
                if (txtBusquedaEmp.Text != string.Empty)
                {
                    var empleado = objControlador.BuscarEmpleado(Convert.ToInt32(txtBusquedaEmp.Text));
                    lblNombreCompleto.Content = empleado.nombre + " " + empleado.primerap + " " + empleado.segundoap;
                    txtBusquedaEmp_KeyDown(sender, new KeyEventArgs(Keyboard.PrimaryDevice,
                                                       Keyboard.PrimaryDevice.ActiveSource,
                                                       0, Key.Enter));
                }
            }
        }

        //HORARIOS
        bool bHorario = false;
        private void tiHorarios_GotFocus(object sender, RoutedEventArgs e)
        {
            //txtBusquedaEmp_Horarios.Text = txtNoEmpleado.Text;
            lblNombreCompleto_Horario.Content = txtNombre.Text + " " + txtPrimerAp.Text + " " + txtSegundoAp.Text;
            if (!bHorario)
            {
                cbRol_Horario.SelectedIndex = empleadoCargado.idrol - 1;
                cbTipo_Horario.SelectedIndex = empleadoCargado.idtipo - 1;
                //cbRol_Horario.SelectedIndex = cbRol.SelectedIndex;
                //cbTipo_Horario.SelectedIndex = cbTipo.SelectedIndex;
                bHorario = true;
            }
            //cbRol_Horario.SelectedIndex = cbTipo.SelectedIndex;
            //cbTipo_Horario.SelectedIndex = cbTipo.SelectedIndex;
            chkCubrioTurno.IsEnabled = cbRol.SelectedItem.ToString() == "Auxiliar" ? true : false; 

        }

        private void btnChecar_Click(object sender, RoutedEventArgs e)
        {
            //inserta horario para calcular salario
        }

        private void chkCubrioTurno_Checked(object sender, RoutedEventArgs e)
        {
            cbRol_Horario.IsEnabled = true;
            //cbRol_Horario.ItemsSource = cbRol.Items;
        }

        private void chkCubrioTurno_Unchecked(object sender, RoutedEventArgs e)
        {
            cbRol_Horario.IsEnabled = false;
        }

        private void TabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            bHorario = false;
        }
    
    }
}
