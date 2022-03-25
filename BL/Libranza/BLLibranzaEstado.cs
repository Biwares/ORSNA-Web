using BD.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BL.Libranza
{

    public class BLLibranzaEstado : BLBase
    {
        public BLLibranzaEstado(string stringConnection, string userId) : base(stringConnection, userId) { }

        public ICollection<LibranzasEstado> GetAll()
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                ICollection<LibranzasEstado> le = context.LibranzasEstado.Where(x => x.Estado == true).ToList();
                return le;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
    }
}
