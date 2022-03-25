using System;
using System.Collections.Generic;

namespace BD.Models
{
    public partial class LibranzaCesiones
    {
        public int Id { get; set; }
        public int? IdLibranza { get; set; }
        public string Beneficiario { get; set; }
        public string Cuit { get; set; }
        public string RegistraCesion { get; set; }
        public string NroEscritura { get; set; }
        public bool? Estado { get; set; }

        public virtual Libranzas IdLibranzaNavigation { get; set; }
    }
}
