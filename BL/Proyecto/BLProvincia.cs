using BD.Models;
using BD.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL.Proyecto
{
    public class BLProvincia : BLBase
    {
        public BLProvincia(string stringConnection, string userId) : base(stringConnection, userId) { }

        public ICollection<VMProvincia> GetAll()
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                ICollection<VMProvincia> prov = VMProvincia.MapList(context.Provincias.Where(x => x.Estado == true).ToList(), con);
                return prov;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
    }
}
