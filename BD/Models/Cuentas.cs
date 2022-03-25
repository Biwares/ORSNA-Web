using System;
using System.Collections.Generic;

namespace BD.Models
{
    public partial class Cuentas
    {
        public Cuentas()
        {
            Proyectos = new HashSet<Proyectos>();
        }

        public int Id { get; set; }
        public string NroCuenta { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaVigencia { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? IdLibranzaTipo { get; set; }
        public int? IdAeropuertosGrupo { get; set; }
        public bool? Estado { get; set; }

        public virtual AeropuertosGrupo IdAeropuertosGrupoNavigation { get; set; }
        public virtual LibranzaTipo IdLibranzaTipoNavigation { get; set; }
        public virtual ICollection<Proyectos> Proyectos { get; set; }
    }
}
