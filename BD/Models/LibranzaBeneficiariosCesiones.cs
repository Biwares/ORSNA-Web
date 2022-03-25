using System;
using System.Collections.Generic;

namespace BD.Models
{
    public partial class LibranzaBeneficiariosCesiones
    {
        public int Id { get; set; }
        public int IdLibranzas { get; set; }
        public int IdBeneficiario { get; set; }
        public int IdBeneficiarioBancos { get; set; }
        public bool? Estado { get; set; }
        public string NroEscritura { get; set; }
        public decimal? Monto { get; set; }
        public virtual BeneficiarioBancos IdBeneficiarioBancosNavigation { get; set; }
        public virtual Beneficiarios IdBeneficiarioNavigation { get; set; }
        public virtual Libranzas IdLibranzasNavigation { get; set; }

    }
}
