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
     
    public partial class EmpleadoABC
    {
        public EmpleadoABC()
        {
            this.MovimientosABC = new HashSet<MovimientosABC>();
            this.SalarioABC = new HashSet<SalarioABC>();
            this.HorariosABC = new HashSet<HorariosABC>();
        }
    
        public int idNumEmpleado { get; set; }
        public string nombre { get; set; }
        public string primerap { get; set; }
        public string segundoap { get; set; }
        public string direccion { get; set; }
        public string curp { get; set; }
        public Nullable<System.DateTime> fechanac { get; set; }
        public int idrol { get; set; }
        public int idtipo { get; set; }
        public byte[] img_usuario { get; set; }
    
        public virtual RolABC RolABC { get; set; }
        public virtual TipoABC TipoABC { get; set; }
        public virtual ICollection<MovimientosABC> MovimientosABC { get; set; }
        public virtual ICollection<SalarioABC> SalarioABC { get; set; }
        public virtual ICollection<HorariosABC> HorariosABC { get; set; }
    }
}
