using System;
using System.Collections.Generic;

namespace BD.Models
{
    public partial class Areas
    {
        public Areas()
        {
            AreasModulos = new HashSet<AreasModulos>();
            Proyectos = new HashSet<Proyectos>();
            UsuariosAreas = new HashSet<UsuariosAreas>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<AreasModulos> AreasModulos { get; set; }
        public virtual ICollection<Proyectos> Proyectos { get; set; }
        public virtual ICollection<UsuariosAreas> UsuariosAreas { get; set; }
    }
}
