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
    /// Lógica de interacción para frmBusquedaMovimientos.xaml
    /// </summary>
    public partial class frmBusquedaMovimientos : Window
    {
        clsBusquedaMovimiento objControlador;
        private string sMovimiento;
        private int iNumEmpleado;

        public string _SMovimiento
        {
            get { return sMovimiento; }
            set { sMovimiento = value; }
        }

        public int _INumEmpleado
        {
            get { return iNumEmpleado; }
            set { iNumEmpleado = value; }
        }

        public frmBusquedaMovimientos(int iNumEmpleado)
        {
            InitializeComponent();
            objControlador = new clsBusquedaMovimiento();
            this._INumEmpleado = iNumEmpleado;
        }

        private void dtgMovimientos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                clsBusquedaMovimiento row = dtgMovimientos.SelectedItem as clsBusquedaMovimiento;
                sMovimiento = row._IdMovimiento.ToString();
                this.Close();
            }
            catch { }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (objControlador.ValidarControlesVacios(gdControles.Children))
            {
                dtgMovimientos.ItemsSource = objControlador.BuscarMovimientoLike(_INumEmpleado, Convert.ToDateTime(dtFechaInicio.Text), Convert.ToDateTime(dtFechaFin.Text));
            }
        }

        private void frmBusqueda_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Escape)
                this.Close();
        }
    }
}
