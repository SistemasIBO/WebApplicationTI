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
    
    public partial class DVR
    {
        public int IdDVR { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string NoSerie { get; set; }
        public Nullable<int> PuertoUSB { get; set; }
        public Nullable<int> NoCamaras { get; set; }
        public Nullable<int> PuertoVGA { get; set; }
        public string Estado { get; set; }
        public string Observaciones { get; set; }
    }
}
