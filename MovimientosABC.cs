//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace abcCompleto
{
    using System;
    using System.Collections.Generic;
    
    public partial class MovimientosABC
    {
        public int idmovimiento { get; set; }
        public int idnumempleado { get; set; }
        public Nullable<int> cant_entregas { get; set; }
        public int idrol { get; set; }
        public int idtipo { get; set; }
        public Nullable<System.DateTime> fecha_movimiento { get; set; }
    
        public virtual EmpleadoABC EmpleadoABC { get; set; }
    }
}
