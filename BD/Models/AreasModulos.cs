using System;
using System.Collections.Generic;

namespace BD.Models
{
    public partial class AreasModulos
    {
        public AreasModulos()
        {
            AreasModulosPermisos = new HashSet<AreasModulosPermisos>();
        }

        public int Id { get; set; }
        public int IdArea { get; set; }
        public int IdModulo { get; set; }
        public bool? Estado { get; set; }

        public virtual Areas IdAreaNavigation { get; set; }
        public virtual Modulos IdModuloNavigation { get; set; }
        public virtual ICollection<AreasModulosPermisos> AreasModulosPermisos { get; set; }
    }
}
