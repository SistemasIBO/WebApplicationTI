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
    
    public partial class Consumibles
    {
        public int IdConsumible { get; set; }
        public Nullable<int> IdProveedor { get; set; }
        public string Producto { get; set; }
        public string Descripcion { get; set; }
        public Nullable<double> Precio { get; set; }
        public Nullable<int> Cantidad { get; set; }
        public Nullable<System.DateTime> FechaCompra { get; set; }
        public string Observaciones { get; set; }
    
        public virtual Proveedor Proveedor { get; set; }
    }
}
