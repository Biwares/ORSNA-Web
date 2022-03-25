using System;
using System.Collections.Generic;

namespace BD.Models
{
    public partial class Permisos
    {
        public Permisos()
        {
            AreasModulosPermisos = new HashSet<AreasModulosPermisos>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Key { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<AreasModulosPermisos> AreasModulosPermisos { get; set; }
    }
}
