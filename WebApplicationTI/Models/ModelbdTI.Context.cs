//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplicationTI.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class bdTIEntities : DbContext
    {
        public bdTIEntities()
            : base("name=bdTIEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Depto> Depto { get; set; }
        public virtual DbSet<Dispositivos> Dispositivos { get; set; }
        public virtual DbSet<DVR> DVR { get; set; }
        public virtual DbSet<Herramienta> Herramienta { get; set; }
        public virtual DbSet<Impresoras> Impresoras { get; set; }
        public virtual DbSet<Proveedor> Proveedor { get; set; }
        public virtual DbSet<Router> Router { get; set; }
        public virtual DbSet<Switch> Switch { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<EquipoComputo> EquipoComputo { get; set; }
        public virtual DbSet<Monitor> Monitor { get; set; }
        public virtual DbSet<Consumibles> Consumibles { get; set; }
        public virtual DbSet<Cuenta> Cuenta { get; set; }
        public virtual DbSet<Camaras> Camaras { get; set; }
    }
}
