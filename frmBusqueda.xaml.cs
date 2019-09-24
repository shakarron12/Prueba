using System;
using System.Collections.Generic;
using System.Data;
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
using System.ComponentModel;

namespace abcCompleto
{
    /// <summary>
    /// Lógica de interacción para frmBusqueda.xaml
    /// </summary>
    public partial class frmBusqueda : Window
    {
        public string sNumEmpleado;
        clsBusqueda objControlador;

        public frmBusqueda()
        {
            InitializeComponent();
            objControlador = new clsBusqueda();
            sNumEmpleado = string.Empty;
            txtBusquedaEmp.Focus();
        }

        private void txtBusquedaEmp_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtBusquedaEmp.Text != string.Empty)
                dtgEmpleados.ItemsSource = objControlador.BuscarEmpleado(txtBusquedaEmp.Text);
            else
                dtgEmpleados.ItemsSource = null;
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();
        }

        private void dtgEmpleados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                clsBusqueda row = dtgEmpleados.SelectedItem as clsBusqueda;
                sNumEmpleado = row._IdNumEmpleado.ToString();
                this.Close();
            }
            catch { }
        }


    }
}
