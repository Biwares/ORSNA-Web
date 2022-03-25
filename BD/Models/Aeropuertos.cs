using System;
using System.Collections.Generic;

namespace BD.Models
{
    public partial class Aeropuertos
    {
        public Aeropuertos()
        {
            LibranzaAeropuertos = new HashSet<LibranzaAeropuertos>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdProvincia { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? Fechafin { get; set; }
        public int? IdAeropuertosGrupo { get; set; }
        public string NombreCorto { get; set; }
        public string CodIata { get; set; }
        public bool? Estado { get; set; }

        public virtual AeropuertosGrupo IdAeropuertosGrupoNavigation { get; set; }
        public virtual Provincias IdProvinciaNavigation { get; set; }
        public virtual ICollection<LibranzaAeropuertos> LibranzaAeropuertos { get; set; }
    }
}
