using System;
using System.Collections.Generic;
using System.Data;
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

        //EMPLEADOS
        protected bool EliminarEmpleadoRN(int sNumEmpleado)
        {
            bool bRegresa = false;
            try
            {
                var resultado = (from a in odb.EmpleadoABC where a.idNumEmpleado == sNumEmpleado select a).Single();

                odb.EmpleadoABC.Remove(resultado);
                odb.SaveChanges();
                bRegresa = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return bRegresa;
        }

        protected List<abcCompleto.EmpleadoABC> BuscarEmpleadoRN(int sNumEmpleado)
        {
            return (from a in odb.EmpleadoABC where a.idNumEmpleado == sNumEmpleado select a).ToList();
        }

        internal List<clsBusqueda> BuscarEmpleadoLikeRN(string sNombreUser)
        {
            return (from a in odb.EmpleadoABC where a.nombre.ToString().Contains(sNombreUser) || a.primerap.ToString().Contains(sNombreUser) || a.segundoap.ToString().Contains(sNombreUser) 
                    select new clsBusqueda { _IdNumEmpleado = a.idNumEmpleado, _SNombreCompleto = (a.nombre + " " + a.primerap + " " + a.segundoap) }).ToList();
        }

        protected bool GuardarEmpleadoRN(EmpleadoABC empleado)
        {
            bool bRegresa = false;
            try
            {
                odb.EmpleadoABC.Add(empleado);
                odb.SaveChanges();
                bRegresa = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return bRegresa;
        }

        protected bool ActualizarEmpleadoRN(EmpleadoABC empleado)
        {
            bool bRegresa = false;
            try
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
                bRegresa = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return bRegresa;
        }

        protected List<string> llenarComboRolRN()
        {
            return (from a in odb.RolABC select a.desc_rol).ToList();
        }

        protected List<string> LlenarComboTipoRN()
        {
            return (from a in odb.TipoABC select a.desc_tipo).ToList();
        }

        protected int RetornaridRolRN(string idRol)
        {
            return (int)(from a in odb.RolABC where a.desc_rol == (idRol) select a.idrol).ToList()[0];
        }

        protected int RetornaridTipoRN(string idTipo)
        {
            return (int)(from a in odb.TipoABC where a.desc_tipo == (idTipo) select a.idtipo).ToList()[0];
        }

        //MOVIMIENTOS
        protected bool GuardarMovimientoRN(MovimientosABC movimiento)
        {
            bool bRegresa = false;
            try
            {
                odb.MovimientosABC.Add(movimiento);
                odb.SaveChanges();
                bRegresa = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return bRegresa;
        }

        protected bool EliminarMovimientoRN(int idMovimiento)
        {
            bool bRegresa = false;
            try
            {
                var resultado = (from a in odb.MovimientosABC where a.idmovimiento == idMovimiento select a).Single();

                odb.MovimientosABC.Remove(resultado);
                odb.SaveChanges();
                bRegresa = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return bRegresa;
        }

        protected bool ActualizarMovimientoRN(MovimientosABC movimiento)
        {
            bool bRegresa = false;
            try
            {
                var movimiento_finded = odb.MovimientosABC.Find(movimiento.idmovimiento);
                movimiento_finded.cant_entregas = movimiento.cant_entregas;
                movimiento_finded.idrol = movimiento.idrol;
                movimiento_finded.idtipo = movimiento.idtipo;
                movimiento_finded.fecha_movimiento = movimiento.fecha_movimiento;
                odb.SaveChanges();
                bRegresa = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return bRegresa;
        }

        internal List<clsBusquedaMovimiento> BuscarMovimientoLikeRN(int iNumEmpleado, DateTime dtFechaInicio, DateTime dtFechaFin)
        {
            return
                (from a in odb.MovimientosABC
                 where a.fecha_movimiento >= dtFechaInicio
                     && a.fecha_movimiento <= dtFechaFin
                     && a.idnumempleado == iNumEmpleado
                 select new clsBusquedaMovimiento { 
                     _IdMovimiento = a.idmovimiento, 
                     _INumEmpleado = a.idnumempleado, 
                     _DtFecha = a.fecha_movimiento.ToString() 
                 }).ToList();

        }

        internal List<clsMovimiento> BuscarMovimientoRN(int iNumMovimiento)
        {
            return
                (from a in odb.MovimientosABC
                 where a.idmovimiento == iNumMovimiento
                 select new clsMovimiento
                 {
                     _ICantidad = a.cant_entregas.Value,
                     _IRol = a.idrol,
                     _ITipo = a.idtipo,
                     _DtFecha = a.fecha_movimiento.ToString()
                 }).ToList();

        }

        protected int RetornarValesRN(int iNumEmpleado)
        {
            int valestotales = 0;
            List<MovimientosABC> valesTotalesEmpleado = (from a in odb.MovimientosABC
                                                         where a.idnumempleado == iNumEmpleado
                                                         select a).ToList();

            foreach (MovimientosABC movimiento in valesTotalesEmpleado)
            {
                valestotales = (int)movimiento.cant_entregas;
            }
            return valestotales;
        }

        //SALARIOS
        protected bool GuardarSalarioRN(SalarioABC salario)
        {
            bool bRegresa = false;
            try
            {
                odb.SalarioABC.Add(salario);
                odb.SaveChanges();
                bRegresa = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return bRegresa;
        }

        protected bool EliminarSalarioRN(int idEmpleado)
        {
            bool bRegresa = false;
            try
            {
                var resultado = (from a in odb.SalarioABC where a.idNumEmpleado == idEmpleado select a).Single();

                odb.SalarioABC.Remove(resultado);
                odb.SaveChanges();
                bRegresa = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return bRegresa;
        }

        protected bool ActualizarSalarioRN(SalarioABC salario)
        {
            bool bRegresa = false;
            try
            {
                var salario_finded = (from a in odb.SalarioABC
                             where a.idNumEmpleado == salario.idNumEmpleado
                             select a).FirstOrDefault();
                salario_finded.salario_mensual = salario.salario_mensual;
                odb.SaveChanges();
                bRegresa = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return bRegresa;
        }

        protected List<abcCompleto.SalarioABC> BuscarSalarioRN(int iNumEmpleado)
        {
            return (from a in odb.SalarioABC where a.idNumEmpleado == iNumEmpleado select a).ToList();
        }

        protected List<abcCompleto.SalarioABC> BuscarSalariosTotalesRN()
        {
            return (from a in odb.SalarioABC select a).ToList();
        }
    }
}
