using System;
using System.Collections.Generic;

namespace BD.Models
{
    public partial class ProyectoProvincias
    {
        public int Id { get; set; }
        public int? IdProyecto { get; set; }
        public int? IdProvincia { get; set; }
        public bool? Estado { get; set; }

        public virtual Provincias IdProvinciaNavigation { get; set; }
        public virtual Proyectos IdProyectoNavigation { get; set; }
    }
}
