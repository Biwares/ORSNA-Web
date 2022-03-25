using System;
using System.Collections.Generic;
using BD.Models;

namespace BD.ViewModels
{
    public class VMArea
    {
        public int IdArea { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }

        public static VMArea Map(BD.Models.Areas p, string con)
        {
            OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
            VMArea response = new VMArea();

            response.IdArea = p.Id;
            response.Nombre = p.Nombre;
            response.Codigo = p.Codigo;

            return response;
        }

        public static ICollection<VMArea> MapList(ICollection<BD.Models.Areas> area, string con)
        {
            ICollection<VMArea> listResponse = new System.Collections.ObjectModel.Collection<VMArea>();

            foreach (var a in area)
            {
                listResponse.Add(VMArea.Map(a, con));
            }

            return listResponse;
        }
    }
}
