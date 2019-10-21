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
using System.Windows.Input;

namespace abcCompleto
{
    public abstract class clsEmpleado : clsRN 
    {
        public clsEmpleado() 
        {
        }

        //METODOS GENERALES

        /// <summary>
        /// Verifica que la conexion siga en pie.
        /// </summary>
        /// <returns>Bool</returns>
        internal bool VerificarConexionPrin()
        {
            return VerificarConexionRN();
        }

        /// <summary>
        /// Realiza un recorrido por todos los grids y valida que no se encuentren vacios.
        /// <param name="uiControles">Grid con los controles de la ventana.</param>
        /// </summary>
        /// <returns>Bool</returns>
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

        /// <summary>
        /// Valida que el caracter que recibe sea correcto.
        /// <param name="key">Tecla presionada por el usuario.</param>
        /// <param name="sValidacion">Tipo de validacion ( EsNumero || EsLetra).</param>
        /// </summary>
        /// <returns>Bool</returns>
        internal bool isCaracterValido(Key key, string sValidacion)
        {
            var cDato = (Char)KeyInterop.VirtualKeyFromKey(key);

            if (key == Key.Escape)
            {
                return true;
            }
            else if (key == Key.Tab)
            {
                return true;
            }
            else if (key == Key.OemPeriod)
            {
                return false;
            }
            else if (key == Key.Space)
            {
                return false;
            }
            else if (sValidacion.ToLower() == "numero")
            {
                if (key.ToString().StartsWith("NumPad"))
                {
                    return true;
                }
                else if (char.IsNumber(cDato))
                {
                    return true;
                }
            }
            else if (sValidacion.ToLower() == "letra")
            {
                if (char.IsLetter(cDato) || char.IsSeparator(cDato))
                {
                    return true;
                }
            }

            return false;
        }

        //EMPLEADOS

        /// <summary>
        /// Retorna un empleado.
        /// <param name="iNumEmpleado">Numero del empleado a buscar.</param>
        /// </summary>
        /// <returns>Bool</returns>
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

        /// <summary>
        /// Elimina un empleado.
        /// <param name="iNumEmpleado">Numero del empleado a eliminar.</param>
        /// </summary>
        /// <returns>Bool</returns>
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

        /// <summary>
        /// Guarda un empleado nuevo.
        /// <param name="empleado">Estructura que contiene los datos de un empleado.</param>
        /// </summary>
        /// <returns>Bool</returns>
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

        /// <summary>
        /// Modifica un empleado existente.
        /// <param name="empleado">Estructura que contiene los datos de un empleado.</param>
        /// </summary>
        /// <returns>Bool</returns>
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

        /// <summary>
        /// Regresa todos los roles de la BD.
        /// </summary>
        /// <returns>List<string></returns>
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

        /// <summary>
        /// Regresa todos los tipos de la BD.
        /// </summary>
        /// <returns>List<string></returns>
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

        /// <summary>
        /// Regresa el id del Rol.
        /// <param name="sNombreRol">Nombre del rol para filtrar la busqueda del ID.</param>
        /// </summary>
        /// <returns>int</returns>
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

        /// <summary>
        /// Regresa el id del Tipo.
        /// </summary>
        /// <param name="sNombreTipo">Nombre del Tipo de empleado (externo || interno).</param>
        /// <returns>int</returns>
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

        /// <summary>
        /// Regresa los empleados que contengan lo que se va escribiendo en el control.
        /// </summary>
        /// <param name="sEmpleado">Filtro para la busqueda del empleado.</param>
        /// <returns>List</returns>
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

        /// <summary>
        /// Elimina un movimiento.
        /// </summary>
        /// <param name="iNumMovimiento">Numero del movimiento a eliminar.</param>
        /// <returns>bool</returns>
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

        /// <summary>
        /// Guarda un movimiento nuevo.
        /// </summary>
        /// <param name="movimiento">Estructura que contiene los datos del mnovimiento a guardar.</param>
        /// <returns>bool</returns>
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

        /// <summary>
        /// Actualiza un movimiento existente.
        /// </summary>
        /// <param name="movimiento">Estructura que contiene los datos del mnovimiento a actualizar.</param>
        /// <returns>bool</returns>
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

        /// <summary>
        /// Regresa los movimientos realizados en un rango de tiempo por el empleado.
        /// </summary>
        /// <param name="iNumEmpleado">Numero del empleado.</param>
        /// <param name="dtFechaInicio">Fecha inicial para el rango de busqueda.</param>
        /// <param name="dtFechaFin">Fecha final para el rango de busqueda.</param>
        /// <returns>List</returns>
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

        /// <summary>
        /// Regresa un movimiento.
        /// </summary>
        /// <param name="iNumMovimiento">Id del movimiento.</param>
        /// <returns>List</returns>
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

        /// <summary>
        /// Regresa la cantidad de vales que registro el empleado.
        /// </summary>
        /// <param name="iNumEmpleado">Numero de empleado para la busqueda de los vales.</param>
        /// <returns>int</returns>
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

        /// <summary>
        /// Regresa el salario de un empleado.
        /// </summary>
        /// <param name="iNumEmp">Numero de empleado.</param>
        /// <returns>List</returns>
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

        /// <summary>
        /// Regresa todos los salarios de la BD.
        /// </summary>
        /// <returns>List</returns>
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

        /// <summary>
        /// Elimina el salario del empleado.
        /// </summary>
        /// <param name="iNumEmpleado">Numero del empleado.</param>
        /// <returns>bool</returns>
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

        /// <summary>
        /// Guarda un salario nuevo.
        /// </summary>
        /// <param name="salario">Estructura del salario a guardar.</param>
        /// <returns>bool</returns>
        internal bool GuardarSalario(SalarioABC salario)
        {
            bool bRegresa = false;
            try
            {
                GuardarSalarioRN(salario);
                bRegresa = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return bRegresa;
        }

        /// <summary>
        /// Guarda un salario existente.
        /// </summary>
        /// <param name="salario">Estructura del salario a modificar.</param>
        /// <returns>bool</returns>
        internal bool ActualizarSalario(SalarioABC salario)
        {
            bool bRegresa = false;
            try
            {
               ActualizarSalarioRN(salario);
                bRegresa = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return bRegresa;
        }

        //HORARIOS

        /// <summary>
        /// Regresa los registros de horario de un empleado.
        /// </summary>
        /// <param name="iNumEmpleado">Numero del empleado.</param>
        /// <returns>bool</returns>
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

        /// <summary>
        /// Guarda un horario.
        /// </summary>
        /// <param name="horario">Estructura del horario a guardar.</param>
        /// <returns>bool</returns>
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

        /// <summary>
        /// Regresa el bono que le pertenece al rol.
        /// </summary>
        /// <param name="idRol">Id del rol a buscar.</param>
        /// <returns>int</returns>
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
