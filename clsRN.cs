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
    public class DatosEmpleado
    {
        public int idNumEmpleado { get; set; }
        public string NombreCompleto { get; set; }
    }

    class clsRN
    {
        odbBodegaPrueba odb;
        public clsRN() 
        {
            odb = new odbBodegaPrueba();
        }

        internal bool EliminarEmpleado(int sNumEmpleado)
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

        internal List<abcCompleto.EmpleadoABC> BuscarEmpleado(int sNumEmpleado)
        {
            //DataTable datos = (from a in odb.EmpleadoABC where a.idNumEmpleado == sNumEmpleado select a).ToList();
            return (from a in odb.EmpleadoABC where a.idNumEmpleado == sNumEmpleado select a).ToList();
        }

        internal List<DatosEmpleado> BuscarEmpleadoLike(string sNombreUser)
        {
            //var datos = 
            return (from a in odb.EmpleadoABC where a.nombre.ToString().Contains(sNombreUser) select new DatosEmpleado { idNumEmpleado = a.idNumEmpleado, NombreCompleto = (a.nombre + " " + a.primerap + " " +a.segundoap)}).ToList();
        }

        internal bool GuardarEmpleado(EmpleadoABC empleado)
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

        internal bool ActualizarEmpleado(EmpleadoABC empleado)
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

        internal List<string> llenarComboRol()
        {
            return (from a in odb.RolABC select a.desc_rol).ToList();
        }

        internal List<string> llenarComboTipo()
        {
            return (from a in odb.TipoABC select a.desc_tipo).ToList();
        }

        internal int retornaridRol(string idRol)
        {
            return (int)(from a in odb.RolABC where a.desc_rol == (idRol) select a.idrol).ToList()[0];
        }

        internal int retornaridTipo(string idTipo)
        {
            return (int)(from a in odb.TipoABC where a.desc_tipo == (idTipo) select a.idtipo).ToList()[0];
        }
    }
}
