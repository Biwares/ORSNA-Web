using System;
using System.Collections.Generic;

namespace BD.Models
{
    public partial class ProyectosEstado
    {
        public ProyectosEstado()
        {
            Proyectos = new HashSet<Proyectos>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<Proyectos> Proyectos { get; set; }
    }
}
