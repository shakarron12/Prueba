﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class odbBodegaPrueba : DbContext
    {
        public odbBodegaPrueba()
            : base("name=odbBodegaPrueba")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TipoABC> TipoABC { get; set; }
        public virtual DbSet<MovimientosABC> MovimientosABC { get; set; }
        public virtual DbSet<RolABC> RolABC { get; set; }
        public virtual DbSet<EmpleadoABC> EmpleadoABC { get; set; }
        public virtual DbSet<SalarioABC> SalarioABC { get; set; }
    }
}
