using System;
using System.Collections.Generic;

namespace BD.Models
{
    public partial class Adjuntos
    {
        public Adjuntos()
        {
            BeneficiarioAdjuntos = new HashSet<BeneficiarioAdjuntos>();
            ProyectoAdjuntos = new HashSet<ProyectoAdjuntos>();
            LibranzaAdjuntos = new HashSet<LibranzaAdjuntos>();
        }

        public int Id { get; set; }
        public string Modulo { get; set; }
        public DateTime FechaAlta { get; set; }
        public string NombreArchivo { get; set; }
        public bool? Estado { get; set; }
        public string TipoAnexo { get; set; }

        public virtual ICollection<BeneficiarioAdjuntos> BeneficiarioAdjuntos { get; set; }
        public virtual ICollection<ProyectoAdjuntos> ProyectoAdjuntos { get; set; }
        public virtual ICollection<LibranzaAdjuntos> LibranzaAdjuntos { get; set; }
    }
}
