using BD.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BD.ViewModels
{
    public class VMProyecto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string IdProyecto { get; set; }
        public int? IdEstadoProyecto { get; set; }
        public string EstadoProyecto { get; set; }
        public VMCuenta Cuenta { get; set; }
        public VMArea Area { get; set; }
        public VMDestinos Destinos { get; set; }
        public int DestinosId { get; set; }
        public string NroContratacion { get; set; }
        public string NroObra { get; set; }
        public string CodObra { get; set; }
        public string NroPago { get; set; }
        public string Objeto { get; set; }
        public decimal? MontoTotal { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string TipoEstado { get; set; }
        public bool? Estado { get; set; }
        public int Codigo { get; set; }
        public decimal? MontoDisponible { get; set; }
        public ICollection<VMAdjunto> Adjuntos { get; set; }
        public List<VMAeropuerto> Aeropuertos { get; set; }
        public ICollection<VMBeneficiario> Beneficiarios { get; set; }
        public ICollection<VMProvincia> Provincias { get; set; }
        public decimal? MontoPagadoAniosAnteriores { get; set; }
        public bool? PagosImpuestosIncluidos { get; set; }

        public static VMProyecto Map(BD.Models.Proyectos p, string con)
        {
            OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
            VMProyecto response = new VMProyecto();

            response.Id = p.Id;
            response.Nombre = p.Nombre;
            response.Descripcion = p.Descripcion;
            response.IdProyecto = p.IdProyecto;
            response.IdEstadoProyecto = p.IdProyectosEstado;
            response.EstadoProyecto = context.ProyectosEstado.Find(p.IdProyectosEstado).Nombre;
            var cu = context.Cuentas.FirstOrDefault(x => x.Id == p.IdCuenta);
            response.Cuenta = VMCuenta.Map(cu, con);
            response.Area = VMArea.Map(context.Areas.FirstOrDefault(x => x.Id == p.IdArea), con);
            response.Destinos = VMDestinos.Map(context.Destinos.FirstOrDefault(x => x.Id == p.DestinosId), con);
            response.DestinosId = p.DestinosId;
            response.NroContratacion = p.NroContratacion;
            response.NroObra = p.NroObra;
            response.NroPago = p.NroPago;
            response.CodObra = p.CodObra;
            response.Objeto = p.Objeto;
            response.MontoTotal = p.MontoTotal;
            response.MontoPagadoAniosAnteriores = p.MontoPagadoAniosAnteriores;
            response.FechaCreacion = p.FechaCreacion;
            response.TipoEstado = p.TipoEstado;
            response.Estado = p.Estado;
            response.Codigo = p.Codigo;
            response.PagosImpuestosIncluidos = p.PagosImpuestosIncluidos;
            response.MontoDisponible = GetMontoDisponibleProyecto(p.Id, con, null);

            response.Adjuntos = VMAdjunto.MapList(context.Adjuntos.Where(
                a => a.ProyectoAdjuntos.Where(
                    ba => ba.IdProyecto == p.Id).Select(
                    ad => ad.IdProyecto).Contains(p.Id))
                    .ToList(), con);
            
            var IdProyectoAeropuertos = context.ProyectoAeropuertos.Where(x => x.IdProyecto == p.Id && x.Estado == true).Select(x=> x.IdAeropuerto);
            response.Aeropuertos = VMAeropuerto.MapList( context.Aeropuertos.Where(x => IdProyectoAeropuertos.Contains(x.Id)).ToList(), con);
            
            var IdProyectoBeneficiario = context.ProyectoBeneficiarios.Where(x => x.IdProyecto == p.Id).Select(x=> x.IdBeneficiario);
            response.Beneficiarios = VMBeneficiario.MapList( context.Beneficiarios.Where(x => IdProyectoBeneficiario.Contains(x.Id)).ToList(), con);
            
            var IdProyectoProvincias = context.ProyectoProvincias.Where(x => x.IdProyecto == p.Id).Select(x => x.IdProvincia);
            response.Provincias = VMProvincia.MapList( context.Provincias.Where(x => IdProyectoProvincias.Contains(x.Id)).ToList(), con);
            return response;
        }
        public static VMProyecto MapResumido(BD.Models.Proyectos p, string con)
        {
            OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
            VMProyecto response = new VMProyecto();

            response.Id = p.Id;
            response.Nombre = p.Nombre;
            response.Descripcion = p.Descripcion;
            response.IdProyecto = p.IdProyecto;
            response.IdEstadoProyecto = p.IdProyectosEstado;
            response.EstadoProyecto = context.ProyectosEstado.Find(p.IdProyectosEstado).Nombre;
            var cu = context.Cuentas.FirstOrDefault(x => x.Id == p.IdCuenta);
            response.Cuenta = VMCuenta.Map(cu, con);
            response.Area = VMArea.Map(context.Areas.FirstOrDefault(x => x.Id == p.IdArea), con);
            response.Destinos = VMDestinos.Map(context.Destinos.FirstOrDefault(x => x.Id == p.DestinosId), con);
            response.DestinosId = p.DestinosId;
            response.NroContratacion = p.NroContratacion;
            response.NroObra = p.NroObra;
            response.NroPago = p.NroPago;
            response.CodObra = p.CodObra;
            response.Objeto = p.Objeto;
            response.MontoTotal = p.MontoTotal;
            response.MontoPagadoAniosAnteriores = p.MontoPagadoAniosAnteriores;
            response.FechaCreacion = p.FechaCreacion;
            response.TipoEstado = p.TipoEstado;
            response.Estado = p.Estado;
            response.Codigo = p.Codigo;
            response.PagosImpuestosIncluidos = p.PagosImpuestosIncluidos;
            var IdProyectoAeropuertos = context.ProyectoAeropuertos.Where(x => x.IdProyecto == p.Id && x.Estado == true).Select(x => x.IdAeropuerto);
            response.Aeropuertos = VMAeropuerto.MapList(context.Aeropuertos.Where(x => IdProyectoAeropuertos.Contains(x.Id)).ToList(), con);
            response.MontoDisponible = GetMontoDisponibleProyecto(p.Id, con, null);
            return response;
        }


        public static decimal GetMontoDisponibleProyecto(int idProyec, string con,int? idLibranza)
        {
            OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);

            decimal deducciones = 0;
            decimal sumFactura = 0;
            decimal iva = 0;
            decimal ibb = 0;
            var idProyecto = idProyec;

            var pro = context.Proyectos.Find(idProyecto);
            var montoTotalProyecto = (pro.MontoTotal ?? 0) - (pro.MontoPagadoAniosAnteriores ?? 0);
            var mlib = context.Libranzas.Where(x => x.IdProyecto == idProyecto && x.Estado == true && x.IdLibranzaEstados != BD.Utilities.LibranzaEstadosEnum.Anulada);

            if (idLibranza != null)/*Removemos la que se pide, para hacer el calculo en front end :(*/
            {
                mlib = mlib.Where(x => x.Id != idLibranza);
            }

            if (mlib.Count() > 0) /*Retenciones: son los descuentos que hay en la libranzas (Fondo de Reparo, Multa, Mora,)*/
                foreach (var ml in mlib)
                {
                    deducciones += ((ml.MontoFondoReparo ?? 0) + (ml.Multa ?? 0) + (ml.Mora ?? 0)) * ml.TasaDeCambio;
                }
            int[] idsLibDelProyecto = mlib.Select(x => x.Id).ToArray();


            /*Necesitamos dividir en tipo de libranzas para sumar el Iva e Ibb a la resta**/
            var idLibranzasTipoA = context.LibranzaTipo.Where(w => w.Nombre.Equals("A")).Select(s => s.Id).FirstOrDefault();
            var idLibranzasTipoB = context.LibranzaTipo.Where(w => w.Nombre.Equals("B")).Select(s => s.Id).FirstOrDefault();
            var libranzasTipoA = mlib.Where(w => w.IdLibranzaTipo == idLibranzasTipoA);
            var libranzasTipoB = mlib.Where(w => w.IdLibranzaTipo == idLibranzasTipoB);

            int[] idsLibTipoADelProyecto = libranzasTipoA.Select(x => x.Id).ToArray();
            int[] idsLibTipoBDelProyecto = libranzasTipoB.Select(x => x.Id).ToArray();
            var libranzasTipoACreditoFacturas = context.LibranzaFacturas
                .Where(x => idsLibTipoADelProyecto.Contains(x.IdLibranza.Value) && x.Estado == true 
                && x.Tipo.ToUpper().Contains("NOTA DE CRÉDITO"));
            var libranzasTipoANOCreditoFacturas = context.LibranzaFacturas
                .Where(x => idsLibTipoADelProyecto.Contains(x.IdLibranza.Value) && x.Estado == true 
                && !x.Tipo.ToUpper().Contains("NOTA DE CRÉDITO")
                && !x.Tipo.ToUpper().Contains("INVOICE")
                && !x.Tipo.ToUpper().Contains("DEBIT NOTE")
                && !x.Tipo.ToUpper().Contains("CREDIT NOTE"));

            var libranzasTipoBCreditoFacturas = context.LibranzaFacturas
                .Where(x => idsLibTipoBDelProyecto.Contains(x.IdLibranza.Value) && x.Estado == true 
                && x.Tipo.ToUpper().Contains("NOTA DE CRÉDITO"));
            var libranzasTipoBNOCreditoFacturas = context.LibranzaFacturas
                .Where(x => idsLibTipoBDelProyecto.Contains(x.IdLibranza.Value) && x.Estado == true 
                && !x.Tipo.ToUpper().Contains("NOTA DE CRÉDITO")
                && !x.Tipo.ToUpper().Contains("INVOICE")
                && !x.Tipo.ToUpper().Contains("DEBIT NOTE")
                && !x.Tipo.ToUpper().Contains("CREDIT NOTE"));

            var libranzasInvoice = context.LibranzaFacturas.Where(x => idsLibDelProyecto.Contains(x.IdLibranza.Value) && x.Estado == true && x.Tipo.ToUpper().Contains("INVOICE")).Sum(x => x.Monto * x.IdLibranzaNavigation.TasaDeCambio) ?? 0;
            var libranzasDebit = context.LibranzaFacturas.Where(x => idsLibDelProyecto.Contains(x.IdLibranza.Value) && x.Estado == true && x.Tipo.ToUpper().Contains("DEBIT NOTE")).Sum(x => x.Monto * x.IdLibranzaNavigation.TasaDeCambio) ?? 0;
            var libranzasCredit = context.LibranzaFacturas.Where(x => idsLibDelProyecto.Contains(x.IdLibranza.Value) && x.Estado == true && x.Tipo.ToUpper().Contains("CREDIT NOTE")).Sum(x => x.Monto * x.IdLibranzaNavigation.TasaDeCambio) ?? 0;

            foreach (var fac in libranzasTipoANOCreditoFacturas) /*Para todas las facturas de libranzas Tipo A se suma el iva e ibb*/
            {
                sumFactura += fac.Monto ?? 0;
                iva += fac.Iva ?? 0;
                ibb += fac.Ibb ?? 0;
            }
            foreach (var fac in libranzasTipoACreditoFacturas) /*Para todas las facturas de libranzas Tipo A Credito se resta iva e ibb*/
            {
                sumFactura -= fac.Monto ?? 0;
                iva -= fac.Iva ?? 0;
                ibb -= fac.Ibb ?? 0;
            }

            foreach (var fac in libranzasTipoBNOCreditoFacturas) /*Para todas las facturas de libranzas Tipo B no se tiene en cuenta iva e ibb*/
                sumFactura += fac.Monto ?? 0;
            foreach (var fac in libranzasTipoBCreditoFacturas) /*Para todas las facturas de libranzas Tipo B no se tiene en cuenta iva e ibb*/
                sumFactura -= fac.Monto ?? 0;

            var sumaDocumentoDePago = sumFactura + libranzasInvoice + libranzasDebit - libranzasCredit;
            decimal neto = sumaDocumentoDePago - deducciones + iva + ibb;

            return montoTotalProyecto - neto;
        }

        public static Proyectos Map(VMProyecto p, string con)
        {
            OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
            Proyectos response = new Proyectos();

            response.Id = p.Id;
            response.Nombre = p.Nombre;
            response.Descripcion = p.Descripcion;
            response.IdProyecto = p.IdProyecto;
            response.IdProyectosEstado = p.IdEstadoProyecto;
            response.IdCuenta = p.Cuenta.Id;
            response.IdArea = p.Area.IdArea;
            response.NroContratacion = p.NroContratacion;
            response.NroObra = p.NroObra;
            response.CodObra = p.CodObra;
            response.NroPago = p.NroPago;
            response.Objeto = p.Objeto;
            response.MontoTotal = p.MontoTotal;
            response.FechaCreacion = p.FechaCreacion;
            response.TipoEstado = p.TipoEstado;
            response.Estado = p.Estado;
            response.DestinosId = p.DestinosId;
            response.MontoPagadoAniosAnteriores = p.MontoPagadoAniosAnteriores;
            response.PagosImpuestosIncluidos = p.PagosImpuestosIncluidos;
            return response;
        }

        public static ICollection<VMProyecto> MapList(ICollection<BD.Models.Proyectos> proyectos, string con)
        {
            ICollection<VMProyecto> listResponse = new System.Collections.ObjectModel.Collection<VMProyecto>();

            foreach (var proj in proyectos)
            {
                listResponse.Add(VMProyecto.Map(proj, con));
            }

            return listResponse;
        }

        public static ICollection<VMProyecto> MapListResumido(ICollection<BD.Models.Proyectos> proyectos, string con)
        {
            ICollection<VMProyecto> listResponse = new System.Collections.ObjectModel.Collection<VMProyecto>();
            try
            { 
            
            foreach (var proj in proyectos)
            {
                listResponse.Add(VMProyecto.MapResumido(proj, con));
            }
            }
            catch
            {

            }
            return listResponse;
        }
        public static ICollection<Proyectos> MapList(ICollection<VMProyecto> proyectos, string con)
        {
            ICollection<Proyectos> listResponse = new System.Collections.ObjectModel.Collection<Proyectos>();

            foreach (var proj in proyectos)
            {
                listResponse.Add(VMProyecto.Map(proj, con));
            }

            return listResponse;
        }
    }
}
