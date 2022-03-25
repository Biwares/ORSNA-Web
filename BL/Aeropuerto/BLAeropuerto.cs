using BD.Models;
using BD.ViewModels;
using BL.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL.Aeropuerto

{
    public class BLAeropuerto : BLBase
    {
        private const string AUDITUBICACION = "Aeropuerto";

        public BLAeropuerto(string stringConnection,string userId) : base(stringConnection,userId) { }

        public GenericResponse<bool> Save(Aeropuertos newAero)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                var messageError = "";
                var aeroExiste = context.Aeropuertos.Where(x => x.CodIata == newAero.CodIata && x.Id != newAero.Id).Count();

                if (String.IsNullOrEmpty(newAero.CodIata))
                {
                    messageError += "el codigo Iata del aeropueto no puede ser null.";
                }

                if (aeroExiste > 0)
                    messageError += "Ya se encuentra registrado un Aeropuerto con el codigo: " + newAero.CodIata + ".";


                if (messageError.Length > 0)
                    return new GenericResponse<bool>() { Code = 501, Error = messageError };

                var eventType = Enums.AuditEventTypeEnum.ALTA;
                string jsonOld = string.Empty;
                string jsonNew = string.Empty;

                if ((newAero.Id) > 0)
                {
                    Aeropuertos aero = context.Aeropuertos.Where(x => x.Id == newAero.Id).FirstOrDefault();
                    jsonOld = Utils.getJsonFromObject(aero);
                    aero.Nombre = newAero.Nombre;
                    aero.IdProvincia = newAero.IdProvincia;
                    aero.FechaInicio = newAero.FechaInicio;
                    aero.Fechafin = newAero.Fechafin;
                    aero.IdAeropuertosGrupo = newAero.IdAeropuertosGrupo;
                    aero.NombreCorto = newAero.NombreCorto;
                    aero.CodIata = newAero.CodIata;
                    jsonNew = Utils.getJsonFromObject(aero);
                }
                else
                {
                    context.Aeropuertos.Add(newAero);
                    jsonNew = Utils.getJsonFromObject(newAero);
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
        public ICollection<VMAeropuerto> GetAll(int page, string FilterNombre, int? FilterGrupo, int? FilterProvincia, string Order, string ColumnOrder, HttpContext context)
        {
            try
            {
                ICollection<Aeropuertos> aeros = GetAeropuertosFilter(FilterNombre, FilterGrupo, FilterProvincia, Order, ColumnOrder);

                if (page > 1)
                    aeros = aeros.Skip(cantidadElementosPorPagina * (page - 1)).ToList();

                if (page > 0)
                    aeros = aeros.Take(cantidadElementosPorPagina).ToList();

                return VMAeropuerto.MapList(aeros,con);

            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public ICollection<VMAeropuerto> GetAll()
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                ICollection<VMAeropuerto> aeros = VMAeropuerto.MapList(context.Aeropuertos.Where(x => x.Estado == true).ToList(), con);
                return aeros;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public int GetCountPages(int page, string FilterNombre, int? FilterGrupo, int? FilterProvincia, string Order, string ColumnOrder)
        {
            var resp = GetAeropuertosFilter(FilterNombre, FilterGrupo, FilterProvincia, Order, ColumnOrder).Count();
            int rest = (resp % cantidadElementosPorPagina) > 0 ? 1 : 0;
            int pages = (resp / cantidadElementosPorPagina) + rest;
            return pages;
        }
        public ICollection<Aeropuertos> GetAeropuertosFilter(string FilterNombre, int? FilterGrupo, int? FilterProvincia, string Order, string ColumnOrder)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                var query = context.Aeropuertos.Where(x => x.Estado == true);
                if (!string.IsNullOrEmpty(FilterNombre))
                    query = query.Where(x => x.Nombre.Contains(FilterNombre));
                if (FilterGrupo > 0)
                    query = query.Where(x => x.IdAeropuertosGrupo == FilterGrupo);
                if (FilterProvincia > 0)
                    query = query.Where(x => x.IdProvincia == FilterProvincia);

                if (Order == "asc")
                    switch (ColumnOrder)
                    {
                        case "Nombre":
                            query = query.OrderBy(x => x.Nombre);
                            break;
                        case "Grupo":
                            query = query.OrderBy(x => x.IdAeropuertosGrupo);
                            break;
                        case "Provincia":
                            query = query.OrderBy(x => x.IdProvincia);
                            break;
                        default:
                            query = query.OrderBy(x => x.Id);
                            break;
                    }
                else
                    switch (ColumnOrder)
                    {
                        case "Nombre":
                            query = query.OrderByDescending(x => x.Nombre);
                            break;
                        case "Grupo":
                            query = query.OrderByDescending(x => x.IdAeropuertosGrupo);
                            break;
                        case "Provincia":
                            query = query.OrderByDescending(x => x.IdProvincia);
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
        public VMAeropuerto GetAeropuertoById(int IdAero)
        {
            try
            {
                OrsnaDatabaseContext db = new OrsnaDatabaseContext(con);
                return VMAeropuerto.Map(db.Aeropuertos.Find(IdAero), con);
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public GenericResponse<bool> Delete(int idAero)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);

                Aeropuertos aero = context.Aeropuertos.Find(idAero);
                var jsonOld = Utils.getJsonFromObject(aero);
                aero.Estado = false;
                context.SaveChanges();
                AuditHelper.logEvent(context, Enums.AuditEventTypeEnum.BAJA, AUDITUBICACION, AUDITDELETE, null, "", jsonOld, Utils.getJsonFromObject(aero),userId);

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
