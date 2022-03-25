using BD.Models;
using System.Collections.Generic;

namespace BD.ViewModels
{
    public class VMLog
    {
        public int Id { get; set; }
        public string Ubicacion { get; set; }
        public string Mensaje { get; set; }
        public string Detalle { get; set; }
        public string UserName { get; set; }

        public static VMLog Map(Log p, string con)
        {
            OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
            VMLog response = new VMLog();

            response.Id = p.Id;
            response.Ubicacion = p.Ubicacion;
            response.Mensaje = p.Mensaje;
            response.Detalle = p.Detalle;
            response.UserName = p.UserName;

            return response;
        }

        public static ICollection<VMLog> MapList(ICollection<Log> logs, string con)
        {
            ICollection<VMLog> listResponse = new System.Collections.ObjectModel.Collection<VMLog>();

            foreach (var log in logs)
            {
                listResponse.Add(VMLog.Map(log, con));
            }

            return listResponse;
        }
    }
}
