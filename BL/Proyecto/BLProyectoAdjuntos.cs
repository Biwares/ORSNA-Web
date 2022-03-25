using BD.Models;
using BD.ViewModels;
using BL.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Proyecto
{
    public class BLProyectoAdjuntos : BLBase
    {
        private const string AUDITUBICACION = "Proyecto Adjuntos";

        public BLProyectoAdjuntos(string stringConnection, string userId) : base(stringConnection, userId) { }

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
                ProyectoAdjuntos pAdjunto = new ProyectoAdjuntos
                {
                    IdProyecto = vm.idEntidad,
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
        public IEnumerable<ProyectoAdjuntos> Get(int? id)
        {
            try
            {
                var _context = new OrsnaDatabaseContext(con);
                IEnumerable<ProyectoAdjuntos> pA = null;
                if (id != null)
                    pA = _context.ProyectoAdjuntos.Where(x => x.Id == id);
                else
                    pA = _context.ProyectoAdjuntos;
                return pA;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public ICollection<VMAdjunto> GetAdjuntosByProyecto(int id)
        {
            try
            {
                OrsnaDatabaseContext db = new OrsnaDatabaseContext(con);

                ICollection<VMAdjunto> adj = VMAdjunto.MapList(
                    db.Adjuntos.Where(
                a => a.ProyectoAdjuntos.Where(
                    ba => ba.IdProyecto == id && ba.Estado==true).Select(
                    ad => ad.IdProyecto).Contains(id))
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
                var pAdjunto = db.ProyectoAdjuntos.SingleOrDefault(m => m.IdAdjunto == id);
                if (pAdjunto == null)
                {
                    return "No existe";
                }
                var jsonOld = Utils.getJsonFromObject(pAdjunto);
                pAdjunto.Estado = false;
                int? idAdjunto = pAdjunto.IdAdjunto;

                OrsnaDatabaseContext db2 = new OrsnaDatabaseContext(con);

                var adjunto = db2.Adjuntos.SingleOrDefault(a => a.Id == idAdjunto);
                adjunto.Estado = false;

                db.SaveChanges();
                db2.SaveChanges();

                AuditHelper.logEvent(db, Enums.AuditEventTypeEnum.BAJA, AUDITUBICACION, AUDITDELETE, null, "", jsonOld, Utils.getJsonFromObject(pAdjunto), userId);

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
