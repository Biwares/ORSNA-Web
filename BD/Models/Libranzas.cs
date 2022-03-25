using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BD.Models
{
    public partial class Libranzas
    {
        public Libranzas()
        {
            LibranzaAeropuertos = new HashSet<LibranzaAeropuertos>();
            LibranzaBeneficiarios = new HashSet<LibranzaBeneficiarios>();
            LibranzaCesiones = new HashSet<LibranzaCesiones>();
            LibranzaDetalleWorkflow = new HashSet<LibranzaDetalleWorkflow>();
            LibranzaEmbargos = new HashSet<LibranzaEmbargos>();
            LibranzaFacturas = new HashSet<LibranzaFacturas>();
            LibranzaBeneficiariosCesiones = new HashSet<LibranzaBeneficiariosCesiones>();
        }

        public int Id { get; set; }
        public int? IdLibranzaTipo { get; set; }
        public string NroLibranza { get; set; }
        public int? IdProvincia { get; set; }
        public int? IdProyecto { get; set; }
        public int? IdLibranzaEstados { get; set; }
        public decimal? MontoFondoReparo { get; set; }
        public decimal? Multa { get; set; }
        public decimal? Mora { get; set; }
        public decimal? MontoRestante { get; set; }
        public DateTime Fecha { get; set; }
        public bool? Estado { get; set; }
        public bool? Sustituido { get; set; }
        public string NroEmbargo { get; set; }
        public string ResponsableEmbargo { get; set; }
        public decimal? MontoEmbargo { get; set; }
        public decimal? SaldoEmbargo { get; set; }
        public string Objeto { get; set; }
        public string Observaciones { get; set; }
        public bool RegistraCesion { get; set; }
        public string NroEscritura { get; set; }
        public string Beneficiario { get; set; }
        public string IdentificacionTributaria { get; set; }
        public string NormaLegal { get; set; }
        public DateTime? FechaPago { get; set; }
        public string ObjetoDatosGenerales { get; set; }

        /*Cuenta depositaria para libranzas con cesion*/
        public string CesionTipoCuenta { get; set; }
        public string CesionNroCuenta { get; set; }
        public string CesionNombreBanco { get; set; }
        public string CesionSucursal { get; set; }
        public string CesionCbu { get; set; }
        public string CesionCuit { get; set; }
        public string CesionTitular { get; set; }
        public DateTime? CesionFechaVigencia { get; set; }

        public decimal? Retencion { get; set; }
        public string RetencionObservaciones { get; set; }

        public virtual LibranzasEstado IdLibranzaEstadosNavigation { get; set; }
        public virtual Proyectos IdProyectoNavigation { get; set; }
        public virtual ICollection<LibranzaAeropuertos> LibranzaAeropuertos { get; set; }
        public virtual ICollection<LibranzaBeneficiarios> LibranzaBeneficiarios { get; set; }
        public virtual ICollection<LibranzaBeneficiariosCesiones> LibranzaBeneficiariosCesiones { get; set; }
        public virtual ICollection<LibranzaCesiones> LibranzaCesiones { get; set; }
        public virtual ICollection<LibranzaDetalleWorkflow> LibranzaDetalleWorkflow { get; set; }
        public virtual ICollection<LibranzaEmbargos> LibranzaEmbargos { get; set; }
        public virtual ICollection<LibranzaFacturas> LibranzaFacturas { get; set; }
        public virtual ICollection<LibranzaAdjuntos> LibranzaAdjuntos { get; set; }

        public int MonedaId { get; set; }
        public Moneda Moneda { get; set; }
        public decimal TasaDeCambio { get; set; }

    }
}
