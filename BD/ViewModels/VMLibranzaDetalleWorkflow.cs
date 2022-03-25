using BD.Models;
using System;
using System.Collections.Generic;


namespace BD.ViewModels
{
    public class VMLibranzaDetalleWorkflow
    {
        public int Id { get; set; }
        public int IdLibranza { get; set; }
        public DateTime Fecha { get; set; }
        public int IdUsuario { get; set; }
        public string Observaciones { get; set; }
        public VMLibranzasEstado EstadoAnterior { get; set; }
        public VMLibranzasEstado NuevoEstado { get; set; }
        public string Usuario { get; set; }

        public int? IdEstadoAnterior { get; set; }
        public int? IdNuevoEstado { get; set; }
        public DateTime? FechaPago { get; set; }

        public decimal? Retencion { get; set; }
        public string RetencionObservaciones { get; set; }

        public int? MonedaActualId { get; set; }
        public Moneda MonedaActual { get; set; }
        public string MonedaActualNombre { get; set; }
        public decimal TasaDeCambioActual { get; set; }

        public static VMLibranzaDetalleWorkflow Map(BD.Models.LibranzaDetalleWorkflow l, string con)
        {
            OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
            VMLibranzaDetalleWorkflow response = new VMLibranzaDetalleWorkflow();

            response.Id = l.Id;
            response.IdLibranza = l.IdLibranza;
            response.Fecha = l.Fecha;
            response.IdUsuario = l.IdUsuario;
            response.Usuario = context.Usuarios.Find(l.IdUsuario).Email;
            response.Observaciones = l.Observaciones;
            response.EstadoAnterior = VMLibranzasEstado.Map(context.LibranzasEstado.Find(l.IdEstadoAnterior), con);
            response.NuevoEstado = VMLibranzasEstado.Map(context.LibranzasEstado.Find(l.IdNuevoEstado), con);
            response.MonedaActualId = l.MonedaActualId;
            response.MonedaActualNombre = (l.MonedaActualId != null) ? context.Moneda.Find(l.MonedaActualId).NombreCorto : String.Empty;
            response.TasaDeCambioActual = l.TasaDeCambioActual;

            return response;
        }

        public static ICollection<VMLibranzaDetalleWorkflow> MapList(ICollection<BD.Models.LibranzaDetalleWorkflow> adj, string con)
        {
            ICollection<VMLibranzaDetalleWorkflow> listResponse = new System.Collections.ObjectModel.Collection<VMLibranzaDetalleWorkflow>();

            foreach (var lw in adj)
            {
                listResponse.Add(Map(lw, con));
            }

            return listResponse;
        }
        public static LibranzaDetalleWorkflow Map(VMLibranzaDetalleWorkflow l, string con)
        {
            OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
            LibranzaDetalleWorkflow response = new LibranzaDetalleWorkflow();

            response.Id = l.Id;
            response.IdLibranza = l.IdLibranza;
            response.Fecha = l.Fecha;
            response.IdUsuario = l.IdUsuario;
            response.Observaciones = l.Observaciones;
            response.IdEstadoAnterior = l.EstadoAnterior.Id;
            response.IdEstadoAnterior = l.NuevoEstado.Id;
            response.MonedaActualId = l.MonedaActualId;
            response.TasaDeCambioActual = l.TasaDeCambioActual;

            return response;
        }

        public static ICollection<LibranzaDetalleWorkflow> MapList(ICollection<VMLibranzaDetalleWorkflow> adj, string con)
        {
            ICollection<LibranzaDetalleWorkflow> listResponse = new System.Collections.ObjectModel.Collection<LibranzaDetalleWorkflow>();

            foreach (var lw in adj)
            {
                listResponse.Add(Map(lw, con));
            }

            return listResponse;
        }
    }
}
