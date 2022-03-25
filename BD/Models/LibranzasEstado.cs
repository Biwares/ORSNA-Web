using System;
using System.Collections.Generic;

namespace BD.Models
{
    public partial class LibranzasEstado
    {
        public LibranzasEstado()
        {
            LibranzaDetalleWorkflowIdEstadoAnteriorNavigation = new HashSet<LibranzaDetalleWorkflow>();
            LibranzaDetalleWorkflowIdNuevoEstadoNavigation = new HashSet<LibranzaDetalleWorkflow>();
            Libranzas = new HashSet<Libranzas>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<LibranzaDetalleWorkflow> LibranzaDetalleWorkflowIdEstadoAnteriorNavigation { get; set; }
        public virtual ICollection<LibranzaDetalleWorkflow> LibranzaDetalleWorkflowIdNuevoEstadoNavigation { get; set; }
        public virtual ICollection<Libranzas> Libranzas { get; set; }
    }
}
