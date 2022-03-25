using BD.Models;
using BD.ViewModels;
using BL.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Beneficiario
{
    public class BLBeneficiarioAdjuntos : BLBase
    {
        private const string AUDITUBICACION = "Beneficiario Adjunto";

        public BLBeneficiarioAdjuntos(string stringConnection, string userId) : base(stringConnection, userId) { }


        public async Task<BeneficiarioAdjuntos> Post([FromForm]FileB vm, string modulo)
        {
            try
            {
                var _context = new OrsnaDatabaseContext(con);
                Adjuntos Adjunto= new Adjuntos
                {
                    Modulo = modulo,
                    NombreArchivo = vm.archivo.FileName,
                    FechaAlta = DateTime.Now
                };
                _context.Adjuntos.Add(Adjunto);
                int idAdjunto = await _context.SaveChangesAsync();

                var _context2 = new OrsnaDatabaseContext(con);
                BeneficiarioAdjuntos bAdjunto = new BeneficiarioAdjuntos {
                    IdBeneficiario = vm.idEntidad,
                    IdAdjunto = Adjunto.Id
                };

                _context2.Add(bAdjunto);

                await _context2.SaveChangesAsync();

                AuditHelper.logEvent(_context2, Enums.AuditEventTypeEnum.ALTA, AUDITUBICACION, AUDITSAVE, null, "", "", Utils.getJsonFromObject(bAdjunto), userId);

                return bAdjunto;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);

                return null;
            }
        }
        public IEnumerable<BeneficiarioAdjuntos> Get(int? id)
        {
            try
            {
                var _context = new OrsnaDatabaseContext(con);
                IEnumerable<BeneficiarioAdjuntos> bA = null;
                if (id!=null)
                    bA = _context.BeneficiarioAdjuntos.Where(x => x.Id == id);
                else
                    bA = _context.BeneficiarioAdjuntos;
                return bA;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);

                return null;
            }
        }
        public IEnumerable<VMAdjunto> GetAdjuntosByBeneficiario(int id)
        {
            try
            {
                OrsnaDatabaseContext db = new OrsnaDatabaseContext(con);

                IEnumerable<VMAdjunto> adj = VMAdjunto.MapList(
                    db.Adjuntos.Where(
                a => a.BeneficiarioAdjuntos.Where(
                    ba => ba.IdBeneficiario == id).Select(
                    ad => ad.IdBeneficiario).Contains(id))
                    .ToList(), con
                    );
                
                return adj;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);

                return null;
            }
        }
        public string Delete(int id)
        {
            try
            {
                OrsnaDatabaseContext db = new OrsnaDatabaseContext(con);
                var bAdjunto = db.BeneficiarioAdjuntos.SingleOrDefault(m => m.IdAdjunto == id);
                if (bAdjunto == null)
                {
                    return "No existe";
                }

                var jsonOld = Utils.getJsonFromObject(bAdjunto);
                bAdjunto.Estado = false;

                db.SaveChanges();
                AuditHelper.logEvent(db, Enums.AuditEventTypeEnum.BAJA, AUDITUBICACION, AUDITDELETE, null, "", jsonOld, Utils.getJsonFromObject(bAdjunto), userId);

                int? idAdjunto = bAdjunto.IdAdjunto;
                
                OrsnaDatabaseContext db2 = new OrsnaDatabaseContext(con);

                var adjunto = db2.Adjuntos.SingleOrDefault(a=> a.Id==idAdjunto);
                jsonOld = Utils.getJsonFromObject(adjunto);
                adjunto.Estado = false;
                
                db2.SaveChanges();

                AuditHelper.logEvent(db2, Enums.AuditEventTypeEnum.BAJA, AUDITUBICACION, AUDITDELETE, null, "", jsonOld, Utils.getJsonFromObject(adjunto), userId);


                return "Se eliminó con éxito";
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);

                return null;
            }
        }
    }
}
