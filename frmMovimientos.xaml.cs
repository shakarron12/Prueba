﻿using System;
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
    /// Lógica de interacción para frmMovimientos.xaml
    /// </summary>
    public partial class frmMovimientos : Window
    {
        clsMovimiento objControlador = new clsMovimiento();
        private int iRol;
        private int iNumEmpleado;

        public int _INumEmpleado
        {
            get { return iNumEmpleado; }
            set { iNumEmpleado = value; }
        }

        public int _IRol
        {
            get { return iRol; }
            set { iRol = value; }
        }
        
        public frmMovimientos(string sNumEmpleado, string sNombreCompleto, int iNumEmpleado,int iRol, int iTipo)
        {
            InitializeComponent();
            txtNumeroEmp.Text = sNumEmpleado;
            txtNombre.Text = sNombreCompleto;
            this._IRol = iRol;
            this._INumEmpleado = iNumEmpleado;
            cbRol.ItemsSource = objControlador.RetornarRoles();
            cbTipo.ItemsSource = objControlador.RetornarTipos();

            cbRol.SelectedIndex = iRol - 1;
            cbTipo.SelectedIndex = iTipo - 1;

            dtFecha.Text = DateTime.Today.ToShortDateString();
        }

        private void btnSubir_Click(object sender, RoutedEventArgs e)
        {
            txtCantidad.Text = ((Convert.ToInt32(txtCantidad.Text) + 1).ToString());
        }

        private void btnBajar_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(txtCantidad.Text) > 0)
                txtCantidad.Text = ((Convert.ToInt32(txtCantidad.Text) - 1).ToString());
        }

        private void frmMovimientos1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (objControlador.ValidarControlesVacios(GridControles.Children))
            {
                if (Convert.ToInt32(txtCantidad.Text) > 0)
                {
                    MovimientosABC movimiento = new MovimientosABC()
                    {
                        idnumempleado = iNumEmpleado, 
                        cant_entregas = Convert.ToInt32(txtCantidad.Text),
                        idrol = objControlador.RetornarIdRol(cbRol.SelectedValue.ToString()),
                        idtipo = objControlador.RetornarIdTipo(cbTipo.SelectedValue.ToString()),
                        fecha_movimiento = Convert.ToDateTime(dtFecha.Text)
                    };

                    if (objControlador.GuardarMovimiento(movimiento))
                    {
                        MessageBox.Show("Se guardo con exito!...");
                        this.Close();
                    }
                }
                else 
                {
                    MessageBox.Show("Favor de agregar por lo menos 1 cantidad entregada.");
                }
            }
            else 
            {
                MessageBox.Show("No debe haber campos vacios.");
                txtNombre.Focus();
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            frmModificarMovimientos objModificar = new frmModificarMovimientos(_INumEmpleado);
            objModificar.ShowDialog();
        }

        private void txtCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (objControlador.isCaracterValido(e.Key, "numero"))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtCantidad_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
