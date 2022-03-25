using BD.Models;
using System.Collections.Generic;

namespace BD.ViewModels
{
    public class VMUsuario
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool CheckAD { get; set; }

        public static VMUsuario Map(BD.Models.Usuarios p, string con)
        {
            OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
            VMUsuario response = new VMUsuario();

            response.Id = p.Id;
            response.Password = p.Password;
            response.Email = p.Email;
            response.CheckAD = p.CheckAD;

            return response;
        }

        public static ICollection<VMUsuario> MapList(ICollection<BD.Models.Usuarios> Usuario, string con)
        {
            ICollection<VMUsuario> listResponse = new System.Collections.ObjectModel.Collection<VMUsuario>();

            foreach (var a in Usuario)
            {
                listResponse.Add(VMUsuario.Map(a, con));
            }

            return listResponse;
        }
    }
}
