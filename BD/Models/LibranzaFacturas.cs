using System;
using System.Collections.Generic;

namespace BD.Models
{
    public partial class LibranzaFacturas
    {
        public int Id { get; set; }
        public int? IdLibranza { get; set; }
        public string Tipo { get; set; }
        public string Nro { get; set; }
        public DateTime Fecha { get; set; }
        public decimal? Monto { get; set; }
        public decimal? Iva { get; set; }
        public bool? Estado { get; set; }
        public decimal? Ibb { get; set; }

        public virtual Libranzas IdLibranzaNavigation { get; set; }
    }
}
