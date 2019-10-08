using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abcCompleto
{
    public class clsMovimiento : clsEmpleado
    {
        private int iCantidad;
        private int iRol;
        private int iTipo;
        private string dtFecha;

        public string _DtFecha
        {
            get { return dtFecha; }
            set { dtFecha = value; }
        }

        public int _ICantidad
        {
            get { return iCantidad; }
            set { iCantidad = value; }
        }

        public int _IRol
        {
            get { return iRol; }
            set { iRol = value; }
        }

        public int _ITipo
        {
            get { return iTipo; }
            set { iTipo = value; }
        }

        public clsMovimiento()
        {

        }
    }
}
