using BD.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL.Proyecto
{
    public class BLLibranzaEstado : BLBase
    {
        public BLLibranzaEstado(string stringConnection, string userId) : base(stringConnection, userId) { }

        public ICollection<ProyectosEstado> GetAll()
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                ICollection<ProyectosEstado> pe = context.ProyectosEstado.Where(x => x.Estado == true).ToList();
                return pe;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
    }
}
