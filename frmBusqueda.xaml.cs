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
        public frmBusqueda()
        {
            InitializeComponent();
            sNumEmpleado = string.Empty;
            txtBusquedaEmp.Focus();
        }
        clsRN objCale = new clsRN();
        private void txtBusquedaEmp_TextChanged(object sender, TextChangedEventArgs e)
        {
            //DataContext = objCale.BuscarEmpleadoLike(txtBusquedaEmp.Text);
            dtgEmpleados.ItemsSource = objCale.BuscarEmpleadoLike(txtBusquedaEmp.Text);
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void dtgEmpleados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DatosEmpleado row = dtgEmpleados.SelectedItem as DatosEmpleado;
            sNumEmpleado = row.idNumEmpleado.ToString();
            this.Close();
        }


    }
}
