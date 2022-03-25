using BD.Models;
using BD.ViewModels;
using BL.Enums;
using BL.Helpers;
using ElmahCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL.Audit

{
    public class BLAudit : BLBase
    {

        public BLAudit(string stringConnection, string userId) : base(stringConnection, userId) { }


        public IList<Log> GetAll(int page, string FilterFechaDesde, string FilterFechaHasta, string FilterMensaje, string FilterDetalle,string FilterUserName, string Order, string ColumnOrder)
        {
            try
            {
                ICollection<Log> logs = GetLogsFilter(FilterFechaDesde, FilterFechaHasta, FilterMensaje, FilterDetalle, FilterUserName, Order, ColumnOrder);

                if (page > 1)
                    logs = logs.Skip(cantidadElementosPorPagina * (page - 1)).ToList();

                if (page > 0)
                    logs = logs.Take(cantidadElementosPorPagina).ToList();

                return logs.ToList();
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public ICollection<VMLog> GetAll()
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                ICollection<VMLog> areas = VMLog.MapList(context.Log.ToList(), con);
                return areas;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public int GetCountPages(int page, string FilterFechaDesde, string FilterFechaHasta, string FilterMensaje, string FilterDetalle, string FilterUserName, string Order, string ColumnOrder)
        {
            var resp = GetLogsFilter(FilterFechaDesde, FilterFechaHasta, FilterMensaje, FilterDetalle, FilterUserName, Order, ColumnOrder).Count();
            int rest = (resp % cantidadElementosPorPagina) > 0 ? 1 : 0;
            int pages = (resp / cantidadElementosPorPagina) + rest;
            return pages;
        }
        public int GetCountFilterElements(int page, string FilterFechaDesde, string FilterFechaHasta, string FilterMensaje, string FilterDetalle, string FilterUserName, string Order, string ColumnOrder)
        {
            var count = GetLogsFilter(FilterFechaDesde, FilterFechaHasta, FilterMensaje, FilterDetalle, FilterUserName, Order, ColumnOrder).Count();
            return count;
        }
        public List<Log> GetLogsFilter(string FilterFechaDesde, string FilterFechaHasta, string FilterMensaje, string FilterDetalle, string FilterUserName, string Order, string ColumnOrder)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                var query = context.Log.Where(x => true);
                if (!string.IsNullOrEmpty(FilterUserName))
                    query = query.Where(x => x.UserName.Contains(FilterUserName));

                if (!string.IsNullOrEmpty(FilterMensaje))
                    query = query.Where(x => x.Mensaje.Contains(FilterMensaje));
                if (!string.IsNullOrEmpty(FilterDetalle))
                    query = query.Where(x => x.Detalle.Contains(FilterDetalle));
                if (!string.IsNullOrEmpty(FilterFechaDesde))
                {
                    DateTime filterFechaDesde = DateTime.Parse(FilterFechaDesde);
                    query = query.Where(x => x.Fecha.CompareTo(filterFechaDesde) > 0);
                }
                if (!string.IsNullOrEmpty(FilterFechaHasta))
                {
                    DateTime filterFechaHasta = DateTime.Parse(FilterFechaHasta);
                    query = query.Where(x => x.Fecha.CompareTo(filterFechaHasta.AddDays(1)) < 0);
                }

                if (Order == "asc")
                    switch (ColumnOrder)
                    {
                        case "UserName":
                            query = query.OrderBy(x => x.UserName);
                            break;
                        case "Fecha":
                            query = query.OrderBy(x => x.Fecha);
                            break;
                        case "Ubicacion":
                            query = query.OrderBy(x => x.Ubicacion);
                            break;
                        case "Detalle":
                            query = query.OrderBy(x => x.Detalle);
                            break;
                        case "Mensaje":
                            query = query.OrderBy(x => x.Mensaje);
                            break;
                        default:
                            query = query.OrderBy(x => x.Id);
                            break;
                    }
                else
                    switch (ColumnOrder)
                    {
                        case "UserName":
                            query = query.OrderByDescending(x => x.UserName);
                            break;
                        case "Fecha":
                            query = query.OrderByDescending(x => x.Fecha);
                            break;
                        case "Ubicacion":
                            query = query.OrderByDescending(x => x.Ubicacion);
                            break;
                        case "Detalle":
                            query = query.OrderByDescending(x => x.Detalle);
                            break;
                        case "Mensaje":
                            query = query.OrderByDescending(x => x.Mensaje);
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
        public Log GetLogById(int IdLog)
        {
            try
            {
                OrsnaDatabaseContext db = new OrsnaDatabaseContext(con);
                return db.Log.Find(IdLog);
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }

        public static bool AuditEnabled() /*Podemos modificarlo de ser necesario hacer algo dinamico*/
        {
            return true;
        }

        public static void logEvent(OrsnaDatabaseContext db, AuditEventTypeEnum eventType, string controller,
            string method, string message, string details, string oldValue, string newValue,string userId)
        {
            if (!AuditEnabled())
                return;
            try
            {
                using (var context = db)
                {
                    var detailsFinal = string.Format("{0}-{1}-{2}", controller, method, details);
                    if (eventType == AuditEventTypeEnum.MODIFICACION || eventType == AuditEventTypeEnum.ALTA)
                        detailsFinal += Environment.NewLine + "Valor anterior: " + Environment.NewLine + oldValue + "Valor Nuevo: " + Environment.NewLine + newValue;
                    if (string.IsNullOrEmpty(message))
                        message = Utils.GetEnumDescription(eventType);

                    db.Log.Add(new Log() { UserName = userId, Fecha = DateTime.Now, Ubicacion = method, Mensaje = message, Detalle = detailsFinal });
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                // Loguear en ELMAH pero no cortar flujo
                ElmahExtensions.RiseError(ex);
            }
        }
    }
}
