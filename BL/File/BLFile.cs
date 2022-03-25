using BD.Models;
using BD.Utilities;
using BD.ViewModels;
using System;

namespace BL.File
{
    public class BLFile : BLBase
    {
        public BLFile(string stringConnection, string userId) : base(stringConnection, userId) { }

        public VMAdjunto GetAdjuntoById(string IdAdjunto)
        {
            try
            {
                int Id = int.Parse(DataEncode.Base64Decode(IdAdjunto));
                OrsnaDatabaseContext db = new OrsnaDatabaseContext(con);

                VMAdjunto adj = VMAdjunto.Map(db.Adjuntos.Find(Id), con);

                return adj;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
    }
}
