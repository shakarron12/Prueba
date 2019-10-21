using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
    /// Lógica de interacción para frmModificarMovimientos.xaml
    /// </summary>
    public partial class frmModificarMovimientos : Window
    {
        clsMovimiento objControlador = new clsMovimiento();
        private int iNumEmpleado;
        private bool bEsc;
        private bool bExiste;

        public int _INumEmpleado
        {
            get { return iNumEmpleado; }
            set { iNumEmpleado = value; }
        }

        public frmModificarMovimientos(int iNumEmpleado)
        {
            InitializeComponent();
            _INumEmpleado = iNumEmpleado;
            bExiste = true;
            cbRol.ItemsSource = objControlador.RetornarRoles();
            cbTipo.ItemsSource = objControlador.RetornarTipos();
            txtMovimiento.Focus();
        }

        private void txtMovimiento_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (txtMovimiento.Text == string.Empty)
            {
                if (e.Key == Key.Tab)
                {
                    e.Handled = true;
                }
            }
            
            if (e.Key == Key.F1)
            {
                frmBusquedaMovimientos objBusquedaMov = new frmBusquedaMovimientos(_INumEmpleado);
                objBusquedaMov.ShowDialog();
                txtMovimiento.Text = objBusquedaMov._SMovimiento;
                txtMovimiento_LostFocus(sender, e);
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

            MovimientosABC movimiento = new MovimientosABC()
            {
                idmovimiento = Convert.ToInt32(txtMovimiento.Text),
                cant_entregas = Convert.ToInt32(txtCantidad.Text),
                idrol = objControlador.RetornarIdRol(cbRol.SelectedValue.ToString()),
                idtipo = objControlador.RetornarIdTipo(cbTipo.SelectedValue.ToString()),
                fecha_movimiento = Convert.ToDateTime(dtFecha.Text)
            };

            if (objControlador.ActualizarMovimiento(movimiento))
            {
                MessageBox.Show("Se guardo con exito!...");
                btnLimpiar_Click(sender, e);
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Seguro que quiere eliminar el Registro?", "Eliminar Registro", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (objControlador.EliminarMovimiento(Convert.ToInt32(txtMovimiento.Text)))
                {
                    MessageBox.Show("Se elimino con exito!...");
                    btnLimpiar_Click(sender, e);
                }
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtCantidad.Text = string.Empty;
            cbRol.SelectedIndex = -1;
            cbTipo.SelectedIndex = -1;
            txtMovimiento.IsEnabled = true;
            txtMovimiento.Focus();
        }

        private void txtMovimiento_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtMovimiento.Text != string.Empty)
            {
                
                var movimiento = objControlador.BuscarMovimiento(Convert.ToInt32(txtMovimiento.Text));
                txtMovimiento.IsEnabled = false;
                if (movimiento.Count > 0)
                {
                    bExiste = true;
                    foreach (var datos in movimiento)
                    {
                        txtCantidad.Text = datos._ICantidad.ToString();
                        cbRol.SelectedIndex = datos._IRol - 1;
                        cbTipo.SelectedIndex = datos._ITipo - 1;
                        dtFecha.Text = datos._DtFecha;
                    }

                    txtCantidad.Focus();
                }
                else
                {
                    bExiste = false;
                    txtMovimiento.IsEnabled = true;
                    txtMovimiento.Focus();
                }
            }
            else
            {
                bExiste = false;
                txtMovimiento.IsEnabled = true;
                txtMovimiento.Focus();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                bEsc = true;
                this.Close();
            }
        }

        private void txtMovimiento_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (bExiste == false && !bEsc)
            {
                MessageBox.Show("Ingrese un id valido.", "Error", MessageBoxButton.OK);
                e.Handled = true;
                bExiste = true;
            }
        }

    }
}
