using BD.Models;
using BD.ViewModels;
using BL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL.Libranza

{
    public class BLLibranza : BLBase
    {
        private const string AUDITUBICACION = "Libranza";

        public BLLibranza(string stringConnection, string userId) : base(stringConnection, userId) { }

        public decimal GetMontoDisponibleProyectoByIdLibranza(int idLibranza, int idProyec)
        {
            OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
            return VMProyecto.GetMontoDisponibleProyecto(idProyec,con, idLibranza);
        }

        public decimal GetMontoAPagarByIdLibranza(int idLibranza, decimal tasaDeCambio)
        {
            OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
            var l = context.Libranzas.Find(idLibranza);
            var f = context.LibranzaFacturas.Where(x => x.IdLibranza == idLibranza && x.Estado == true).ToList();
            var monto = f.Where(x => x.Monto.HasValue).Sum(x => x.Monto.Value * tasaDeCambio);
            var iva = f.Where(x => x.Iva.HasValue).Sum(x => x.Iva.Value);
            var Ibb = f.Where(x => x.Ibb.HasValue).Sum(x => x.Ibb.Value);
            var montoTotal = monto + iva + Ibb;
            var deducciones = (l.Mora + l.Multa + l.MontoFondoReparo) * tasaDeCambio ?? 0;
            var pagar = montoTotal - deducciones;
            return pagar;
        }
        public VMLibranzaDetalleWorkflow CambiarEstado(VMLibranzaDetalleWorkflow dw)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                var Libranza = context.Libranzas.Find(dw.IdLibranza);

                string jsonNew = string.Empty;
                string jsonOld = Utils.getJsonFromObject(Libranza);

                context.LibranzaDetalleWorkflow.Add(new LibranzaDetalleWorkflow
                {
                    IdLibranza = dw.IdLibranza,
                    Fecha = DateTime.Now,
                    IdUsuario = dw.IdUsuario,
                    Observaciones = dw.Observaciones,
                    IdEstadoAnterior = Libranza.IdLibranzaEstados,
                    IdNuevoEstado = dw.IdNuevoEstado,
                    FechaPago = dw.FechaPago,
                    MonedaActualId = dw.MonedaActualId,
                    TasaDeCambioActual = dw.TasaDeCambioActual,
                });
                //context.SaveChanges();
                Libranza.IdLibranzaEstados = dw.IdNuevoEstado;
                Libranza.FechaPago = dw.FechaPago;
                Libranza.Retencion = dw.Retencion;
                Libranza.RetencionObservaciones = dw.RetencionObservaciones;
                Libranza.MonedaId = dw.MonedaActualId.Value;
                Libranza.TasaDeCambio = dw.TasaDeCambioActual;
                context.SaveChanges();
                jsonNew = Utils.getJsonFromObject(Libranza);

                AuditHelper.logEvent(context, Enums.AuditEventTypeEnum.MODIFICACION, AUDITUBICACION + "-CambiarEstado", AUDITSAVE, null, "", jsonOld, jsonNew, userId);
                return dw;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }

        public DateTime? GetFechaEstado(int idLibranza)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                return context.LibranzaDetalleWorkflow.Where(x => x.IdLibranza == idLibranza).OrderByDescending(x => x.Fecha).Select(x => x.Fecha).First();
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }

        public DateTime? GetFechaPago(int idLibranza)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                var estadoPagado = context.LibranzasEstado.Where(x => x.Nombre.Equals("Pagada")).Select(x => x.Id).Single();
                var fechaPago = context.LibranzaDetalleWorkflow.Where(x => x.IdLibranza == idLibranza && x.IdNuevoEstadoNavigation.Nombre.Equals("Pagada")).OrderByDescending(x => x.Fecha).Select(x => x.FechaPago).FirstOrDefault();
                if (fechaPago == null)
                    fechaPago = context.Libranzas.Where(x => x.Id == idLibranza && x.IdLibranzaEstados == estadoPagado).OrderByDescending(x => x.FechaPago).Select(x => x.FechaPago).FirstOrDefault();
                return fechaPago;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }

        public ICollection<VMLibranzasEstado> GetSiguientesEstado(int IdLibranza)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                var libranza = context.Libranzas.Find(IdLibranza);
                var idLibranzaPagada = context.LibranzasEstado.Where(w => w.Nombre.Equals("Pagada")).Select(s => s.Id).First();
                if (idLibranzaPagada == libranza.IdLibranzaEstados)
                    return VMLibranzasEstado.MapList(context.LibranzasEstado.Where(x => x.Id == idLibranzaPagada).ToList(), con);

                return VMLibranzasEstado.MapList(context.LibranzasEstado.Where(x => x.Id != libranza.IdLibranzaEstados).ToList(), con); //Si no es pagada, devolvemos todo menos el estado actual
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public GenericResponse<int> Save(VMLibranza newLib, int IdUsuario, string pattern)
        {
            try
            {
                var hoy = DateTime.Now;
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                var messageError = "";
                int idTL = 0;
                if (newLib.Proyecto == null)
                {
                    messageError += "Debe seleccionar un proyecto.";
                }
                else
                {
                    int idC = context.Proyectos.Find(newLib.Proyecto.Id).IdCuenta.Value;
                    idTL = context.Cuentas.Find(idC).IdLibranzaTipo.Value;
                }

                if (String.IsNullOrEmpty(newLib.NroLibranza))
                {
                    messageError += "el Nro de libranza no puede ser null.";
                }

                if (messageError.Length > 0)
                    return new GenericResponse<int>() { Code = 501, Error = messageError };

                int idLib = newLib.Id;

                var eventType = Enums.AuditEventTypeEnum.ALTA;
                string jsonOld = string.Empty;
                string jsonNew = string.Empty;

                if ((newLib.Id) > 0)
                {
                    Libranzas lib = context.Libranzas.Where(x => x.Id == newLib.Id).FirstOrDefault();
                    jsonOld = Utils.getJsonFromObject(lib);
                    if (newLib.Proyecto != null && lib.IdLibranzaTipo != idTL)
                    {
                        lib.IdLibranzaTipo = idTL;
                        lib.NroLibranza = GetNroLibranza(lib.IdLibranzaTipo.Value, "D3");
                    }
                    lib.IdProvincia = newLib.Provincia.Id;
                    lib.IdProyecto = newLib.Proyecto.Id;
                    if (newLib.LibranzasEstado != null)
                        lib.IdLibranzaEstados = newLib.LibranzasEstado.Id;
                    lib.Sustituido = newLib.Sustituido;
                    lib.NroEmbargo = newLib.NroEmbargo;
                    lib.ResponsableEmbargo = newLib.ResponsableEmbargo;
                    lib.MontoEmbargo = newLib.MontoEmbargo;
                    lib.SaldoEmbargo = newLib.SaldoEmbargo;
                    lib.RegistraCesion = newLib.RegistraCesion;
                    lib.NroEscritura = newLib.NroEscritura;
                    lib.Beneficiario = newLib.BeneficiarioEmbargo;
                    lib.IdentificacionTributaria = newLib.IdentificacionTributaria;
                    lib.NormaLegal = newLib.NormaLegal;

                    lib.MontoFondoReparo = newLib.MontoFondoReparo;
                    lib.Multa = newLib.Multa;
                    lib.Objeto = newLib.Objeto;
                    lib.ObjetoDatosGenerales = newLib.ObjetoDatosGenerales;
                    lib.Observaciones = newLib.Observaciones;
                    lib.Mora = newLib.Mora;
                    lib.MontoRestante = newLib.MontoRestante;

                    if (newLib.RegistraCesion == true)
                    {
                        lib.CesionTipoCuenta = newLib.CesionTipoCuenta;
                        lib.CesionNroCuenta = newLib.CesionNroCuenta;
                        lib.CesionNombreBanco = newLib.CesionNombreBanco;
                        lib.CesionSucursal = newLib.CesionSucursal;
                        lib.CesionCbu = newLib.CesionCbu;
                        lib.CesionCuit = newLib.CesionCuit;
                        lib.CesionTitular = newLib.CesionTitular;
                        lib.CesionFechaVigencia = newLib.CesionFechaVigencia;
                        lib.NroEscritura = newLib.NroEscritura;
                        lib.Beneficiario = newLib.BeneficiarioEmbargo;
                        lib.IdentificacionTributaria = newLib.IdentificacionTributaria;
                    }
                    else
                    {
                        lib.CesionTipoCuenta = null;
                        lib.CesionNroCuenta = null;
                        lib.CesionNombreBanco = null;
                        lib.CesionSucursal = null;
                        lib.CesionCbu = null;
                        lib.CesionCuit = null;
                        lib.CesionTitular = null;
                        lib.CesionFechaVigencia = null;
                        lib.NroEscritura = null;
                        lib.Beneficiario = null;
                        lib.IdentificacionTributaria = null;
                    }
                    lib.Estado = true;
                    lib.MonedaId = newLib.MonedaId;
                    lib.TasaDeCambio = newLib.TasaDeCambio;
                    context.SaveChanges();
                    eventType = Enums.AuditEventTypeEnum.MODIFICACION;
                    jsonNew = Utils.getJsonFromObject(lib);
                }
                else
                {
                    Libranzas libranza = VMLibranza.Map(newLib, con);
                    libranza.Fecha = hoy;
                    if (newLib.RegistraCesion == true)
                    {
                        libranza.CesionTipoCuenta = newLib.CesionTipoCuenta;
                        libranza.CesionNroCuenta = newLib.CesionNroCuenta;
                        libranza.CesionNombreBanco = newLib.CesionNombreBanco;
                        libranza.CesionSucursal = newLib.CesionSucursal;
                        libranza.CesionCbu = newLib.CesionCbu;
                        libranza.CesionCuit = newLib.CesionCuit;
                        libranza.CesionTitular = newLib.CesionTitular;
                        libranza.CesionFechaVigencia = newLib.CesionFechaVigencia;
                    }
                    else
                    {
                        libranza.CesionTipoCuenta = null;
                        libranza.CesionNroCuenta = null;
                        libranza.CesionNombreBanco = null;
                        libranza.CesionSucursal = null;
                        libranza.CesionCbu = null;
                        libranza.CesionCuit = null;
                        libranza.CesionTitular = null;
                        libranza.CesionFechaVigencia = null;
                    }
                    libranza.Estado = true;
                    libranza.IdLibranzaEstados = context.LibranzasEstado.Where(x => x.Nombre.Equals("En elaboración")).Single().Id;
                    if (newLib.Proyecto != null)
                    {
                        libranza.IdLibranzaTipo = idTL;
                        libranza.NroLibranza = GetNroLibranza(libranza.IdLibranzaTipo.Value, "D3");
                    }
                    libranza.NormaLegal = newLib.NormaLegal;
                    libranza.Objeto = newLib.Objeto;
                    libranza.Observaciones = newLib.Observaciones;

                    context.Libranzas.Add(libranza);
                    context.SaveChanges();
                    idLib = libranza.Id;
                    context.LibranzaDetalleWorkflow.Add(new LibranzaDetalleWorkflow
                    {
                        IdLibranza = idLib,
                        Fecha = hoy,
                        IdUsuario = IdUsuario,
                        Observaciones = "Nueva Libranza",
                        IdEstadoAnterior = libranza.IdLibranzaEstados,
                        IdNuevoEstado = libranza.IdLibranzaEstados,
                        MonedaActualId = libranza.MonedaId,
                        TasaDeCambioActual = libranza.TasaDeCambio,
                    });
                    context.SaveChanges();
                    jsonNew = Utils.getJsonFromObject(libranza);

                }


                ICollection<LibranzaBeneficiarios> lb = context.LibranzaBeneficiarios.Where(x => x.IdLibranzas == idLib).ToList();
                if (lb.Count() > 0)
                {
                    foreach (LibranzaBeneficiarios l in lb)
                    {
                        l.Estado = false;
                        context.SaveChanges();
                    }
                }
                if (newLib.Beneficiario != null)
                    foreach (VMBeneficiario be in newLib.Beneficiario)
                    {
                        LibranzaBeneficiarios lbn = new LibranzaBeneficiarios();
                        lbn.IdBeneficiario = be.Id;
                        lbn.IdLibranzas = idLib;
                        lbn.IdBeneficiarioBancos = be.Bancos.FirstOrDefault().Id;
                        lbn.Estado = true;

                        context.LibranzaBeneficiarios.Add(lbn);
                        context.SaveChanges();
                    }

                ICollection<LibranzaBeneficiariosCesiones> lbc = context.LibranzaBeneficiariosCesiones.Where(x => x.IdLibranzas == idLib).ToList();
                if (lbc.Count() > 0)
                {
                    foreach (LibranzaBeneficiariosCesiones l in lbc)
                    {
                       
                        l.Estado = false;
                        context.SaveChanges();
                    }
                }
                if (newLib.BeneficiarioCesiones != null)
                {
                    var i = 0;
                    foreach (VMBeneficiario be in newLib.BeneficiarioCesiones)
                    {
                        LibranzaBeneficiariosCesiones lbn = new LibranzaBeneficiariosCesiones();
                        lbn.IdBeneficiario = be.Id;
                        lbn.IdLibranzas = idLib;
                        lbn.IdBeneficiarioBancos = be.Bancos.FirstOrDefault().Id;
                        lbn.Estado = true;
                        if (newLib.NroEscrituraCesiones != null)
                            lbn.NroEscritura = newLib.NroEscrituraCesiones[i];
                        if (newLib.MontosCesiones != null)
                            lbn.Monto = newLib.MontosCesiones[i];

                        context.LibranzaBeneficiariosCesiones.Add(lbn);
                        context.SaveChanges();
                        i++;
                    }
                }
                  



                ICollection<LibranzaFacturas> lfa = context.LibranzaFacturas.Where(x => x.IdLibranza == idLib).ToList();
                if (lfa.Count() > 0)
                {
                    foreach (LibranzaFacturas l in lfa)
                    {
                        l.Estado = false;
                    }
                }
                if (newLib.Factura != null)
                    foreach (VMFactura fa in newLib.Factura)
                    {
                        LibranzaFacturas lf = new LibranzaFacturas();
                        lf.IdLibranza = idLib;
                        lf.Tipo = fa.Tipo;
                        lf.Nro = fa.Nro;
                        lf.Fecha = fa.Fecha;
                        lf.Monto = fa.Monto;
                        lf.Iva = fa.Iva;
                        lf.Ibb = fa.Ibb;
                        lf.Estado = true;

                        context.LibranzaFacturas.Add(lf);
                    }
                context.SaveChanges();

                AuditHelper.logEvent(context, eventType, AUDITUBICACION, AUDITSAVE, null, "", jsonOld, jsonNew, userId);
                return new GenericResponse<int>() { Code = 200, Result = idLib };
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return new GenericResponse<int>()
                {
                    Code = 500,
                    Error = ex.Message
                };
            }
        }
        public ICollection<VMLibranza> GetAll(int page, string FilterAeropuerto, string FilterEstado, string FilterBeneficiario,
            int FilterProyecto, int FilterTipoLibranza, string FilterFactura, string FilterDesde,
            string FilterHasta, string FilterIdsProyecto, List<int> areasDelUsuario, string Order, string ColumnOrder, int takeNumber)
        {
            try
            {
                ICollection<Libranzas> libranzas = GetLibranzasFilter(FilterAeropuerto, FilterEstado, FilterBeneficiario,
                    FilterProyecto, FilterTipoLibranza, FilterFactura, FilterDesde,
                    FilterHasta, FilterIdsProyecto, areasDelUsuario, Order, ColumnOrder);

                if (page > 1)
                    libranzas = libranzas.Skip(cantidadElementosPorPagina * (page - 1)).ToList();

                if (page > 0)
                    libranzas = libranzas.Take(cantidadElementosPorPagina).ToList();

                return VMLibranza.MapList(libranzas, con);
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public ICollection<VMLibranza> GetAll()
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                ICollection<VMLibranza> Lib = VMLibranza.MapList(context.Libranzas.Where(x => x.Estado == true).ToList(), con);
                return Lib;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public int GetCountPages(string FilterAeropuerto, string FilterEstado, string FilterBeneficiario,
            int FilterProyecto, int FilterTipoLibranza, string FilterFactura, string FilterDesde,
            string FilterHasta, string FilterIdsProyecto, List<int> areasDelUsuario, string Order, string ColumnOrder)
        {
            int count = GetLibranzasFilter(FilterAeropuerto, FilterEstado, FilterBeneficiario,
            FilterProyecto, FilterTipoLibranza, FilterFactura, FilterDesde,
            FilterHasta, FilterIdsProyecto, areasDelUsuario, Order, ColumnOrder).Count();
            int rest = (count % cantidadElementosPorPagina) > 0 ? 1 : 0;
            int pages = (count / cantidadElementosPorPagina) + rest;
            return pages;
        }

        public int GetCountFilterElements(string FilterAeropuerto, string FilterEstado, string FilterBeneficiario,
            int FilterProyecto, int FilterTipoLibranza, string FilterFactura, string FilterDesde,
            string FilterHasta, string FilterIdsProyecto, List<int> areasDelUsuario, string Order, string ColumnOrder)
        {
            int count = GetLibranzasFilter(FilterAeropuerto, FilterEstado, FilterBeneficiario,
            FilterProyecto, FilterTipoLibranza, FilterFactura, FilterDesde,
            FilterHasta, FilterIdsProyecto, areasDelUsuario, Order, ColumnOrder).Count();
            return count;
        }

        public ICollection<Libranzas> GetLibranzasFilter(string FilterAeropuerto, string FilterEstado, string FilterBeneficiario,
            int FilterProyecto, int FilterTipoLibranza, string FilterFactura, string FilterDesde,
            string FilterHasta, string FilterIdsProyecto, List<int> areasDelUsuario, string Order, string ColumnOrder)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                var proyectosConAreasDelUsuario = context.Proyectos.Where(x => x.Estado == true && x.IdArea.HasValue && areasDelUsuario.Contains(x.IdArea.Value)).Select(x => x.Id).ToList();
                IQueryable<Libranzas> query = context.Libranzas.Where(x => x.Estado == true && x.IdProyecto.HasValue && proyectosConAreasDelUsuario.Contains(x.IdProyecto.Value));
                if (!string.IsNullOrEmpty(FilterAeropuerto))
                {
                    if (FilterAeropuerto.StartsWith(','))
                        FilterAeropuerto = FilterAeropuerto.Substring(1);

                    int[] filter = FilterAeropuerto.Split(',').Select(int.Parse).ToArray();

                    var p = context.ProyectoAeropuertos.Where(x => filter.Contains(x.IdAeropuerto.Value) && x.Estado == true).Select(x => x.IdProyecto).ToArray();

                    //var w = context.LibranzaAeropuertos.Where(x => p.Contains(x.Id.Value)).Select(x => x.IdLibranza).ToList();

                    query = query.Where(x => p.Contains(x.IdProyecto));
                }
                if (!string.IsNullOrEmpty(FilterEstado))
                {
                    query = query.Where(x => x.IdLibranzaEstados == int.Parse(FilterEstado));
                }
                if (!string.IsNullOrEmpty(FilterBeneficiario))
                {
                    if (FilterBeneficiario.StartsWith(','))
                        FilterBeneficiario = FilterBeneficiario.Substring(1);
                    int[] filter = FilterBeneficiario.Split(',').Select(int.Parse).ToArray();

                    var w = context.LibranzaBeneficiarios.Where(x => filter.Contains(x.IdBeneficiario) && x.Estado == true).Select(x => x.IdLibranzas).ToList();

                    query = query.Where(x => w.Contains(x.Id));
                }
                if (!string.IsNullOrEmpty(FilterIdsProyecto))
                {
                    if (FilterIdsProyecto.StartsWith(','))
                        FilterIdsProyecto = FilterIdsProyecto.Substring(1);
                    List<int> filter = FilterIdsProyecto.Split(',').Select(int.Parse).ToList();

                    query = query.Where(x => filter.Contains(x.IdProyecto.Value));
                }
                if (FilterProyecto > 0)
                {
                    query = query.Where(x => x.IdProyecto == FilterProyecto);
                }
                if (FilterTipoLibranza > 0)
                {
                    query = query.Where(x => x.IdLibranzaTipo == FilterTipoLibranza);
                }
                if (!string.IsNullOrEmpty(FilterFactura))
                {
                    var lf = context.LibranzaFacturas.Where(x => x.Nro.Contains(FilterFactura) && x.Estado == true).Select(x => x.IdLibranza).ToList();
                    query = query.Where(x => lf.Contains(x.Id));
                }
                if (!string.IsNullOrEmpty(FilterDesde))
                {
                    DateTime filter = DateTime.Parse(FilterDesde);
                    query = query.Where(x => x.Fecha >= filter);
                }
                if (!string.IsNullOrEmpty(FilterHasta))
                {
                    DateTime filter = DateTime.Parse(FilterHasta).AddDays(1);
                    query = query.Where(x => x.Fecha < filter);
                }
                if (Order == "asc")
                    switch (ColumnOrder)
                    {
                        case "NroLibranza":
                            query = query.OrderBy(x => x.NroLibranza);
                            break;
                        case "Fecha":
                            query = query.OrderBy(x => x.Fecha);
                            break;
                        case "Estado":
                            query = query.OrderBy(x => x.IdLibranzaEstados);
                            break;
                        case "Factura":
                            query = query.OrderBy(x => x.LibranzaFacturas);
                            break;
                        default:
                            query = query.OrderBy(x => x.Id);
                            break;
                    }
                else
                    switch (ColumnOrder)
                    {
                        case "NroLibranza":
                            query = query.OrderByDescending(x => x.NroLibranza);
                            break;
                        case "Fecha":
                            query = query.OrderByDescending(x => x.Fecha);
                            break;
                        case "Estado":
                            query = query.OrderByDescending(x => x.IdLibranzaEstados);
                            break;
                        case "Factura":
                            query = query.OrderByDescending(x => x.LibranzaFacturas);
                            break;
                        default:
                            query = query.OrderByDescending(x => x.Id);
                            break;
                    }
                return query.ToList();
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public VMLibranza GetLibranzaById(int Id)
        {
            try
            {
                OrsnaDatabaseContext db = new OrsnaDatabaseContext(con);

                VMLibranza lib = VMLibranza.Map(db.Libranzas.Find(Id), con);

                return lib;

            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public string GetNroLibranza(int tipoId, string pattern)
        {
            try
            {
                OrsnaDatabaseContext db = new OrsnaDatabaseContext(con);
                int max = 1;
                string anioActual = DateTime.Now.Year.ToString();
                var cont = db.Libranzas.Where(x => x.NroLibranza.Contains(anioActual) && x.IdLibranzaTipo == tipoId).OrderByDescending(x => x.NroLibranza).FirstOrDefault();
                if (cont != null)
                {
                    var ultimoNumero = cont.NroLibranza.Split('/');
                    max += Int32.Parse(ultimoNumero[0]);
                }

                return max.ToString(pattern) + "/" + DateTime.Now.Year.ToString();
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return "";
            }
        }
        public void ActualizarNroLibranza(string pattern)
        {
            try
            {
                OrsnaDatabaseContext db = new OrsnaDatabaseContext(con);

                foreach (var anio in db.Libranzas.Select(x => x.Fecha.Year).Distinct())
                {
                    foreach (var tipo in db.Libranzas.Select(x => x.IdLibranzaTipo).Distinct())
                    {
                        var countLibranza = 0;
                        var t1 = db.Libranzas.Where(w => w.IdLibranzaTipo == tipo && w.NroLibranza.Contains(anio.ToString())).OrderBy(o => o.Id);
                        foreach (var libranza in t1)
                        {
                            var findLibranzaById = db.Libranzas.Find(libranza.Id);
                            countLibranza++;
                            findLibranzaById.NroLibranza = getNroLibranza(countLibranza, anio, pattern);
                        }
                    }
                }

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
            }
        }
        public string getNroLibranza(int max, int anio, string pattern)
        {
            try
            {
                string nro = max.ToString(pattern) + "/" + anio.ToString();
                return nro;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return "";
            }
        }
        public GenericResponse<bool> Delete(int Id)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);

                Libranzas lib = context.Libranzas.Find(Id);
                var jsonOld = Utils.getJsonFromObject(lib);
                lib.Estado = false;
                context.SaveChanges();

                AuditHelper.logEvent(context, Enums.AuditEventTypeEnum.BAJA, AUDITUBICACION, AUDITDELETE, null, "", jsonOld, Utils.getJsonFromObject(lib), userId);
                return new GenericResponse<bool>() { Code = 200, Result = true };
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return new GenericResponse<bool>() { Code = 500, Error = ex.Message };
            }
        }
        public ICollection<LibranzaTipo> LibranzaTipos()
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                ICollection<LibranzaTipo> lt = context.LibranzaTipo.Where(x => x.Estado == true).ToList();
                return lt;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public byte[] GetPDF(int Id)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                var lst = VMLibranza.Map(context.Libranzas.Where(x => x.Id == Id).SingleOrDefault(), con);
                return PdfReport.Create(lst);
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }

        public ICollection<Moneda> GetAllMoneda()
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                ICollection<Moneda> aeros = context.Moneda.Where(x => x.Estado).ToList();
                return aeros;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
    }
}
