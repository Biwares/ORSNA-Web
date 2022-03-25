using BD.Models;
using BD.ViewModels;
using BL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BL.Proyecto

{
    public class BLProyecto : BLBase
    {
        private const string AUDITUBICACION = "Proyecto";

        public BLProyecto(string stringConnection, string userId) : base(stringConnection, userId) { }

        public GenericResponse<int> Save(VMProyecto newProyecto)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                var messageError = "";

                if (newProyecto.Area.IdArea == 0)
                    messageError += "Debe seleccionar un area.";

                if (newProyecto.Cuenta.Id == 0)
                    messageError += "Debe seleccionar una cuenta";

                var requiereAeropuertos = true;
                if (newProyecto.DestinosId == 0)
                    messageError += "Debe seleccionar un destino";
                else if ((requiereAeropuertos = context.Destinos.Find(newProyecto.DestinosId).RequiereAeropuerto) && newProyecto.Aeropuertos.Count() == 0)
                    messageError += "Debe seleccionar un aeropuerto";

                if (messageError.Length > 0)
                    return new GenericResponse<int>() { Code = 501, Error = messageError };

                int IdProyecto = newProyecto.Id;

                var eventType = Enums.AuditEventTypeEnum.ALTA;
                string jsonOld = string.Empty;
                string jsonNew = string.Empty;

                if ((newProyecto.Id) > 0)
                {
                    Proyectos proyecto = context.Proyectos.Where(x => x.Id == newProyecto.Id).FirstOrDefault();
                    proyecto.Nombre = newProyecto.Nombre;
                    proyecto.Descripcion = newProyecto.Descripcion;
                    proyecto.IdCuenta = newProyecto.Cuenta.Id;
                    proyecto.IdArea = newProyecto.Area.IdArea;
                    proyecto.NroContratacion = newProyecto.NroContratacion;
                    proyecto.NroObra = newProyecto.NroObra;
                    proyecto.CodObra = newProyecto.CodObra;
                    proyecto.NroPago = newProyecto.NroPago;
                    proyecto.Objeto = newProyecto.Objeto;
                    proyecto.MontoTotal = newProyecto.MontoTotal;
                    proyecto.IdProyectosEstado = newProyecto.IdEstadoProyecto;
                    proyecto.MontoPagadoAniosAnteriores = newProyecto.MontoPagadoAniosAnteriores;
                    proyecto.TipoEstado = newProyecto.TipoEstado;
                    proyecto.DestinosId = newProyecto.DestinosId;
                    proyecto.Estado = true;
                    proyecto.IdProyecto = proyecto.Codigo + " - " + newProyecto.Cuenta.Descripcion;
                    proyecto.PagosImpuestosIncluidos = newProyecto.PagosImpuestosIncluidos;
                    context.SaveChanges();
                    eventType = Enums.AuditEventTypeEnum.MODIFICACION;
                    jsonNew = Utils.getJsonFromObject(proyecto);
                }
                else
                {
                    Proyectos proyecto = VMProyecto.Map(newProyecto, con);
                    proyecto.Codigo = GetIdTentativo();
                    proyecto.FechaCreacion = DateTime.Today;
                    proyecto.IdProyecto = proyecto.Codigo.ToString() + " - " + newProyecto.Cuenta.Descripcion;
                    context.Proyectos.Add(proyecto);
                    context.SaveChanges();
                    IdProyecto = proyecto.Id;
                    jsonNew = Utils.getJsonFromObject(proyecto);
                }
                if (newProyecto.Provincias != null)
                    foreach (VMProvincia idp in newProyecto.Provincias)
                    {
                        ProyectoProvincias pp = new ProyectoProvincias();
                        pp.IdProyecto = IdProyecto;
                        pp.IdProvincia = idp.Id;

                        context.ProyectoProvincias.Add(pp);
                        context.SaveChanges();
                    }

                ICollection<ProyectoAeropuertos> proyAer = context.ProyectoAeropuertos.Where(x => x.IdProyecto == IdProyecto).ToList();
                if (proyAer.Count() > 0)
                {
                    foreach (ProyectoAeropuertos p in proyAer)
                    {
                        p.Estado = false;
                        context.SaveChanges();
                    }
                }
                if (newProyecto.Aeropuertos != null && requiereAeropuertos)
                    foreach (VMAeropuerto ida in newProyecto.Aeropuertos)
                    {
                        ProyectoAeropuertos pa = new ProyectoAeropuertos();
                        pa.IdProyecto = IdProyecto;
                        pa.IdAeropuerto = ida.Id;

                        context.ProyectoAeropuertos.Add(pa);
                        context.SaveChanges();
                    }
                if (newProyecto.Beneficiarios != null)
                    foreach (VMBeneficiario idb in newProyecto.Beneficiarios)
                    {
                        ProyectoBeneficiarios be = new ProyectoBeneficiarios();
                        be.IdProyecto = IdProyecto;
                        be.IdBeneficiario = idb.Id;

                        context.ProyectoBeneficiarios.Add(be);
                        context.SaveChanges();
                    }

                AuditHelper.logEvent(context, eventType, AUDITUBICACION, AUDITSAVE, null, "", jsonOld, jsonNew, userId);
                return new GenericResponse<int>() { Code = 200, Result = IdProyecto };
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
        public ICollection<VMProyecto> GetAll(int page, string FilterAeropuerto, string FilterIdProyecto, string FilterArea,
            int? FilterEstado, int FilterFechaCreacion, string FilterObra, List<int> areasDelUsuario, string Order, string ColumnOrder, string FilterCuentas, int FilterDestino)
        {
            try
            {
                ICollection<Proyectos> proyectos = GetProyectosFilter(FilterAeropuerto, FilterIdProyecto, FilterArea, FilterEstado,
                    FilterFechaCreacion, FilterObra, areasDelUsuario, Order, ColumnOrder, FilterCuentas, FilterDestino);

                if (page > 1)
                    proyectos = proyectos.Skip(cantidadElementosPorPagina * (page - 1)).ToList();

                if (page > 0)
                    proyectos = proyectos.Take(cantidadElementosPorPagina).ToList();

                return VMProyecto.MapList(proyectos, con);
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public ICollection<VMProyecto> GetAll()
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                ICollection<VMProyecto> proyectosC = VMProyecto.MapList(context.Proyectos.Where(x => x.Estado == true).ToList(), con);
                return proyectosC;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public ICollection<VMProyecto> GetAllReducido()
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                ICollection<VMProyecto> proyectosC = context.Proyectos
                    .Include(x => x.IdCuentaNavigation)
                    .Where(x => x.Estado == true)
                    .Select(x => new VMProyecto() {
                        Id = x.Id,
                        IdProyecto = x.IdProyecto,
                        Cuenta = new VMCuenta() { LibranzaTipo = new VMLibranzaTipo() { Id = x.IdCuentaNavigation.IdLibranzaTipoNavigation.Id, Nombre = x.IdCuentaNavigation.IdLibranzaTipoNavigation.Nombre} },
                        Nombre = x.Nombre,
                    }).ToList();
                return proyectosC;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public List<int> GetAnios()
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                var proyectosC = context.Proyectos.Where(x => x.Estado == true).Select(x => x.FechaCreacion.Year).Distinct().OrderByDescending(x => x).ToList();
                return proyectosC;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public List<VMProyecto> GetProyectosIds(List<int> areasDelUsuario)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                var proyectosC = context.Proyectos.Where(x => x.Estado == true && x.IdArea.HasValue && areasDelUsuario.Contains(x.IdArea.Value)).Select(x => new VMProyecto() { Id=x.Id, Nombre= x.IdProyecto }).ToList();
                return proyectosC;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public ICollection<VMDestinos> GetDestinos(int? idCuenta)
        {
            try
            {
                OrsnaDatabaseContext db = new OrsnaDatabaseContext(con);
                var query = db.Destinos.Where(x => x.Estado);
                if (idCuenta != null)
                {
                    int idGrupo = db.Cuentas.Find(idCuenta).IdAeropuertosGrupo.Value;
                    if (idGrupo != db.AeropuertosGrupo.Where(x => x.Nombre.Equals("SNA")).Single().Id)
                    {

                        query = query.Where(x => x.RequiereAeropuerto);
                    }
                }
                return VMDestinos.MapList(query.ToList(), con);
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public int GetCountPages(string FilterAeropuerto, string FilterIdProyecto, string FilterArea, int? FilterEstado, int FilterFechaCreacion, string FilterObra, List<int> areasDelUsuario, string Order, string ColumnOrder, string FilterCuentas, int FilterDestino)
        {
            ICollection<Proyectos> resp = GetProyectosFilter(FilterAeropuerto, FilterIdProyecto, FilterArea, FilterEstado,
                    FilterFechaCreacion, FilterObra, areasDelUsuario, Order, ColumnOrder, FilterCuentas, FilterDestino).ToList();
            int count = resp.Count();
            int rest = (count % cantidadElementosPorPagina) > 0 ? 1 : 0;
            int pages = (count / cantidadElementosPorPagina) + rest;
            return pages;
        }
        public int GetCountFilterElements(string FilterAeropuerto, string FilterIdProyecto, string FilterArea, int? FilterEstado, int FilterFechaCreacion, string FilterObra, List<int> areasDelUsuario, string Order, string ColumnOrder, string FilterCuentas, int FilterDestino)
        {
            int count = GetProyectosFilter(FilterAeropuerto, FilterIdProyecto, FilterArea, FilterEstado,
                    FilterFechaCreacion, FilterObra, areasDelUsuario, Order, ColumnOrder, FilterCuentas, FilterDestino).Count();
            return count;
        }
        public ICollection<Proyectos> GetProyectosFilter(string FilterAeropuerto, string FilterId, string FilterArea,
            int? FilterEstado, int FilterFechaCreacion, string FilterObra, List<int> areasDelUsuario, string Order, string ColumnOrder, string FilterCuentas, int FilterDestino)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                ICollection<Proyectos> query = context.Proyectos.Where(x => x.Estado == true && x.IdArea.HasValue && areasDelUsuario.Contains(x.IdArea.Value)).ToList();
                if (!string.IsNullOrEmpty(FilterAeropuerto))
                {
                    if (FilterAeropuerto.StartsWith(','))
                        FilterAeropuerto = FilterAeropuerto.Substring(1);
                    int[] filter = FilterAeropuerto.Split(',').Select(int.Parse).ToArray();

                    /*int?[] filternull = filter.Where(j => j > 0)
                                        .Select(x => (int?)Convert.ToInt32(x))
                                        .ToArray();*/

                    var w = context.ProyectoAeropuertos.Where(x => filter.Contains(x.IdAeropuerto.Value) && x.Estado.Value).Select(x => x.IdProyecto).ToList();

                    query = query.Where(x => w.Contains(x.Id)).ToList();
                }
                if (!string.IsNullOrEmpty(FilterId))
                {
                    if (FilterId.StartsWith(','))
                        FilterId = FilterId.Substring(1);
                    int[] filter = FilterId.Split(',').Select(int.Parse).ToArray();
                    query = query.Where(x => filter.Contains(x.Id)).ToList();
                }
                if (!string.IsNullOrEmpty(FilterObra))
                {
                    query = query.Where(x => x.CodObra.Contains(FilterObra)).ToList();
                }
                if (!string.IsNullOrEmpty(FilterArea))
                {
                    if (FilterArea.StartsWith(','))
                        FilterArea = FilterArea.Substring(1);
                    int[] filter = FilterArea.Split(',').Select(int.Parse).ToArray();

                    /*int?[] filternull = filter.Where(j => j > 0)
                                        .Select(x => (int?)Convert.ToInt32(x))
                                        .ToArray();
                    List<int> w = context.Proyectos.Where(x => filter.Contains(x.IdArea.Value)).Select(x => x.Id).ToList();*/
                    query = query.Where(q => filter.Contains(q.IdArea.Value)).ToList();

                }
                if (!string.IsNullOrEmpty(FilterCuentas))
                {
                    if (FilterCuentas.StartsWith(','))
                        FilterCuentas = FilterCuentas.Substring(1);
                    int[] filter = FilterCuentas.Split(',').Select(int.Parse).ToArray();
                    query = query.Where(q => filter.Contains(q.IdCuenta.Value)).ToList();
                }
                if (FilterEstado > 0)
                {
                    query = query.Where(x => x.IdProyectosEstado == FilterEstado).ToList();
                }
                if (FilterDestino > 0)
                {
                    query = query.Where(x => x.DestinosId == FilterDestino).ToList();
                }
                if (FilterFechaCreacion != 0)
                {
                    query = query.Where(x => x.FechaCreacion.Year == FilterFechaCreacion).ToList();
                }
                if (Order == "asc")
                    switch (ColumnOrder)
                    {
                        case "Aeropuerto":
                            query = query.OrderBy(x => x.ProyectoAeropuertos).ToList();
                            break;
                        case "CodObra":
                            query = query.OrderBy(x => x.NroObra).ToList();
                            break;
                        case "Area":
                            query = query.OrderBy(x => x.IdArea).ToList();
                            break;
                        case "Estado":
                            query = query.OrderBy(x => x.IdProyectosEstado).ToList();
                            break;
                        case "FechaCreacion":
                            query = query.OrderBy(x => x.FechaCreacion).ToList();
                            break;
                        case "IdProyecto":
                            query = query.OrderBy(x => x.IdProyecto).ToList();
                            break;
                        case "Cuenta":
                            query = query.OrderBy(x => x.IdCuenta).ToList();//TODO: verificar el orden por alfabeto
                            break;
                        case "MontoTotal":
                            query = query.OrderBy(x => x.MontoTotal).ToList();
                            break;
                        default:
                            query = query.OrderBy(x => x.Id).ToList();
                            break;
                    }
                else
                    switch (ColumnOrder)
                    {
                        case "Aeropuerto":
                            query = query.OrderByDescending(x => x.ProyectoAeropuertos).ToList();
                            break;
                        case "CodObra":
                            query = query.OrderByDescending(x => x.NroObra).ToList();
                            break;
                        case "Area":
                            query = query.OrderByDescending(x => x.IdArea).ToList();
                            break;
                        case "Estado":
                            query = query.OrderByDescending(x => x.IdProyectosEstado).ToList();
                            break;
                        case "FechaCreacion":
                            query = query.OrderByDescending(x => x.FechaCreacion).ToList();
                            break;
                        case "IdProyecto":
                            query = query.OrderByDescending(x => x.IdProyecto).ToList();
                            break;
                        case "Cuenta":
                            query = query.OrderByDescending(x => x.IdCuenta).ToList();//TODO: verificar el orden por alfabeto
                            break;
                        case "MontoTotal":
                            query = query.OrderByDescending(x => x.MontoTotal).ToList();
                            break;
                        default:
                            query = query.OrderByDescending(x => x.Id).ToList();
                            break;
                    }
                return query;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public VMProyecto GetProyectoById(int IdProyecto, bool puedeEditarMonto = false)
        {
            try
            {
                OrsnaDatabaseContext db = new OrsnaDatabaseContext(con);

                VMProyecto proy = VMProyecto.Map(db.Proyectos.Find(IdProyecto), con);
                return proy;

            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public int GetIdTentativo()
        {
            try
            {
                OrsnaDatabaseContext db = new OrsnaDatabaseContext(con);
                var ultimoProyecto = db.Proyectos.OrderByDescending(x => x.Codigo).FirstOrDefault();
                return (ultimoProyecto == null) ? 1 : ultimoProyecto.Codigo + 1;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return 0;
            }
        }
        public GenericResponse<bool> Delete(int idPro)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                string messageError = string.Empty;
                if (context.Libranzas.Any(w => w.IdProyecto == idPro && w.Estado.HasValue && w.Estado.Value))
                {
                    messageError += "El proyecto no se puede eliminar porque tiene libranzas asociadas.";
                    return new GenericResponse<bool>() { Code = 501, Error = messageError };
                }

                Proyectos proyecto = context.Proyectos.Find(idPro);
                var jsonOld = Utils.getJsonFromObject(proyecto);
                proyecto.Estado = false;
                context.SaveChanges();
                AuditHelper.logEvent(context, Enums.AuditEventTypeEnum.BAJA, AUDITUBICACION, AUDITDELETE, null, "", jsonOld, Utils.getJsonFromObject(proyecto), userId);
                return new GenericResponse<bool>() { Code = 200, Result = true };
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return new GenericResponse<bool>() { Code = 500, Error = ex.Message };
            }
        }
    }
}
