using System;
using System.Collections.Generic;

namespace BD.Models
{
    public partial class ProyectoAeropuertos
    {
        public int Id { get; set; }
        public int? IdProyecto { get; set; }
        public int? IdAeropuerto { get; set; }
        public bool? Estado { get; set; }

        public virtual Proyectos IdProyectoNavigation { get; set; }
    }
}
