using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace abcCompleto
{
    public abstract class clsEmpleado : clsRN
    {
        public clsEmpleado() 
        {
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

        internal List<EmpleadoABC> BuscarEmpleado(int iNumEmpleado) 
        {
            return BuscarEmpleadoRN(iNumEmpleado);
        }

        internal bool EliminarEmpleado(int iNumEmpleado) 
        {
            return EliminarEmpleadoRN(iNumEmpleado);
        }

        internal bool GuardarEmpleado(EmpleadoABC empleado) 
        {
           return GuardarEmpleadoRN(empleado);
        }

        internal bool ActualizarEmpleado(EmpleadoABC empleado)
        {
            return ActualizarEmpleadoRN(empleado);
        }

        internal List<string> RetornarRoles()
        {
            return llenarComboRolRN();
        }

        internal List<string> RetornarTipos()
        {
            return LlenarComboTipoRN();
        }

        internal int RetornarIdRol(string sNombreRol) 
        {
            return RetornaridRolRN(sNombreRol);
        }

        internal int RetornarIdTipo(string sNombreTipo)
        {
            return RetornaridTipoRN(sNombreTipo);
        }

        internal List<clsBusqueda> BuscarEmpleado(string sEmpleado)
        {
            return BuscarEmpleadoLikeRN(sEmpleado);
        }

        //MOVIIMIENTOS 
         
        internal bool EliminarMovimiento(int iNumMovimiento)
        {
            return EliminarMovimientoRN(iNumMovimiento);
        }

        internal bool GuardarMovimiento(MovimientosABC movimiento)
        {
            return GuardarMovimientoRN(movimiento);
        }

        internal bool ActualizarMovimiento(MovimientosABC movimiento)
        {
            return ActualizarMovimientoRN(movimiento);
        }

        internal List<clsBusquedaMovimiento> BuscarMovimientoLike(int iNumEmpleado, DateTime dtFechaInicio, DateTime dtFechaFin)
        {
            return BuscarMovimientoLikeRN(iNumEmpleado, dtFechaInicio, dtFechaFin);
        }

        internal List<clsMovimiento> BuscarMovimiento(int iNumMovimiento)
        {
            return BuscarMovimientoRN(iNumMovimiento);
        }

        internal int retornarVales(int iNumEmpleado) 
        {
            return RetornarValesRN(iNumEmpleado);
        }

        //SALARIOS
        internal List<SalarioABC> BuscarSalario(int iNumEmp)
        {
            return BuscarSalarioRN(iNumEmp);
        }

        internal List<SalarioABC> BuscarSalarios(int iNumEmp)
        {
            return BuscarSalariosTotalesRN();
        }

        internal bool EliminarSalario(int iNumEmpleado)
        {
            return EliminarSalarioRN(iNumEmpleado);
        }

        internal bool GuardarSalario(SalarioABC movimiento)
        {
            return GuardarSalarioRN(movimiento);
        }

        internal bool ActualizarSalario(SalarioABC movimiento)
        {
            return ActualizarSalarioRN(movimiento);
        }

       
    }
}
