using System;
using System.Collections.Generic;

namespace BD.Models
{
    public partial class LibranzaAeropuertos
    {
        public int Id { get; set; }
        public int? IdLibranza { get; set; }
        public int? IdAeropuerto { get; set; }
        public bool? Estado { get; set; }

        public virtual Aeropuertos IdAeropuertoNavigation { get; set; }
        public virtual Libranzas IdLibranzaNavigation { get; set; }
    }
}
