using System;
using System.Collections.Generic;
using System.Linq;
using BD.Models;

namespace BD.ViewModels
{
    public class VMCuenta
    {
        public int Id { get; set; }
        public string NroCuenta { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaVigencia { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public VMLibranzaTipo LibranzaTipo { get; set; }
        public VMAeropuertosGrupo AeropuertosGrupo { get; set; }


        public static VMCuenta Map(BD.Models.Cuentas p, string con)
        {
            OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
            VMCuenta response = new VMCuenta();

            response.Id = p.Id;
            response.NroCuenta = p.NroCuenta;
            response.Nombre = p.Nombre;
            response.Descripcion = p.Descripcion;
            response.FechaVigencia = p.FechaVigencia;
            response.FechaCreacion = p.FechaCreacion;
            if (p.IdLibranzaTipo > 0)
                response.LibranzaTipo = VMLibranzaTipo.Map(context.LibranzaTipo.FirstOrDefault(x => x.Id == p.IdLibranzaTipo), con);
            if (p.IdAeropuertosGrupo > 0)
                response.AeropuertosGrupo = VMAeropuertosGrupo.Map(context.AeropuertosGrupo.FirstOrDefault(x => x.Id == p.IdAeropuertosGrupo), con);


            return response;
        }

        public static ICollection<VMCuenta> MapList(ICollection<BD.Models.Cuentas> cuenta, string con)
        {
            ICollection<VMCuenta> listResponse = new System.Collections.ObjectModel.Collection<VMCuenta>();

            foreach (var c in cuenta)
            {
                listResponse.Add(VMCuenta.Map(c, con));
            }

            return listResponse;
        }
    }
}
