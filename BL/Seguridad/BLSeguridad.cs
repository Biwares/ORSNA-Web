using BD.Models;
using BD.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL.Seguridad
{
    public class BLSeguridad : BLBase
    {
        public BLSeguridad(string stringConnection, string userId) : base(stringConnection, userId) { }

        public GenericResponse<LoginResponse> CheckUserPass(string username, string password)
        {
            try
            {
                var db = new OrsnaDatabaseContext(con);
                var user = db.Usuarios.Where(x => x.Email == username && x.Password == password);
                if (user.Count() > 0)
                    return new GenericResponse<LoginResponse>()
                    {
                        Code = 200,
                        Result = new LoginResponse()
                        {
                            Response = "Login Correcto.",
                            Estado = true,
                            username = username,
                            password = password,
                            IdUsuario = user.First().Id
                        }
                    };

                return new GenericResponse<LoginResponse>() { Code = 200, Result = new LoginResponse() { Response = "Login Incorrecto.", Estado = false } };
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return new GenericResponse<LoginResponse>() { Code = 200, Result = new LoginResponse() { Response = "Login Incorrecto.", Estado = false } };
            }
        }

        public VMPermisos getPermisos(int? idUsuario, string Email, string modulo)
        {
            try
            {
                VMPermisos permisos = new VMPermisos();
                var db = new OrsnaDatabaseContext(con);

                var queryIdUsuario = db.Usuarios.AsQueryable();

                var query = db.UsuarioRol.AsQueryable();



                if (idUsuario.HasValue)
                    query = query.Where(x => x.IdUsuario == idUsuario);
                else
                {
                    var idUsuarioEmail = queryIdUsuario.Where(x => x.Email.Equals(Email)).Single().Id;
                    query = query.Where(x => x.IdUsuario == idUsuarioEmail);
                }


                var UsuarioRol = query.Select(x => x.IdRol).ToList();
                var idModulo = db.Modulos.Where(x => x.Nombre.Equals(modulo)).Select(x => x.Id).Single();
                var Roles = db.RolModulo.Where(x => UsuarioRol.Contains(x.IdRol) && x.IdModulo == idModulo).ToList();

                permisos.Editar = Roles.Any(x => x.Editar);
                permisos.Eliminar = Roles.Any(x => x.Eliminar);
                return permisos;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }

        public List<Modulos> getPermisosVer(int? idUsuario,string Email)
        {
            try
            {
                var db = new OrsnaDatabaseContext(con);

                var queryIdUsuario = db.Usuarios.AsQueryable();

                var query = db.UsuarioRol.AsQueryable();
                


                if (idUsuario.HasValue)
                    query = query.Where(x => x.IdUsuario == idUsuario);
                else
                {
                    var idUsuarioEmail = queryIdUsuario.Where(x => x.Email.Equals(Email)).Single().Id;
                    query = query.Where(x => x.IdUsuario == idUsuarioEmail);
                } 
                    

                var UsuarioRol = query.Select(x => x.IdRol).ToList();
                var Roles = db.RolModulo.Where(x => UsuarioRol.Contains(x.IdRol) && x.Ver).Select(x => x.IdModulo).ToList();
                return db.Modulos.Where(x => Roles.Contains(x.Id)).ToList();
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public List<int> GetAreasDelUsuario(int? idUsuario, string Email)
        {
            try
            {
                var db = new OrsnaDatabaseContext(con);

                var queryIdUsuario = db.Usuarios.AsQueryable();

                var query = db.UsuariosAreas.AsQueryable();

                if (idUsuario.HasValue)
                    query = query.Where(x => x.IdUsuario == idUsuario);
                else
                {
                    var idUsuarioEmail = queryIdUsuario.Where(x => x.Email.Equals(Email)).Single().Id;
                    query = query.Where(x => x.IdUsuario == idUsuarioEmail);
                }

                return query.Where(x => x.IdArea.HasValue && x.Estado == true).Select(x => x.IdArea.Value).ToList();
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
    }
}
