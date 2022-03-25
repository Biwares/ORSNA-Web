using System;
using System.Collections.Generic;

namespace BD.Models
{
    public partial class AreasModulosPermisos
    {
        public int Id { get; set; }
        public int? IdAreaModulo { get; set; }
        public int? IdPermiso { get; set; }
        public bool? Estado { get; set; }

        public virtual AreasModulos IdAreaModuloNavigation { get; set; }
        public virtual Permisos IdPermisoNavigation { get; set; }
    }
}
