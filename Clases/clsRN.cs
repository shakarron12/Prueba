using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace abcCompleto
{

    public class clsRN
    {
        odbBodegaPrueba odb;

        public clsRN()
        {
            odb = new odbBodegaPrueba();
        }

        /// <summary>
        /// Da un refresh a las entidades cargadas.
        /// </summary>
        protected void RefrescarEntidades()
        {
            foreach (var entity in odb.ChangeTracker.Entries())
            {
                entity.Reload();
            }
        }

        /// <summary>
        /// Verifica que la conexion siga en pie.
        /// </summary>
        /// <returns>Bool</returns>
        protected bool VerificarConexionRN()
        {
            bool bConected = false;

            if (odb.Database.Exists())
            {
                bConected = true;
            }
            return bConected;
        }

        //EMPLEADOS

        /// <summary>
        /// Elimina un empleado.
        /// </summary>
        /// <param name="iNumEmpleado">Numero del empleado a eliminar.</param>
        /// <returns>void</returns>
        protected void EliminarEmpleadoRN(int iNumEmpleado)
        {
            EliminarSalarioRN(iNumEmpleado);
            EliminarMovimientosEmpleadoRN(iNumEmpleado);
            EliminarHorarioRN(iNumEmpleado);

            var resultado = (from a in odb.EmpleadoABC where a.idNumEmpleado == iNumEmpleado select a).Single<EmpleadoABC>();
            odb.EmpleadoABC.Remove(resultado);
            odb.SaveChanges();
        }

        /// <summary>
        /// Retorna un empleado.
        /// <param name="iNumEmpleado">Numero del empleado a buscar.</param>
        /// </summary>
        /// <returns>Bool</returns>
        protected abcCompleto.EmpleadoABC BuscarEmpleadoRN(int iNumEmpleado)
        {
            EmpleadoABC empleadoRetornar = ((from a in odb.EmpleadoABC where a.idNumEmpleado == iNumEmpleado select a).ToList().Count) > 0
                ? (from a in odb.EmpleadoABC where a.idNumEmpleado == iNumEmpleado select a).ToList()[0]
                : null;
            return empleadoRetornar;
        }

        /// <summary>
        /// Regresa los empleados que contengan lo que se va escribiendo en el control.
        /// </summary>
        /// <param name="sEmpleado">Filtro para la busqueda del empleado.</param>
        /// <returns>List</returns>
        protected List<clsBusqueda> BuscarEmpleadoLikeRN(string sNombreUser)
        {
            return (from a in odb.EmpleadoABC
                    where a.nombre.ToString().Contains(sNombreUser) || a.primerap.ToString().Contains(sNombreUser) || a.segundoap.ToString().Contains(sNombreUser)
                    select new clsBusqueda { _IdNumEmpleado = a.idNumEmpleado, _SNombreCompleto = (a.nombre + " " + a.primerap + " " + a.segundoap) }).ToList();
        }

        /// <summary>
        /// Guarda un empleado nuevo.
        /// </summary>
        /// <param name="empleado">Estructura que contiene los datos de un empleado.</param>
        /// <returns>void</returns>
        protected void GuardarEmpleadoRN(EmpleadoABC empleado)
        {
            odb.EmpleadoABC.Add(empleado);
            odb.SaveChanges();
        }

        /// <summary>
        /// Modifica un empleado existente.
        /// </summary>
        /// <param name="empleado">Estructura que contiene los datos de un empleado.</param>
        /// <returns>void</returns>
        protected void ActualizarEmpleadoRN(EmpleadoABC empleado)
        {
            var empleado_finded = odb.EmpleadoABC.Find(empleado.idNumEmpleado);
            empleado_finded.nombre = empleado.nombre;
            empleado_finded.primerap = empleado.primerap;
            empleado_finded.segundoap = empleado.segundoap;
            empleado_finded.direccion = empleado.direccion;
            empleado_finded.curp = empleado.curp;
            empleado_finded.fechanac = empleado.fechanac;
            empleado_finded.idrol = empleado.idrol;
            empleado_finded.idtipo = empleado.idtipo;
            empleado_finded.img_usuario = empleado.img_usuario;
            odb.SaveChanges();
        }

        /// <summary>
        /// Regresa todos los roles de la BD.
        /// </summary>
        /// <returns>List</returns>
        protected List<string> llenarComboRolRN()
        {
            return (from a in odb.RolABC select a.desc_rol).ToList();
        }

        /// <summary>
        /// Regresa todos los tipos de la BD.
        /// </summary>
        /// <returns>List</returns>
        protected List<string> LlenarComboTipoRN()
        {
            return (from a in odb.TipoABC select a.desc_tipo).ToList();
        }

        /// <summary>
        /// Regresa el id del Rol.
        /// <param name="sNombreRol">Nombre del rol para filtrar la busqueda del ID.</param>
        /// </summary>
        /// <returns>int</returns>
        protected int RetornaridRolRN(string idRol)
        {
            return (int)(from a in odb.RolABC where a.desc_rol == (idRol) select a.idrol).ToList()[0]; ;
        }

        /// <summary>
        /// Regresa el id del Tipo.
        /// </summary>
        /// <param name="sNombreTipo">Nombre del Tipo de empleado (externo || interno).</param>
        /// <returns>int</returns>
        protected int RetornaridTipoRN(string sNombreTipo)
        {
            return (int)(from a in odb.TipoABC where a.desc_tipo == (sNombreTipo) select a.idtipo).ToList()[0];
        }

        /// <summary>
        /// Regresa el bono que le pertenece al rol.
        /// </summary>
        /// <param name="idRol">Id del rol a buscar.</param>
        /// <returns>int</returns>
        protected int RetornarBonoRolRN(int idRol)
        {
            return (int)(from a in odb.RolABC where a.idrol == (idRol) select a.bono).ToList()[0]; ;
        }

        //MOVIMIENTOS

        /// <summary>
        /// Guarda un movimiento nuevo.
        /// </summary>
        /// <param name="movimiento">Estructura que contiene los datos del mnovimiento a guardar.</param>
        /// <returns>void</returns>
        protected void GuardarMovimientoRN(MovimientosABC movimiento)
        {
            odb.MovimientosABC.Add(movimiento);
            odb.SaveChanges();
        }

        /// <summary>
        /// Elimina un movimiento.
        /// </summary>
        /// <param name="iNumEmpleado">Numero del empleado.</param>
        /// <returns>int</returns>
        protected int RetornarTipoEmpleadoRN(int iNumEmpleado)
        {
            return (from a in odb.EmpleadoABC where a.idNumEmpleado == iNumEmpleado select a.idtipo).Single();
        }

        /// <summary>
        /// Elimina un movimiento.
        /// </summary>
        /// <param name="iNumMovimiento">Numero del movimiento a eliminar.</param>
        /// <returns>void</returns>
        protected void EliminarMovimientoRN(int idMovimiento)
        {
            var resultado = (from a in odb.MovimientosABC where a.idmovimiento == idMovimiento select a).Single();
            odb.MovimientosABC.Remove(resultado);
            odb.SaveChanges();
        }

        /// <summary>
        /// Elimina todos los movimientos de un empleado.
        /// </summary>
        /// <param name="idNumEmpleado">Numero de empleado.</param>
        /// <returns>void</returns>
        protected void EliminarMovimientosEmpleadoRN(int idNumEmpleado)
        {
            List<MovimientosABC> MovimientosRetornar = ((from a in odb.MovimientosABC where a.idnumempleado == idNumEmpleado select a).ToList().Count) > 0
                ? (from a in odb.MovimientosABC where a.idnumempleado == idNumEmpleado select a).ToList()
                : null;
            //var resultado = (from a in odb.MovimientosABC where a.idnumempleado == idNumEmpleado select a).Single();
            if (MovimientosRetornar != null)
            {

                foreach (MovimientosABC MovimientoEliminar in MovimientosRetornar)
                {
                    odb.MovimientosABC.Remove(MovimientoEliminar);
                }
                
                odb.SaveChanges();
            }
        }

        /// <summary>
        /// Actualiza un movimiento existente.
        /// </summary>
        /// <param name="movimiento">Estructura que contiene los datos del mnovimiento a actualizar.</param>
        /// <returns>void</returns>
        protected void ActualizarMovimientoRN(MovimientosABC movimiento)
        {
            var movimiento_finded = odb.MovimientosABC.Find(movimiento.idmovimiento);
            movimiento_finded.cant_entregas = movimiento.cant_entregas;
            movimiento_finded.idrol = movimiento.idrol;
            movimiento_finded.idtipo = movimiento.idtipo;
            movimiento_finded.fecha_movimiento = movimiento.fecha_movimiento;
            odb.SaveChanges();
        }

        /// <summary>
        /// Regresa los movimientos realizados en un rango de tiempo por el empleado.
        /// </summary>
        /// <param name="iNumEmpleado">Numero del empleado.</param>
        /// <param name="dtFechaInicio">Fecha inicial para el rango de busqueda.</param>
        /// <param name="dtFechaFin">Fecha final para el rango de busqueda.</param>
        /// <returns>List</returns>
        protected List<clsBusquedaMovimiento> BuscarMovimientoLikeRN(int iNumEmpleado, DateTime dtFechaInicio, DateTime dtFechaFin)
        {
            RefrescarEntidades();
            return (from a in odb.MovimientosABC
                    where a.fecha_movimiento >= dtFechaInicio
                        && a.fecha_movimiento <= dtFechaFin
                        && a.idnumempleado == iNumEmpleado
                    select new clsBusquedaMovimiento
                    {
                        _IdMovimiento = a.idmovimiento,
                        _INumEmpleado = a.idnumempleado,
                        _DtFecha = a.fecha_movimiento.ToString()
                    }).ToList();
        }

        /// <summary>
        /// Regresa un movimiento.
        /// </summary>
        /// <param name="iNumMovimiento">Id del movimiento.</param>
        /// <returns>List</returns>
        protected List<clsMovimiento> BuscarMovimientoRN(int iNumMovimiento)
        {
            RefrescarEntidades();
            return (from a in odb.MovimientosABC
                    where a.idmovimiento == iNumMovimiento
                    select new clsMovimiento
                    {
                        _ICantidad = a.cant_entregas.Value,
                        _IRol = a.idrol,
                        _ITipo = a.idtipo,
                        _DtFecha = a.fecha_movimiento.ToString()
                    }).ToList();
        }

        /// <summary>
        /// Regresa los vales que registro el empleado.
        /// </summary>
        /// <param name="iNumEmpleado">Numero de empleado para la busqueda de los vales.</param>
        /// <returns>List</returns>
        protected List<MovimientosABC> RetornarValesRN(int iNumEmpleado)
        {
            RefrescarEntidades();
            return (from a in odb.MovimientosABC
                    where a.idnumempleado == iNumEmpleado
                    select a).ToList();
        }

        //SALARIOS

        /// <summary>
        /// Guarda un salario nuevo.
        /// </summary>
        /// <param name="salario">Estructura del salario a guardar.</param>
        /// <returns>void</returns>
        protected void GuardarSalarioRN(SalarioABC salario)
        {
            odb.SalarioABC.Add(salario);
            odb.SaveChanges();
        }

        /// <summary>
        /// Elimina el salario del empleado.
        /// </summary>
        /// <param name="iNumEmpleado">Numero del empleado.</param>
        /// <returns>void</returns>
        protected void EliminarSalarioRN(int iNumEmpleado)
        {
            SalarioABC SalarioRetornar = ((from a in odb.SalarioABC where a.idNumEmpleado == iNumEmpleado select a).ToList().Count) > 0
                ? (from a in odb.SalarioABC where a.idNumEmpleado == iNumEmpleado select a).ToList()[0]
                : null;
            //var resultado = (from a in odb.SalarioABC where a.idNumEmpleado == iNumEmpleado select a).Single();
            if (SalarioRetornar != null)
            {
                odb.SalarioABC.Remove(SalarioRetornar);
                odb.SaveChanges();
            }
        }

        /// <summary>
        /// Guarda un salario existente.
        /// </summary>
        /// <param name="salario">Estructura del salario a modificar.</param>
        /// <returns>void</returns>
        protected void ActualizarSalarioRN(SalarioABC salario)
        {
            var salario_finded = (from a in odb.SalarioABC
                                  where a.idNumEmpleado == salario.idNumEmpleado
                                  select a).FirstOrDefault();
            salario_finded.salario_mensual = salario.salario_mensual;
            odb.SaveChanges();
        }

        /// <summary>
        /// Regresa el salario de un empleado.
        /// </summary>
        /// <param name="iNumEmpleado">Numero de empleado.</param>
        /// <returns>List</returns>
        protected List<abcCompleto.SalarioABC> BuscarSalarioRN(int iNumEmpleado)
        {
            RefrescarEntidades();
            return (from a in odb.SalarioABC where a.idNumEmpleado == iNumEmpleado select a).ToList();

        }

        /// <summary>
        /// Regresa todos los salarios de la BD.
        /// </summary>
        /// <returns>List</returns>
        protected List<abcCompleto.SalarioABC> BuscarSalariosTotalesRN()
        {
            RefrescarEntidades();
            return (from a in odb.SalarioABC select a).ToList();
        }

        //HORARIOS

        /// <summary>
        /// Regresa los registros de horario de un empleado.
        /// </summary>
        /// <param name="iNumEmpleado">Numero del empleado.</param>
        /// <returns>List</returns>
        protected List<HorariosABC> BuscarHorariosRN(int iNumEmpleado)
        {
            return (from a in odb.HorariosABC
                    where a.idnumempleado == iNumEmpleado && a.idtipo != 2
                    select a).ToList();
        }

        /// <summary>
        /// Guarda un horario.
        /// </summary>
        /// <param name="horario">Estructura del horario a guardar.</param>
        /// <returns>void</returns>
        protected void GuardarHorarioRN(HorariosABC horario)
        {
            odb.HorariosABC.Add(horario);
            odb.SaveChanges();
        }

        /// <summary>
        /// Elimina los horarios de un empleado.
        /// </summary>
        /// <param name="iNumEmpleado">id del empleado.</param>
        /// <returns>void</returns>
        protected void EliminarHorarioRN(int iNumEmpleado)
        {
            List<HorariosABC> HorariosRetornar = ((from a in odb.HorariosABC where a.idnumempleado == iNumEmpleado select a).ToList().Count) > 0
                ? (from a in odb.HorariosABC where a.idnumempleado == iNumEmpleado select a).ToList()
                : null;
            //var resultado = (from a in odb.HorariosABC where a.idnumempleado == iNumEmpleado select a).Single();
            if (HorariosRetornar != null)
            {
                foreach (HorariosABC HorarioEliminar in HorariosRetornar)
                {
                    odb.HorariosABC.Remove(HorarioEliminar);
                }
                odb.SaveChanges();
            }
        }
    }
}