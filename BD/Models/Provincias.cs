using System;
using System.Collections.Generic;

namespace BD.Models
{
    public partial class Provincias
    {
        public Provincias()
        {
            Aeropuertos = new HashSet<Aeropuertos>();
            ProyectoProvincias = new HashSet<ProyectoProvincias>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<Aeropuertos> Aeropuertos { get; set; }
        public virtual ICollection<ProyectoProvincias> ProyectoProvincias { get; set; }
    }
}
