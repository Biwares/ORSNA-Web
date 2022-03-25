using BD.Models;
using BD.ViewModels;
using BL.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Proyecto
{
    public class BLLibranzaAdjuntos : BLBase
    {
        private const string AUDITUBICACION = "Libranza Adjuntos";

        public BLLibranzaAdjuntos(string stringConnection, string userId) : base(stringConnection, userId) { }

        public async Task<VMAdjunto> Post([FromForm]FileB vm, string modulo)
        {
            try
            {
                var _context = new OrsnaDatabaseContext(con);
                Adjuntos Adjunto = new Adjuntos
                {
                    Modulo = modulo,
                    NombreArchivo = vm.archivo.FileName,
                    TipoAnexo = vm.tipoAnexo,
                    FechaAlta = DateTime.Now
                };
                _context.Adjuntos.Add(Adjunto);
                int idAdjunto = await _context.SaveChangesAsync();

                var _context2 = new OrsnaDatabaseContext(con);
                LibranzaAdjuntos pAdjunto = new LibranzaAdjuntos
                {
                    IdLibranza = vm.idEntidad,
                    IdAdjunto = Adjunto.Id
                };

                _context2.Add(pAdjunto);


                await _context2.SaveChangesAsync();
                VMAdjunto vma = new VMAdjunto();
                vma = VMAdjunto.Map(Adjunto, con);

                AuditHelper.logEvent(_context, Enums.AuditEventTypeEnum.ALTA, AUDITUBICACION, "Post", null, "", "", Utils.getJsonFromObject(pAdjunto), userId);
                return vma;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public IEnumerable<LibranzaAdjuntos> Get(int? id)
        {
            try
            {
                var _context = new OrsnaDatabaseContext(con);
                IEnumerable<LibranzaAdjuntos> lA = null;
                if (id != null)
                    lA = _context.LibranzaAdjuntos.Where(x => x.Id == id);
                else
                    lA = _context.LibranzaAdjuntos;
                return lA;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public ICollection<VMAdjunto> GetAdjuntosByLibranza(int id)
        {
            try
            {
                OrsnaDatabaseContext db = new OrsnaDatabaseContext(con);

                ICollection<VMAdjunto> adj = VMAdjunto.MapList(
                    db.Adjuntos.Where(
                a => a.LibranzaAdjuntos.Where(
                    ba => ba.IdLibranza == id && ba.Estado==true).Select(
                    ad => ad.IdLibranza).Contains(id))
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
        public async Task<string> Delete(int id)
        {
            try
            {
                OrsnaDatabaseContext db = new OrsnaDatabaseContext(con);
                var lAdjunto = db.LibranzaAdjuntos.SingleOrDefault(m => m.IdAdjunto == id);
                if (lAdjunto == null)
                {
                    return "No existe";
                }
                var jsonOld = Utils.getJsonFromObject(lAdjunto);
                lAdjunto.Estado = false;
                int? idAdjunto = lAdjunto.IdAdjunto;

                OrsnaDatabaseContext db2 = new OrsnaDatabaseContext(con);

                var adjunto = db2.Adjuntos.SingleOrDefault(a => a.Id == idAdjunto);
                adjunto.Estado = false;

                db.SaveChanges();
                db2.SaveChanges();

                AuditHelper.logEvent(db, Enums.AuditEventTypeEnum.BAJA, AUDITUBICACION, AUDITDELETE, null, "", jsonOld, Utils.getJsonFromObject(lAdjunto), userId);

                return "se elimino con éxito";
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
    }
}
