using BD.Models;
using System;
using System.Collections.Generic;

namespace BD.ViewModels
{
    public class VMFactura
    {
        public int Id { get; set; }
        public int? IdLibranza { get; set; }
        public string Tipo { get; set; }
        public string Nro { get; set; }
        public DateTime Fecha { get; set; }
        public decimal? Monto { get; set; }
        public decimal? Iva { get; set; }
        public decimal? Ibb { get; set; }

        public static VMFactura Map(BD.Models.LibranzaFacturas l, string con)
        {
            OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
            VMFactura response = new VMFactura();

            response.Id = l.Id;
            response.IdLibranza = l.IdLibranza;
            response.Tipo = l.Tipo;
            response.Nro = l.Nro;
            response.Fecha = l.Fecha;
            response.Monto = l.Monto;
            response.Iva = l.Iva;
            response.Ibb = l.Ibb;
            
            return response;
        }

        public static ICollection<VMFactura> MapList(ICollection<BD.Models.LibranzaFacturas> adj, string con)
        {
            ICollection<VMFactura> listResponse = new System.Collections.ObjectModel.Collection<VMFactura>();

            foreach (var lf in adj)
            {
                listResponse.Add(Map(lf, con));
            }

            return listResponse;
        }
    }
}
