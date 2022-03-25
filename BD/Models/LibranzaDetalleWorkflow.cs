using System;
using System.Collections.Generic;

namespace BD.Models
{
    public partial class LibranzaDetalleWorkflow
    {
        public int Id { get; set; }
        public int IdLibranza { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime? FechaPago { get; set; }
        public int IdUsuario { get; set; }
        public string Observaciones { get; set; }
        public int? IdEstadoAnterior { get; set; }
        public int? IdNuevoEstado { get; set; }

        public virtual LibranzasEstado IdEstadoAnteriorNavigation { get; set; }
        public virtual Libranzas IdLibranzaNavigation { get; set; }
        public virtual LibranzasEstado IdNuevoEstadoNavigation { get; set; }

        public int? MonedaActualId { get; set; }
        public Moneda MonedaActual { get; set; }
        public decimal TasaDeCambioActual { get; set; }
    }
}
