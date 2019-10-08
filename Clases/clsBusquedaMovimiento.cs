using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abcCompleto
{

   public class clsBusquedaMovimiento : clsEmpleado
    {

        private int idMovimiento;
        private int iNumEmpleado;
        private string dtFecha;

        public int _IdMovimiento
        {
            get { return idMovimiento; }
            set { idMovimiento = value; }
        }
       
        public int _INumEmpleado
        {
            get { return iNumEmpleado; }
            set { iNumEmpleado = value; }
        }

        public string _DtFecha
        {
            get { return dtFecha; }
            set { dtFecha = value; }
        }

        public clsBusquedaMovimiento() 
        {   
           
        }

    }
}
