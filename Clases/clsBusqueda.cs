using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abcCompleto
{
    public class clsBusqueda : clsEmpleado
    {

        private int idNumEmpleado;
        private string sNombreCompleto;

        public string _SNombreCompleto
        {
            get { return sNombreCompleto; }
            set { sNombreCompleto = value; }
        }

        public int _IdNumEmpleado
        {
            get { return idNumEmpleado; }
            set { idNumEmpleado = value; }
        }

        public clsBusqueda() 
        {
           
        }

        
    }
}
