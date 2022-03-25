using BD.Models;
using BD.ViewModels;
using BL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL.Area

{
    public class BLArea : BLBase
    {
        private const string AUDITUBICACION = "Area";


        public BLArea(string stringConnection, string userId) : base(stringConnection, userId) { }


        public GenericResponse<bool> Save(Areas newArea)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                var messageError = "";
                var areaExiste = context.Areas.Where(x => x.Codigo == newArea.Codigo && x.Id != newArea.Id && x.Estado.HasValue && x.Estado.Value).Count();

                if (String.IsNullOrEmpty(newArea.Codigo))
                {
                    messageError += "el código del área no puede ser null.";
                }

                if (areaExiste > 0)
                    messageError += "El área nro: " + newArea.Codigo + " existe.";


                if (messageError.Length > 0)
                    return new GenericResponse<bool>() { Code = 501, Error = messageError };

                var eventType = Enums.AuditEventTypeEnum.ALTA;
                string jsonOld = string.Empty;
                string jsonNew = string.Empty;

                if ((newArea.Id) > 0)
                {
                    Areas area = context.Areas.Where(x => x.Id == newArea.Id).FirstOrDefault();
                    jsonOld = Utils.getJsonFromObject(area);
                    area.Codigo = newArea.Codigo;
                    area.Nombre = newArea.Nombre;
                    eventType = Enums.AuditEventTypeEnum.MODIFICACION;
                    jsonNew = Utils.getJsonFromObject(area);
                }
                else
                {
                    context.Areas.Add(newArea);
                    jsonNew = Utils.getJsonFromObject(newArea);

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
        public IList<Areas> GetAll(int page, string FilterCodArea, string FilterNomArea, string Order, string ColumnOrder,bool filtrarAreas, List<int> areasDelUsuario)
        {
            try
            {
                ICollection<Areas> areas = GetAreasFilter(FilterCodArea, FilterNomArea, Order, ColumnOrder, filtrarAreas, areasDelUsuario);

                if (page > 1)
                    areas = areas.Skip(cantidadElementosPorPagina * (page - 1)).ToList();

                if (page >0)
                    areas = areas.Take(cantidadElementosPorPagina).ToList();

                return areas.ToList();
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public ICollection<VMArea> GetAll()
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                ICollection<VMArea> areas = VMArea.MapList(context.Areas.Where(x => x.Estado == true).ToList(), con);
                return areas;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public int GetCountFilterElements(int page, string FilterCodArea, string FilterNomArea, string Order, string ColumnOrder)
        {
            var count = GetAreasFilter(FilterCodArea, FilterNomArea, Order, ColumnOrder, false, null).Count();
            return count;
        }
        public int GetCountPages(int page, string FilterCodArea, string FilterNomArea, string Order, string ColumnOrder)
        {
            var resp = GetAreasFilter(FilterCodArea, FilterNomArea, Order, ColumnOrder, false, null).Count();
            int rest = (resp % cantidadElementosPorPagina) > 0 ? 1 : 0;
            int pages = (resp / cantidadElementosPorPagina) + rest;
            return pages;
        }
        public List<Areas> GetAreasFilter(string FilterCodArea, string FilterNomArea, string Order, string ColumnOrder, bool filtrarAreas, List<int> areasDelUsuario)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                var query = context.Areas.Where(x => x.Estado == true);
                if (filtrarAreas)
                    query = query.Where(x => areasDelUsuario.Contains(x.Id));
                if (!string.IsNullOrEmpty(FilterNomArea))
                    query = query.Where(x => x.Nombre.Contains(FilterNomArea));
                if (!string.IsNullOrEmpty(FilterCodArea))
                    query = query.Where(x => x.Codigo.Contains(FilterCodArea));

                if (Order == "asc")
                    switch (ColumnOrder)
                    {
                        case "NomArea":
                            query = query.OrderBy(x => x.Nombre);
                            break;
                        case "CodArea":
                            query = query.OrderBy(x => x.Codigo);
                            break;
                        default:
                            query = query.OrderBy(x => x.Id);
                            break;
                    }
                else
                    switch (ColumnOrder)
                    {
                        case "NomArea":
                            query = query.OrderByDescending(x => x.Nombre);
                            break;
                        case "CodArea":
                            query = query.OrderByDescending(x => x.Codigo);
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
        public Areas GetAreaById(int IdArea)
        {
            try
            {
                OrsnaDatabaseContext db = new OrsnaDatabaseContext(con);
                return db.Areas.Find(IdArea);
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public GenericResponse<bool> Delete(int idArea)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);

                string messageError = string.Empty;

                if (context.Proyectos.Any(w => w.IdArea == idArea && w.Estado.HasValue && w.Estado.Value))
                {
                    messageError += "El área no se puede eliminar porque tiene proyectos asociados.";
                    return new GenericResponse<bool>() { Code = 501, Error = messageError };
                }

                if (context.UsuariosAreas.Any(w => w.IdArea == idArea && w.Estado.HasValue && w.Estado.Value))
                {
                    messageError += "El área no se puede eliminar porque tiene usuarios asociados.";
                    return new GenericResponse<bool>() { Code = 501, Error = messageError };
                }

                Areas area = context.Areas.Find(idArea);
                var jsonOld = Utils.getJsonFromObject(area);
                area.Estado = false;
                context.SaveChanges();

                AuditHelper.logEvent(context, Enums.AuditEventTypeEnum.BAJA, AUDITUBICACION, AUDITDELETE, null, "", jsonOld, Utils.getJsonFromObject(area), userId);

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
