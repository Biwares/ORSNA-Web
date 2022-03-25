using System;
using System.Collections.Generic;

namespace BD.Models
{
    public partial class LibranzaTipo
    {
        public LibranzaTipo()
        {
            Cuentas = new HashSet<Cuentas>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<Cuentas> Cuentas { get; set; }
    }
}
