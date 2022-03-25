using System;
using System.Collections.Generic;

namespace BD.Models
{
    public partial class Beneficiarios
    {
        public Beneficiarios()
        {
            BeneficiarioAdjuntos = new HashSet<BeneficiarioAdjuntos>();
            BeneficiarioBancos = new HashSet<BeneficiarioBancos>();
            LibranzaBeneficiarios = new HashSet<LibranzaBeneficiarios>();
            ProyectoBeneficiarios = new HashSet<ProyectoBeneficiarios>();
        }

        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public string Descripcion { get; set; }
        public string Cuit { get; set; }
        public DateTime FechaAlta { get; set; }
        public string NacionalExtranjero { get; set; }
        public string Email { get; set; }
        public bool? Estado { get; set; }
        public string Telefono { get; set; }

        public virtual ICollection<BeneficiarioAdjuntos> BeneficiarioAdjuntos { get; set; }
        public virtual ICollection<BeneficiarioBancos> BeneficiarioBancos { get; set; }
        public virtual ICollection<LibranzaBeneficiarios> LibranzaBeneficiarios { get; set; }

        public virtual ICollection<LibranzaBeneficiariosCesiones> LibranzaBeneficiariosCesiones { get; set; }
        public virtual ICollection<ProyectoBeneficiarios> ProyectoBeneficiarios { get; set; }
    }
}
