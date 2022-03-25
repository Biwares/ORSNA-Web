using System;
using System.Collections.Generic;
using BD.ViewModels;
using BD.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using ElmahCore;

namespace BL.Aeropuerto
{
    public class BLAeropuertosGrupo : BLBase
    {
        public BLAeropuertosGrupo(string stringConnection, string userId) : base(stringConnection, userId) { }

        public ICollection<VMAeropuertosGrupo> GetAll()
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                ICollection<VMAeropuertosGrupo> aeros = VMAeropuertosGrupo.MapList(context.AeropuertosGrupo.Where(x => x.Estado == true).ToList(), con);
                return aeros;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
    }
}
