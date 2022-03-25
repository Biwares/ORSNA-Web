using System;
using System.Collections.Generic;

namespace BD.Models
{
    public partial class Proyectos
    {
        public Proyectos()
        {
            Libranzas = new HashSet<Libranzas>();
            ProyectoAdjuntos = new HashSet<ProyectoAdjuntos>();
            ProyectoAeropuertos = new HashSet<ProyectoAeropuertos>();
            ProyectoBeneficiarios = new HashSet<ProyectoBeneficiarios>();
            ProyectoProvincias = new HashSet<ProyectoProvincias>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? IdCuenta { get; set; }
        public string IdProyecto { get; set; }
        public int Codigo { get; set; }
        public int? IdArea { get; set; }
        public int DestinosId { get; set; }
        public string NroContratacion { get; set; }
        public string NroObra { get; set; }
        public string CodObra { get; set; }
        public string NroPago { get; set; }
        public string Objeto { get; set; }
        public decimal? MontoTotal { get; set; }
        public decimal? MontoPagadoAniosAnteriores { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string TipoEstado { get; set; }
        public bool? Estado { get; set; }
        public int? IdProyectosEstado { get; set; }

        public virtual Destinos Destinos { get; set; }
        public virtual Areas IdAreaNavigation { get; set; }
        public virtual Cuentas IdCuentaNavigation { get; set; }
        public virtual ProyectosEstado IdProyectosEstadoNavigation { get; set; }
        public virtual ICollection<Libranzas> Libranzas { get; set; }
        public virtual ICollection<ProyectoAdjuntos> ProyectoAdjuntos { get; set; }
        public virtual ICollection<ProyectoAeropuertos> ProyectoAeropuertos { get; set; }
        public virtual ICollection<ProyectoBeneficiarios> ProyectoBeneficiarios { get; set; }
        public virtual ICollection<ProyectoProvincias> ProyectoProvincias { get; set; }
        
        private bool? _pagosImpuestosIncluidos = false;
        public bool? PagosImpuestosIncluidos
        {
            get => _pagosImpuestosIncluidos ?? false;
            set => _pagosImpuestosIncluidos = value ?? false;
        }
    }
}
