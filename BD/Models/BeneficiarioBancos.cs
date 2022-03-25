using System;
using System.Collections.Generic;

namespace BD.Models
{
    public partial class BeneficiarioBancos
    {
        public BeneficiarioBancos()
        {
            LibranzaBeneficiarios = new HashSet<LibranzaBeneficiarios>();

        }



        public int Id { get; set; }
        public int? IdBeneficiario { get; set; }
        public string TipoCuenta { get; set; }
        public string NroCuenta { get; set; }
        public string NombreBanco { get; set; }
        public string Sucursal { get; set; }
        public string Cbu { get; set; }
        public string Cuit { get; set; }
        public string Titular { get; set; }
        public DateTime FechaVigencia { get; set; }
        public bool? Estado { get; set; }
        public string Direccion { get; set; }
        public string BicSwift { get; set; }
        public double? EsNacional { get; set; }

        public virtual Beneficiarios IdBeneficiarioNavigation { get; set; }

        public virtual ICollection<LibranzaBeneficiarios> LibranzaBeneficiarios { get; set; }


    }
}
