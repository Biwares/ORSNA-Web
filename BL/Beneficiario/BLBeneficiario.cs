using BD.Models;
using BD.ViewModels;
using BL.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL.Beneficiario
{
    public class BLBeneficiario : BLBase
    {
        private const string AUDITUBICACION = "Beneficiario";

        public BLBeneficiario(string stringConnection, string userId) : base(stringConnection, userId) { }

        public GenericResponse<int> Save(VMBeneficiario newBen)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                var messageError = "";
                var Benexiste = context.Beneficiarios.Where(x => x.Cuit == newBen.Cuit && x.Id != newBen.Id && x.Estado == true).Count();

                if (String.IsNullOrEmpty(newBen.Cuit))
                {
                    messageError += "el cuit no puede ser null.";
                }

                if (Benexiste > 0)
                    messageError += "Ya se encuentra registrado un cuit: " + newBen.Cuit + ".";


                if (messageError.Length > 0)
                    return new GenericResponse<int>() { Code = 501, Error = messageError };

                int Idbeneficiario = newBen.Id;

                var eventType = Enums.AuditEventTypeEnum.ALTA;
                string jsonOld = string.Empty;
                string jsonNew = string.Empty;

                if ((newBen.Id) > 0)
                {
                    Beneficiarios beneficiario = context.Beneficiarios.Where(x => x.Id == newBen.Id).FirstOrDefault();
                    jsonOld = Utils.getJsonFromObject(beneficiario);
                    beneficiario.RazonSocial = newBen.RazonSocial;
                    beneficiario.Descripcion = newBen.Descripcion;
                    beneficiario.Cuit = newBen.Cuit;
                    if (newBen.FechaAlta == null)
                        beneficiario.FechaAlta = DateTime.Today;
                    else
                        beneficiario.FechaAlta = newBen.FechaAlta.Value;
                    beneficiario.NacionalExtranjero = newBen.NacionalExtranjero.Trim();
                    beneficiario.Email = newBen.Email;
                    beneficiario.Telefono = newBen.Telefono;
                    beneficiario.Estado = true;
                    context.SaveChanges();
                    eventType = Enums.AuditEventTypeEnum.MODIFICACION;
                    jsonNew = Utils.getJsonFromObject(beneficiario);
                }
                else
                {
                    if (newBen.FechaAlta == null)
                        newBen.FechaAlta = DateTime.Today;
                    Beneficiarios beneficiario = VMBeneficiario.Map(newBen, con);
                    context.Beneficiarios.Add(beneficiario);
                    context.SaveChanges();
                    jsonNew = Utils.getJsonFromObject(beneficiario);
                    Idbeneficiario = beneficiario.Id;
                }

                ICollection<BeneficiarioBancos> benban = context.BeneficiarioBancos.Where(x => x.IdBeneficiario == Idbeneficiario).ToList();
                if (benban.Count() > 0)
                {
                    foreach (BeneficiarioBancos b in benban)
                    {
                        b.Estado = false;
                    }
                }
                if (newBen.Bancos != null)
                    foreach (VMBeneficiarioBancos bb in newBen.Bancos)
                    {
                        BeneficiarioBancos banco = new BeneficiarioBancos();
                        banco.EsNacional = bb.EsNacional;
                        if (banco.EsNacional == 0)
                        {
                            banco.Direccion = bb.Direccion;
                            banco.BicSwift = bb.BicSwift;
                            banco.Cuit = bb.Cuit;
                        }
                        banco.Sucursal = bb.Sucursal;
                        banco.Cbu = bb.Cbu;
                        banco.Titular = bb.Titular;
                        banco.TipoCuenta = bb.TipoCuenta;
                        banco.IdBeneficiario = Idbeneficiario;
                        banco.NombreBanco = bb.NombreBanco;
                        banco.NroCuenta = bb.NroCuenta;
                        if (bb.FechaVigencia == null)
                            bb.FechaVigencia = DateTime.Today;
                        banco.FechaVigencia = bb.FechaVigencia.Value;

                        context.BeneficiarioBancos.Add(banco);
                    }
                context.SaveChanges();

                AuditHelper.logEvent(context, eventType, AUDITUBICACION, AUDITSAVE, null, "", jsonOld, jsonNew, userId);

                return new GenericResponse<int>() { Code = 200, Result = Idbeneficiario };
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return new GenericResponse<int>() { Code = 500, Error = ex.Message };
            }
        }
        public IList<Beneficiarios> GetAll(int page, string FilterRazonSocial, string FilterCuit, string FilterNacionalExtranjero, string Order, string ColumnOrder)
        {
            try
            {
                ICollection<Beneficiarios> beneficiarios = GetProveedorFilter(FilterRazonSocial, FilterCuit, FilterNacionalExtranjero, Order, ColumnOrder);

                if (page > 1)
                    beneficiarios = beneficiarios.Skip(cantidadElementosPorPagina * (page - 1)).ToList();

                if (page > 0)
                    beneficiarios = beneficiarios.Take(cantidadElementosPorPagina).ToList();

                return beneficiarios.ToList();
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public ICollection<VMBeneficiario> GetAll(string FilterRazonSocial, string FilterCuit)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                if (string.IsNullOrEmpty(FilterRazonSocial))
                    FilterRazonSocial = "";
                if (string.IsNullOrEmpty(FilterCuit))
                    FilterCuit = "";
                var query = VMBeneficiario.MapList(
                    context.Beneficiarios.Where(x=> x.Cuit.Contains (FilterCuit) && x.RazonSocial.Contains(FilterRazonSocial) && x.Estado==true).ToList()
                    ,con);
                return query;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public ICollection<VMBeneficiario> GetAll()
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                ICollection<VMBeneficiario> b = VMBeneficiario.MapList(context.Beneficiarios.Where(x => x.Estado == true).ToList(), con);
                return b;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public int GetCountPages(int page, string FilterRazonSocial, string FilterCuit, string FilterNacionalExtranjero, string Order, string ColumnOrder)
        {
            var resp = GetProveedorFilter(FilterRazonSocial, FilterCuit, FilterNacionalExtranjero, Order, ColumnOrder).Count();
            int rest = (resp % cantidadElementosPorPagina) > 0 ? 1 : 0;
            int pages = (resp / cantidadElementosPorPagina) + rest;
            return pages;
        }
        public int GetCountFilterElements(int page, string FilterRazonSocial, string FilterCuit, string FilterNacionalExtranjero, string Order, string ColumnOrder)
        {
            var count = GetProveedorFilter(FilterRazonSocial, FilterCuit, FilterNacionalExtranjero, Order, ColumnOrder).Count();
            return count;
        }
        public List<Beneficiarios> GetProveedorFilter(string FilterRazonSocial, string FilterCuit, string FilterNacionalExtranjero, string Order, string ColumnOrder)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                var query = context.Beneficiarios.Where(x => x.Estado == true);
                if (!string.IsNullOrEmpty(FilterRazonSocial))
                    query = query.Where(x => x.RazonSocial.Contains(FilterRazonSocial));
                if (!string.IsNullOrEmpty(FilterCuit))
                    query = query.Where(x => x.Cuit.Contains(FilterCuit));
                if (!string.IsNullOrEmpty(FilterNacionalExtranjero))
                    query = query.Where(x => x.NacionalExtranjero.Contains(FilterNacionalExtranjero));

                if (Order == "asc")
                    switch (ColumnOrder)
                    {
                        case "RazonSocial":
                            query = query.OrderBy(x => x.RazonSocial);
                            break;
                        case "Cuit":
                            query = query.OrderBy(x => x.Cuit);
                            break;
                        case "FechaAlta":
                            query = query.OrderBy(x => x.FechaAlta);
                            break;
                        case "Email":
                            query = query.OrderBy(x => x.Email);
                            break;
                        default:
                            query = query.OrderBy(x => x.Id);
                            break;
                    }
                else
                    switch (ColumnOrder)
                    {
                        case "RazonSocial":
                            query = query.OrderByDescending(x => x.RazonSocial);
                            break;
                        case "Cuit":
                            query = query.OrderByDescending(x => x.Cuit);
                            break;
                        case "FechaAlta":
                            query = query.OrderByDescending(x => x.FechaAlta);
                            break;
                        case "Email":
                            query = query.OrderByDescending(x => x.Email);
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
        public VMBeneficiario GetBeneficiarioById(int IdBeneficiario)
        {
            try
            {
                OrsnaDatabaseContext db = new OrsnaDatabaseContext(con);

                VMBeneficiario benef = VMBeneficiario.Map(db.Beneficiarios.Find(IdBeneficiario), con);
                
                return benef;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public GenericResponse<bool> Delete(int IdProveedor)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);

                string messageError = string.Empty;
                if (context.LibranzaBeneficiarios.Any(w => w.IdBeneficiario == IdProveedor && w.Estado.HasValue && w.Estado.Value))
                {
                    messageError += "El beneficiario no se puede eliminar porque tiene libranzas asociadas.";
                    return new GenericResponse<bool>() { Code = 501, Error = messageError };
                }
                else
                {
                    Beneficiarios proveedor = context.Beneficiarios.Find(IdProveedor);
                    var jsonOld = Utils.getJsonFromObject(proveedor);
                    proveedor.Estado = false;
                    context.SaveChanges();
                    AuditHelper.logEvent(context, Enums.AuditEventTypeEnum.BAJA, AUDITUBICACION, AUDITDELETE, null, "", jsonOld, Utils.getJsonFromObject(proveedor), userId);

                }
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
