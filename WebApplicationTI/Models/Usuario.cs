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
    using System.Collections.Generic;
    
    public partial class Usuario
    {
        public Usuario user { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            this.EquipoComputo = new HashSet<EquipoComputo>();
        }
    
        public int IdUsuario { get; set; }
        public int IdDepto { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Ext { get; set; }
    
        public virtual Depto Depto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EquipoComputo> EquipoComputo { get; set; }
    }
}