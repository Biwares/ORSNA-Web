using System;
using System.Collections.Generic;

namespace BD.Models
{
    public partial class Modulos
    {
        public Modulos()
        {
            AreasModulos = new HashSet<AreasModulos>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string RouterLink { get; set; }
        public string Icono { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<AreasModulos> AreasModulos { get; set; }
    }
}
