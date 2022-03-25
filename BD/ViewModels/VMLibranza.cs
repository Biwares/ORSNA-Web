using System;
using System.Collections.Generic;
using System.Linq;
using BD.Models;
using Microsoft.EntityFrameworkCore;

namespace BD.ViewModels
{
    public class VMLibranza
    {
        public int Id { get; set; }
        public VMLibranzaTipo Libranzatipo { get; set; }
        public string NroLibranza { get; set; }
        public VMProvincia Provincia { get; set; }
        public VMProyecto Proyecto { get; set; }
        public VMLibranzasEstado LibranzasEstado { get; set; }
        public bool? Sustituido { get; set; }
        public string NroEmbargo { get; set; }
        public string ResponsableEmbargo { get; set; }
        public decimal? MontoEmbargo { get; set; }
        public string Objeto { get; set; }
        public string Observaciones { get; set; }
        public decimal? SaldoEmbargo { get; set; }
        public bool RegistraCesion { get; set; }
        public string NroEscritura { get; set; }
        public string BeneficiarioEmbargo { get; set; }
        public string IdentificacionTributaria { get; set; }
        public decimal? MontoFondoReparo { get; set; }
        public decimal? Multa { get; set; }
        public decimal? Mora { get; set; }
        public decimal? MontoRestante { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime? FechaPago { get; set; }
        public decimal MontoNeto { get; set; }
        public decimal MontoBruto { get; set; }
        public decimal Retenciones { get; set; }
        public ICollection<VMLibranzaDetalleWorkflow> DetallesWorkflow { get; set; }
        public ICollection<VMBeneficiario> Beneficiario { get; set; }
        public ICollection<VMBeneficiario> BeneficiarioCesiones { get; set; }

        public ICollection<VMFactura> Factura { get; set; }
        public string NormaLegal { get; set; }
        public ICollection<VMCuenta> Cuenta { get; set; }
        public string BeneficiarioCesion { get; set; }
        public string BeneficiarioCesion2 { get; set; }

        public  decimal?[] MontosCesiones { get; set; }
        public  string[] NroEscrituraCesiones { get; set; }

        public ICollection<VMBeneficiarioBancos> CuentaDepositaria { get; set; }
        public ICollection<VMAdjunto> Adjuntos { get; set; }
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

        public bool EstaPaga
        {
            get
            {
                return LibranzasEstado.Nombre.ToLower().Equals("pagada");
            }
        }

        public decimal TasaDeCambio { get; set; }
        public int MonedaId { get; set; }
        public string MonedaNombreCorto { get; set; }


        public static VMLibranza Map(BD.Models.Libranzas l, string con)
        {
            OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
            VMLibranza response = new VMLibranza();
            decimal iva = 0;
            decimal ibb = 0;
            response.Id = l.Id;
            if (l.IdLibranzaTipo != null)
                response.Libranzatipo = VMLibranzaTipo.Map(context.LibranzaTipo.Find(l.IdLibranzaTipo), con);
            response.NroLibranza = l.NroLibranza;
            if (l.IdProvincia != null)
                response.Provincia = VMProvincia.Map(context.Provincias.Find(l.IdProvincia), con);
            response.Sustituido = l.Sustituido;
            response.NroEmbargo = l.NroEmbargo;
            response.ResponsableEmbargo = l.ResponsableEmbargo;
            response.MontoEmbargo = l.MontoEmbargo;
            response.SaldoEmbargo = l.SaldoEmbargo;
            response.Mora = l.Mora;
            response.Multa = l.Multa;
            response.RegistraCesion = l.RegistraCesion;
            response.NroEscritura = l.NroEscritura;
            response.BeneficiarioEmbargo = l.Beneficiario;
            response.NormaLegal = l.NormaLegal;
            response.Objeto = l.Objeto;
            response.Observaciones = l.Observaciones;
            decimal sumFactura = 0;
            response.Retenciones = ((l.MontoFondoReparo ?? 0) + (l.Multa ?? 0) + (l.Mora ?? 0));
            var libranzas = context.LibranzaFacturas.Include(x=>x.IdLibranzaNavigation).Where(x => x.IdLibranza == l.Id && x.Estado == true);
            var libranzasCredito = libranzas.Where(x => x.Tipo.ToUpper().Contains("CRÉDITO") || x.Tipo.ToUpper().Contains("CREDIT NOTE"));
            var libranzasNoCredito = libranzas.Where(x => !x.Tipo.ToUpper().Contains("CRÉDITO") && !x.Tipo.ToUpper().Contains("CREDIT NOTE"));
            foreach (var fac in libranzasNoCredito)
            {
                sumFactura += fac.Monto ?? 0;
                
                iva += fac.Iva ?? 0;
                ibb += fac.Ibb ?? 0;
            }
            foreach (var fac in libranzasCredito)
            {
                sumFactura -= fac.Monto ?? 0;
                iva -= fac.Iva ?? 0;
                ibb -= fac.Ibb ?? 0;
            }

            //response.MontoNeto = sumFactura - response.Retenciones;
            response.MontoNeto = sumFactura - response.Retenciones + iva + ibb;

            if (response.Libranzatipo.Nombre.Equals("B")) // Si es Tipo B no se usa iva e ibb
                response.MontoNeto = sumFactura - response.Retenciones;
            
            response.MontoBruto = sumFactura;

            var wl = context.LibranzaDetalleWorkflow.Where(x => x.IdLibranza == l.Id);
            if (wl.Count() > 0)
                response.DetallesWorkflow = VMLibranzaDetalleWorkflow.MapList(wl.ToList(), con);

            response.IdentificacionTributaria = l.IdentificacionTributaria;
            if (l.IdProyecto != null)
                response.Proyecto = VMProyecto.Map(context.Proyectos.Find(l.IdProyecto), con);
            if (l.IdLibranzaEstados != null)
                response.LibranzasEstado = VMLibranzasEstado.Map(context.LibranzasEstado.Find(l.IdLibranzaEstados), con);
            response.MontoFondoReparo = l.MontoFondoReparo;
            response.Fecha = l.Fecha;
            response.FechaPago = l.FechaPago;
            response.Factura = VMFactura.MapList(context.LibranzaFacturas.Where(
                a => a.IdLibranza == l.Id && a.Estado == true).ToList(), con);
           
            /*******************************************************/
            var IdsBeneficiarios = context.LibranzaBeneficiarios.Where(x => x.IdLibranzas == l.Id && x.Estado == true).Select(x => x.IdBeneficiario);
            var IdsCuentas = context.LibranzaBeneficiarios.Where(x => x.IdLibranzas == l.Id && x.Estado == true).Select(x => x.IdBeneficiarioBancos);

            response.Beneficiario = VMBeneficiario.MapList(context.Beneficiarios.Where(x => IdsBeneficiarios.Contains(x.Id)).ToList(), con);
            foreach (var item in response.Beneficiario)
            {
                var cuenta = context.LibranzaBeneficiarios.Where(x => x.IdLibranzas == l.Id && x.Estado == true && x.IdBeneficiario == item.Id).Select(x => x.IdBeneficiarioBancos).SingleOrDefault();
                var cuentaSeleccionadaEnLaLibranza = context.BeneficiarioBancos.Find(cuenta);
                var cuentaSeleccionadaEnLaLibranzaLista = context.BeneficiarioBancos.Where(x => x.Id == cuenta).ToList();
                item.nombreCuentaSeleccionada = cuentaSeleccionadaEnLaLibranza.NroCuenta;
                item.Bancos = VMBeneficiarioBancos.MapList(cuentaSeleccionadaEnLaLibranzaLista, con);
            }

            response.BeneficiarioCesion = l.Beneficiario;




            response.CuentaDepositaria = VMBeneficiarioBancos.MapList(context.BeneficiarioBancos.Where(x => IdsCuentas.Contains(x.Id)).ToList(), con);


            /*******************************************************/

            var IdsBeneficiariosCesiones = context.LibranzaBeneficiariosCesiones.Where(x => x.IdLibranzas == l.Id && x.Estado == true).Select(x => x.IdBeneficiario);
            var IdsCuentasCesiones = context.LibranzaBeneficiariosCesiones.Where(x => x.IdLibranzas == l.Id && x.Estado == true).Select(x => x.IdBeneficiarioBancos);
            var MontosCesiones = context.LibranzaBeneficiariosCesiones.Where(x => x.IdLibranzas == l.Id && x.Estado == true).Select(x => x.Monto).ToArray(); 
            var NroEscrituraCesiones = context.LibranzaBeneficiariosCesiones.Where(x => x.IdLibranzas == l.Id && x.Estado == true).Select(x => x.NroEscritura).ToArray(); ;

            response.MontosCesiones = MontosCesiones;
            response.NroEscrituraCesiones = NroEscrituraCesiones;

            response.BeneficiarioCesiones = VMBeneficiario.MapList(context.Beneficiarios.Where(x => IdsBeneficiariosCesiones.Contains(x.Id)).ToList(), con);


            foreach (var item in response.BeneficiarioCesiones)
            {
                var cuenta = context.LibranzaBeneficiariosCesiones.Where(x => x.IdLibranzas == l.Id && x.Estado == true && x.IdBeneficiario == item.Id).Select(x => x.IdBeneficiarioBancos).SingleOrDefault();
                var cuentaSeleccionadaEnLaLibranza = context.BeneficiarioBancos.Find(cuenta);
                var cuentaSeleccionadaEnLaLibranzaLista = context.BeneficiarioBancos.Where(x => x.Id == cuenta).ToList();
                item.nombreCuentaSeleccionada = cuentaSeleccionadaEnLaLibranza.NroCuenta;
                item.Bancos = VMBeneficiarioBancos.MapList(cuentaSeleccionadaEnLaLibranzaLista, con);
            }

            response.BeneficiarioCesion = l.Beneficiario;


            response.Adjuntos = VMAdjunto.MapList(context.Adjuntos.Where(
                a => a.LibranzaAdjuntos.Where(
                    la => la.IdLibranza == l.Id).Select(
                    ad => ad.IdLibranza).Contains(l.Id))
                    .ToList(), con);

            /*Cuenta depositaria para libranzas con cesion*/
            response.CesionTipoCuenta = l.CesionTipoCuenta;
            response.CesionNroCuenta = l.CesionNroCuenta;
            response.CesionNombreBanco = l.CesionNombreBanco;
            response.CesionSucursal = l.CesionSucursal;
            response.CesionCbu = l.CesionCbu;
            response.CesionCuit = l.CesionCuit;
            response.CesionTitular = l.CesionTitular;
            response.CesionFechaVigencia = l.CesionFechaVigencia;

            response.ObjetoDatosGenerales = l.ObjetoDatosGenerales;

            response.Retencion = l.Retencion ?? 0;
            response.RetencionObservaciones = l.RetencionObservaciones;

            response.MonedaId = l.MonedaId;
            var moneda = context.Moneda.Find(l.MonedaId);
            response.MonedaNombreCorto = moneda.NombreCorto;
            response.TasaDeCambio = l.TasaDeCambio;
            return response;
        }

        public static ICollection<VMLibranza> MapList(ICollection<BD.Models.Libranzas> libranzas, string con)
        {
            ICollection<VMLibranza> listResponse = new System.Collections.ObjectModel.Collection<VMLibranza>();

            foreach (var li in libranzas)
            {
                listResponse.Add(VMLibranza.Map(li, con));
            }

            return listResponse;
        }

        public static Libranzas Map(VMLibranza l, string con)
        {
            OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
            Libranzas response = new Libranzas();

            response.Id = l.Id;
            if (l.Libranzatipo != null)
                response.IdLibranzaTipo = l.Libranzatipo.Id;

            response.NroLibranza = l.NroLibranza;
            response.IdProvincia = l.Provincia.Id;
            response.IdProyecto = l.Proyecto.Id;
            if (l.LibranzasEstado != null)
                response.IdLibranzaEstados = l.LibranzasEstado.Id;
            response.Sustituido = l.Sustituido;
            response.NroEmbargo = l.NroEmbargo;
            response.ResponsableEmbargo = l.ResponsableEmbargo;
            response.MontoEmbargo = l.MontoEmbargo;
            response.SaldoEmbargo = l.SaldoEmbargo;
            response.RegistraCesion = l.RegistraCesion;
            response.NroEscritura = l.NroEscritura;
            response.Beneficiario = l.BeneficiarioEmbargo;
            response.IdentificacionTributaria = l.IdentificacionTributaria;
            response.MontoFondoReparo = l.MontoFondoReparo;
            response.Multa = l.Multa;
            response.Mora = l.Mora;
            response.Fecha = l.Fecha;
            response.NormaLegal = l.NormaLegal;
            response.Objeto = l.Objeto;
            response.Observaciones = l.Observaciones;

            /*Cuenta depositaria para libranzas con cesion*/
            response.CesionTipoCuenta = l.CesionTipoCuenta;
            response.CesionNroCuenta = l.CesionNroCuenta;
            response.CesionNombreBanco = l.CesionNombreBanco;
            response.CesionSucursal = l.CesionSucursal;
            response.CesionCbu = l.CesionCbu;
            response.CesionCuit = l.CesionCuit;
            response.CesionTitular = l.CesionTitular;
            response.CesionFechaVigencia = l.CesionFechaVigencia;

            response.ObjetoDatosGenerales = l.ObjetoDatosGenerales;

            response.Retencion = l.Retencion;
            response.RetencionObservaciones = l.RetencionObservaciones;

            response.MonedaId = l.MonedaId;
            response.TasaDeCambio = l.TasaDeCambio;
            return response;
        }

        public static ICollection<Libranzas> MapList(ICollection<VMLibranza> libranzas, string con)
        {
            ICollection<Libranzas> listResponse = new System.Collections.ObjectModel.Collection<Libranzas>();

            foreach (var li in libranzas)
            {
                listResponse.Add(VMLibranza.Map(li, con));
            }

            return listResponse;
        }
    }
}
