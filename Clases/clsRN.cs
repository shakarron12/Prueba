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

        protected void RefrescarEntidades() 
        {
            foreach (var entity in odb.ChangeTracker.Entries())
            {
                entity.Reload();
            }
        }

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
        protected void EliminarEmpleadoRN(int sNumEmpleado)
        {
            var resultado = (from a in odb.EmpleadoABC where a.idNumEmpleado == sNumEmpleado select a).Single();
            odb.EmpleadoABC.Remove(resultado);
            odb.SaveChanges();
        }

        protected abcCompleto.EmpleadoABC BuscarEmpleadoRN(int sNumEmpleado)
        {
            EmpleadoABC empleadoRetornar = ((from a in odb.EmpleadoABC where a.idNumEmpleado == sNumEmpleado select a).ToList().Count) > 0
                ? (from a in odb.EmpleadoABC where a.idNumEmpleado == sNumEmpleado select a).ToList()[0]
                : null;
            return empleadoRetornar;
            //return (from a in odb.EmpleadoABC where a.idNumEmpleado == sNumEmpleado select a).ToList()[0];
        }

        protected List<clsBusqueda> BuscarEmpleadoLikeRN(string sNombreUser)
        {
            return (from a in odb.EmpleadoABC where a.nombre.ToString().Contains(sNombreUser) || a.primerap.ToString().Contains(sNombreUser) || a.segundoap.ToString().Contains(sNombreUser) 
                    select new clsBusqueda { _IdNumEmpleado = a.idNumEmpleado, _SNombreCompleto = (a.nombre + " " + a.primerap + " " + a.segundoap) }).ToList();
        }

        protected void GuardarEmpleadoRN(EmpleadoABC empleado)
        {
            odb.EmpleadoABC.Add(empleado);
            odb.SaveChanges();
        }

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
            return (int)(from a in odb.RolABC where a.desc_rol == (idRol) select a.idrol).ToList()[0];;
        }

        protected int RetornaridTipoRN(string idTipo)
        {
            return (int)(from a in odb.TipoABC where a.desc_tipo == (idTipo) select a.idtipo).ToList()[0];
        }

        protected int RetornarBonoRolRN(int idRol) 
        {
            return (int)(from a in odb.RolABC where a.idrol == (idRol) select a.bono).ToList()[0];;
        }

        //MOVIMIENTOS
        protected void GuardarMovimientoRN(MovimientosABC movimiento)
        {
            odb.MovimientosABC.Add(movimiento);
            odb.SaveChanges();
        }

        protected void EliminarMovimientoRN(int idMovimiento)
        {
            var resultado = (from a in odb.MovimientosABC where a.idmovimiento == idMovimiento select a).Single();

            odb.MovimientosABC.Remove(resultado);
            odb.SaveChanges();
        }

        protected void ActualizarMovimientoRN(MovimientosABC movimiento)
        {
            var movimiento_finded = odb.MovimientosABC.Find(movimiento.idmovimiento);
            movimiento_finded.cant_entregas = movimiento.cant_entregas;
            movimiento_finded.idrol = movimiento.idrol;
            movimiento_finded.idtipo = movimiento.idtipo;
            movimiento_finded.fecha_movimiento = movimiento.fecha_movimiento;
            odb.SaveChanges();
        }

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

        protected List<MovimientosABC> RetornarValesRN(int iNumEmpleado)
        {
            RefrescarEntidades();
            return (from a in odb.MovimientosABC
                    where a.idnumempleado == iNumEmpleado
                    select a).ToList();
        }

        //SALARIOS
        protected void GuardarSalarioRN(SalarioABC salario)
        {
            odb.SalarioABC.Add(salario);
            odb.SaveChanges();
        }

        protected void EliminarSalarioRN(int idEmpleado)
        {
            var resultado = (from a in odb.SalarioABC where a.idNumEmpleado == idEmpleado select a).Single();
            odb.SalarioABC.Remove(resultado);
            odb.SaveChanges();
        }

        protected void ActualizarSalarioRN(SalarioABC salario)
        {
            var salario_finded = (from a in odb.SalarioABC
                                  where a.idNumEmpleado == salario.idNumEmpleado
                                  select a).FirstOrDefault();
            salario_finded.salario_mensual = salario.salario_mensual;
            odb.SaveChanges();
            salario_finded = (from a in odb.SalarioABC
                              where a.idNumEmpleado == salario.idNumEmpleado
                              select a).FirstOrDefault();
        }

        protected List<abcCompleto.SalarioABC> BuscarSalarioRN(int iNumEmpleado)
        {
            RefrescarEntidades();            
            return (from a in odb.SalarioABC where a.idNumEmpleado == iNumEmpleado select a).ToList();

        }
       
        protected List<abcCompleto.SalarioABC> BuscarSalariosTotalesRN()
        {
            RefrescarEntidades();
            return (from a in odb.SalarioABC select a).ToList();
        }

        //HORARIOS
        protected List<HorariosABC> BuscarHorariosRN(int iNumEmpleado) 
        {
            return (from a in odb.HorariosABC
                    where a.idnumempleado == iNumEmpleado && a.idtipo != 2
                    select a).ToList();
        }

        protected void GuardarHorarioRN(HorariosABC horario)
        {
            odb.HorariosABC.Add(horario);
            odb.SaveChanges();
        }
    }
}
