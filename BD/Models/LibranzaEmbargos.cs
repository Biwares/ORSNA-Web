using System;
using System.Collections.Generic;

namespace BD.Models
{
    public partial class LibranzaEmbargos
    {
        public int Id { get; set; }
        public int? IdLibranza { get; set; }
        public string NroEmbargo { get; set; }
        public string Responsable { get; set; }
        public decimal? Monto { get; set; }
        public decimal? Saldo { get; set; }
        public bool? Estado { get; set; }

        public virtual Libranzas IdLibranzaNavigation { get; set; }
    }
}
