using BD.Models;
using BD.ViewModels;
using BL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BL.Beneficiario
{
    public class BLBeneficiarioBancos : BLBase
    {
        private const string AUDITUBICACION = "Beneficiario Bancos";

        public BLBeneficiarioBancos(string stringConnection, string userId) : base(stringConnection, userId) { }

        public GenericResponse<bool> Save(BeneficiarioBancos newPB)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                var messageError = "";
                var pbExiste = context.BeneficiarioBancos.Where(x => x.Cbu == newPB.Cbu && x.Id != newPB.Id).Count();

                if (String.IsNullOrEmpty(newPB.Cbu))
                {
                    messageError += "el CBU no puede ser null.";
                }

                if (pbExiste > 0)
                    messageError += "Ya se encuentra registrado una CBU con el numero: " + newPB.Cbu + ".";


                if (messageError.Length > 0)
                    return new GenericResponse<bool>() { Code = 501, Error = messageError };

                var eventType = Enums.AuditEventTypeEnum.ALTA;
                string jsonOld = string.Empty;
                string jsonNew = string.Empty;

                if (newPB.Id > 0)
                {
                    BeneficiarioBancos pb = context.BeneficiarioBancos.Where(x => x.Id == newPB.Id).FirstOrDefault();
                    pb.IdBeneficiario = newPB.IdBeneficiario;
                    pb.TipoCuenta = newPB.TipoCuenta;
                    pb.NroCuenta = newPB.NroCuenta;
                    pb.Sucursal = newPB.Sucursal;
                    pb.Cbu = newPB.Cbu;
                    pb.Titular = newPB.Titular;
                    pb.FechaVigencia = newPB.FechaVigencia;
                    pb.BicSwift = newPB.BicSwift;
                    pb.Direccion = newPB.Direccion;
                    eventType = Enums.AuditEventTypeEnum.MODIFICACION;
                    pb.EsNacional = newPB.EsNacional;
                    jsonNew = Utils.getJsonFromObject(pb);
                }
                else
                {
                    context.BeneficiarioBancos.Add(newPB);
                    jsonNew = Utils.getJsonFromObject(newPB);
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
        public ICollection<VMBeneficiarioBancos> GetBancosToProveedor(int idBeneficiario)
        {
            try
            {
                OrsnaDatabaseContext db = new OrsnaDatabaseContext(con);
                var query = VMBeneficiarioBancos.MapList(db.BeneficiarioBancos.Where(x=> x.IdBeneficiario==idBeneficiario && x.Estado==true).ToList(),con);
                return query.ToList();
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public BeneficiarioBancos GetBancoById(int idBank)
        {
            try
            {
                OrsnaDatabaseContext db = new OrsnaDatabaseContext(con);
                var query = db.BeneficiarioBancos.Find(idBank);
                return query;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public GenericResponse<bool> Delete(int IdBanco)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);

                BeneficiarioBancos pb = context.BeneficiarioBancos.Find(IdBanco);
                var jsonOld = Utils.getJsonFromObject(pb);
                pb.Estado = false;
                context.SaveChanges();

                AuditHelper.logEvent(context, Enums.AuditEventTypeEnum.BAJA, AUDITUBICACION, AUDITDELETE, null, "", jsonOld, Utils.getJsonFromObject(pb), userId);

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
