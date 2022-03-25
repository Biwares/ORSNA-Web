using System;
using System.Collections.Generic;
using BD.Models;
using BD.Utilities;

namespace BD.ViewModels
{
    public class VMEmbargo
    {
        public int Id { get; set; }
        public int? IdLibranza { get; set; }
        public string NroEmbargo { get; set; }
        public string Responsable { get; set; }
        public decimal? Monto { get; set; }
        public decimal? Saldo { get; set; }

        public static VMEmbargo Map(BD.Models.LibranzaEmbargos l, string con)
        {
            OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
            VMEmbargo response = new VMEmbargo();

            response.Id = l.Id;
            response.IdLibranza = l.IdLibranza;
            response.NroEmbargo = l.NroEmbargo;
            response.Responsable = l.Responsable;
            response.Monto = l.Monto;
            response.Saldo = l.Saldo;

            return response;
        }

        public static ICollection<VMEmbargo> MapList(ICollection<BD.Models.LibranzaEmbargos> adj, string con)
        {
            ICollection<VMEmbargo> listResponse = new System.Collections.ObjectModel.Collection<VMEmbargo>();

            foreach (var lt in adj)
            {
                listResponse.Add(VMEmbargo.Map(lt, con));
            }

            return listResponse;
        }
    }
}
