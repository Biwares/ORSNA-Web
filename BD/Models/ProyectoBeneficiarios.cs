using System;
using System.Collections.Generic;

namespace BD.Models
{
    public partial class ProyectoBeneficiarios
    {
        public int Id { get; set; }
        public int IdProyecto { get; set; }
        public int? IdBeneficiario { get; set; }
        public bool? Estado { get; set; }

        public virtual Beneficiarios IdBeneficiarioNavigation { get; set; }
        public virtual Proyectos IdProyectoNavigation { get; set; }
    }
}
