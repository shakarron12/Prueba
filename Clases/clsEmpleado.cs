using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Data.Entity.Core;

namespace abcCompleto
{
    public abstract class clsEmpleado : clsRN 
    {
        public clsEmpleado() 
        {
        }

        internal bool VerificarConexionPrin()
        {
            return VerificarConexionRN();
        }
        
        internal bool ValidarControlesVacios(UIElementCollection uiControles)
        {
            bool bRegresa = true;

            foreach (UIElement Control in uiControles)
            {
                if(!bRegresa)
                    break;

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

        //EMPLEADOS
        internal abcCompleto.EmpleadoABC BuscarEmpleado(int iNumEmpleado) 
        {
            EmpleadoABC empleado = new EmpleadoABC();
            try
            {
                empleado = BuscarEmpleadoRN(iNumEmpleado);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return empleado;
        }

        internal bool EliminarEmpleado(int iNumEmpleado)
        {
            bool bRegresa = false;
            try
            {
                EliminarEmpleadoRN(iNumEmpleado);
                bRegresa = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return bRegresa;
        }

        internal bool GuardarEmpleado(EmpleadoABC empleado)
        {
            bool bRegresa = false;
            try
            {
                GuardarEmpleadoRN(empleado);
                bRegresa = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return bRegresa;
        }

        internal bool ActualizarEmpleado(EmpleadoABC empleado)
        {
            bool bRegresa = false;
            try
            {
                ActualizarEmpleadoRN(empleado);
                bRegresa = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return bRegresa;
        }

        internal List<string> RetornarRoles()
        {
            List<string> roles = new List<string>();
            try
            {
                roles = llenarComboRolRN();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return roles;
        }

        internal List<string> RetornarTipos()
        {
            List<string> tipos = new List<string>();
            try
            {
                tipos = LlenarComboTipoRN();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return tipos;
        }

        internal int RetornarIdRol(string sNombreRol)
        {
            int iRol = 0;
            try
            {
                iRol = RetornaridRolRN(sNombreRol);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return iRol;
        }

        internal int RetornarIdTipo(string sNombreTipo)
        {
            int iTipo = 0;
            try
            {
                iTipo = RetornaridTipoRN(sNombreTipo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return iTipo;
        }

        internal List<clsBusqueda> BuscarEmpleado(string sEmpleado)
        {
            List<abcCompleto.clsBusqueda> empleados = new List<clsBusqueda>();
            try
            {
                empleados = BuscarEmpleadoLikeRN(sEmpleado);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return empleados;
        }

        //MOVIIMIENTOS 

        internal bool EliminarMovimiento(int iNumMovimiento)
        {
            bool bRegresa = false;
            try
            {
                EliminarMovimientoRN(iNumMovimiento);
                bRegresa = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return bRegresa;
        }

        internal bool GuardarMovimiento(MovimientosABC movimiento)
        {
            bool bRegresa = false;
            try
            {
                GuardarMovimientoRN(movimiento);
                bRegresa = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return bRegresa;
        }

        internal bool ActualizarMovimiento(MovimientosABC movimiento)
        {
            bool bRegresa = false;
            try
            {
                ActualizarMovimientoRN(movimiento);
                bRegresa = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return bRegresa;
        }

        internal List<clsBusquedaMovimiento> BuscarMovimientoLike(int iNumEmpleado, DateTime dtFechaInicio, DateTime dtFechaFin)
        {
            List<clsBusquedaMovimiento> movimiento = new List<clsBusquedaMovimiento>();

            try
            {
                movimiento = BuscarMovimientoLikeRN(iNumEmpleado, dtFechaInicio, dtFechaFin);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return movimiento;
        }

        internal List<clsMovimiento> BuscarMovimiento(int iNumMovimiento)
        {
            List<clsMovimiento> movimiento = new List<clsMovimiento>();

            try
            {
                movimiento = BuscarMovimientoRN(iNumMovimiento);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return movimiento;
        }

        internal int retornarVales(int iNumEmpleado)
        {
            int valestotales = 0;
            try
            {
                List<MovimientosABC> valesTotalesEmpleado = RetornarValesRN(iNumEmpleado);
                foreach (MovimientosABC movimiento in valesTotalesEmpleado)
                {
                    valestotales += (int)movimiento.cant_entregas;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return valestotales;
        }

        //SALARIOS
        internal List<SalarioABC> BuscarSalario(int iNumEmp)
        {
            List<abcCompleto.SalarioABC> salario = new List<SalarioABC>();
            try
            {
                salario = BuscarSalarioRN(iNumEmp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return salario;
        }

        internal List<SalarioABC> BuscarSalarios()
        {
            List<abcCompleto.SalarioABC> salarios = new List<SalarioABC>();
            salarios.Clear();
            try
            {
                salarios = BuscarSalariosTotalesRN();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return salarios;
        }

        internal bool EliminarSalario(int iNumEmpleado)
        {
            bool bRegresa = false;
            try
            {
                EliminarSalarioRN(iNumEmpleado);
                bRegresa = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return bRegresa;
        }

        internal bool GuardarSalario(SalarioABC movimiento)
        {
            bool bRegresa = false;
            try
            {
                GuardarSalarioRN(movimiento);
                bRegresa = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return bRegresa;
        }

        internal bool ActualizarSalario(SalarioABC movimiento)
        {
            bool bRegresa = false;
            try
            {
               ActualizarSalarioRN(movimiento);
                bRegresa = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return bRegresa;
        }

        //HORARIOS
        internal List<HorariosABC> BuscarHorarios(int iNumEmpleado)
        {
            List<HorariosABC> horarios = new List<HorariosABC>();

            try
            {
                horarios = BuscarHorariosRN(iNumEmpleado);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return horarios;
        }

        internal bool GuardarHorario(HorariosABC horario)
        {
            bool bRegresa = false;
            try
            {
                GuardarHorarioRN(horario);
                bRegresa = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return bRegresa;
        }

        internal int RetornarBonoRol(int idRol)
        {
            int iBono = 0;
            try
            {
                iBono = RetornarBonoRolRN(idRol);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return iBono;
        }
        
    }
}
