using BD.Models;
using BD.ViewModels;
using BL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL.Area

{
    public class BLCuenta : BLBase
    {
        private const string AUDITUBICACION = "Cuenta";

        public BLCuenta(string stringConnection, string userId) : base(stringConnection, userId) { }


        public GenericResponse<bool> Save(Cuentas newCuenta)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                var messageError = "";
                var cuentaExiste = context.Cuentas.Where(x => x.NroCuenta == newCuenta.NroCuenta && x.Id != newCuenta.Id && x.Estado == true).Count();

                if (String.IsNullOrEmpty(newCuenta.NroCuenta))
                {
                    messageError += "Debe completar el núumero de cuenta.";
                }

                if (cuentaExiste > 0)
                    messageError += "Ya se encuentra registrado una cuenta fiduciaria con el número: " + newCuenta.NroCuenta + ".";


                if (messageError.Length > 0)
                    return new GenericResponse<bool>() { Code = 501, Error = messageError };

                var eventType = Enums.AuditEventTypeEnum.ALTA;
                string jsonOld = string.Empty;
                string jsonNew = string.Empty;

                if ((newCuenta.Id) > 0)
                {
                    Cuentas cuenta = context.Cuentas.Where(x => x.Id == newCuenta.Id).FirstOrDefault();
                    cuenta.Descripcion = newCuenta.Descripcion;
                    cuenta.FechaVigencia = newCuenta.FechaVigencia;
                    cuenta.Nombre = newCuenta.Nombre;
                    cuenta.NroCuenta = newCuenta.NroCuenta;
                    cuenta.IdAeropuertosGrupo = newCuenta.IdAeropuertosGrupo;
                    cuenta.IdLibranzaTipo = newCuenta.IdLibranzaTipo;
                    eventType = Enums.AuditEventTypeEnum.MODIFICACION;
                    jsonNew = Utils.getJsonFromObject(cuenta);
                }
                else
                {
                    context.Cuentas.Add(newCuenta);
                    jsonNew = Utils.getJsonFromObject(newCuenta);
                }

                context.SaveChanges();
                AuditHelper.logEvent(context, eventType, AUDITUBICACION, AUDITSAVE, null, "", jsonOld, jsonNew, userId);

                return new GenericResponse<bool>() { Code = 200, Result = true };
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return new GenericResponse<bool>() { Code = 500, Error = ex.Message };
            }
        }
        public ICollection<VMCuenta> GetAll(int page, string FilterNroCuenta, string FilterNombre, int FilterTipoLibranza, int FilterGrupoAeropuerto, string Order, string ColumnOrder)
        {
            try
            {
                ICollection<Cuentas> cuentas = GetCuentasFilter(FilterNroCuenta, FilterNombre, FilterTipoLibranza, FilterGrupoAeropuerto, Order, ColumnOrder);

                if (page > 1)
                    cuentas = cuentas.Skip(cantidadElementosPorPagina * (page - 1)).ToList();

                if (page > 0)
                    cuentas = cuentas.Take(cantidadElementosPorPagina).ToList();

                return VMCuenta.MapList(cuentas, con);

            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public ICollection<VMCuenta> GetAll()
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                ICollection<VMCuenta> cuenta = VMCuenta.MapList(context.Cuentas.Where(x => x.Estado == true).ToList(), con);
                return cuenta;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }

        public int GetCountFilterElements(int page, string FilterNroCuenta, string FilterNombre, int FilterTipoLibranza, int FilterGrupoAeropuerto, string Order, string ColumnOrder)
        {
            var count = GetCuentasFilter(FilterNroCuenta, FilterNombre, FilterTipoLibranza, FilterGrupoAeropuerto, Order, ColumnOrder).Count();
            return count;
        }
        public int GetCountPages(int page, string FilterNroCuenta, string FilterNombre, int FilterTipoLibranza, int FilterGrupoAeropuerto, string Order, string ColumnOrder)
        {
            var resp = GetCuentasFilter(FilterNroCuenta, FilterNombre, FilterTipoLibranza, FilterGrupoAeropuerto, Order, ColumnOrder).Count();
            int rest = (resp % cantidadElementosPorPagina) > 0 ? 1 : 0;
            int pages = (resp / cantidadElementosPorPagina) + rest;
            return pages;
        }
        public ICollection<Cuentas> GetCuentasFilter(string FilterNroCuenta, string FilterNombre, int FilterTipoLibranza, int FilterGrupoAeropuerto, string Order, string ColumnOrder)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                var query = context.Cuentas.Where(x => x.Estado == true);
                if (!string.IsNullOrEmpty(FilterNroCuenta))
                    query = query.Where(x => x.NroCuenta.Contains(FilterNroCuenta));
                if (!string.IsNullOrEmpty(FilterNombre))
                    query = query.Where(x => x.Nombre.Contains(FilterNombre));
                if (FilterTipoLibranza > 0)
                    query = query.Where(x => x.IdLibranzaTipo == FilterTipoLibranza);
                if (FilterGrupoAeropuerto > 0)
                    query = query.Where(x => x.IdAeropuertosGrupo == FilterGrupoAeropuerto);

                if (Order == "asc")
                    switch (ColumnOrder)
                    {
                        case "NroCuenta":
                            query = query.OrderBy(x => x.NroCuenta);
                            break;
                        case "Nombre":
                            query = query.OrderBy(x => x.Nombre);
                            break;
                        case "TipoLibranza":
                            query = query.OrderBy(x => x.IdLibranzaTipo);
                            break;
                        case "GrupoAeropuerto":
                            query = query.OrderBy(x => x.IdAeropuertosGrupo);
                            break;
                        default:
                            query = query.OrderBy(x => x.FechaCreacion);
                            break;
                    }
                else
                    switch (ColumnOrder)
                    {
                        case "NroCuenta":
                            query = query.OrderByDescending(x => x.NroCuenta);
                            break;
                        case "Nombre":
                            query = query.OrderByDescending(x => x.Nombre);
                            break;
                        case "TipoLibranza":
                            query = query.OrderByDescending(x => x.IdLibranzaTipo);
                            break;
                        case "GrupoAeropuerto":
                            query = query.OrderByDescending(x => x.IdAeropuertosGrupo);
                            break;
                        default:
                            query = query.OrderByDescending(x => x.FechaCreacion);
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
        public Cuentas GetCuentaById(int IdCuenta)
        {
            try
            {
                OrsnaDatabaseContext db = new OrsnaDatabaseContext(con);
                return db.Cuentas.Find(IdCuenta);
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public ICollection<VMAeropuerto> GetAerosToCuenta(int idCuenta)
        {
            try
            {
                OrsnaDatabaseContext db = new OrsnaDatabaseContext(con);
                int idGrupo = db.Cuentas.Find(idCuenta).IdAeropuertosGrupo.Value;
                if (idGrupo == db.AeropuertosGrupo.Where(x=>x.Nombre.Equals("SNA")).Single().Id)
                {
                    return VMAeropuerto.MapList(db.Aeropuertos.ToList(), con);
                }
                else
                {
                    return VMAeropuerto.MapList(db.Aeropuertos.Where(x => x.IdAeropuertosGrupo == idGrupo).ToList(), con);
                }
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public GenericResponse<bool> Delete(int idCuenta)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                string messageError = string.Empty;

                if (context.Proyectos.Any(w => w.IdCuenta == idCuenta && w.Estado.HasValue && w.Estado.Value))
                {
                    messageError += "La cuenta no se puede eliminar porque tiene proyectos asociados.";
                    return new GenericResponse<bool>() { Code = 501, Error = messageError };
                }

                Cuentas cuenta = context.Cuentas.Find(idCuenta);
                cuenta.Estado = false;
                var jsonOld = Utils.getJsonFromObject(cuenta);
                context.SaveChanges();
                AuditHelper.logEvent(context, Enums.AuditEventTypeEnum.BAJA, AUDITUBICACION, AUDITDELETE, null, "", jsonOld, Utils.getJsonFromObject(cuenta), userId);

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
