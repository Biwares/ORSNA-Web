using System;
using System.Collections.Generic;
using BD.ViewModels;
using BD.Models;
using System.Linq;
using System.Threading.Tasks;
using System.DirectoryServices;
using Microsoft.Extensions.Configuration;

namespace BL.Usuario

{
    public class BLUsuario : BLBase
    {
        public BLUsuario(string stringConnection) : base(stringConnection) { }
        public BLUsuario(string stringConnection, string userId) : base(stringConnection, userId) { }
        public GenericResponse<bool> Save(Usuarios newUsuario)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                var messageError = "";
                var usuario = newUsuario.Email.Trim();
                var UsuarioExiste = context.Usuarios.Where(x => x.Email == usuario && x.Id != newUsuario.Id && x.Estado == true).Count();

                if (String.IsNullOrEmpty(usuario))
                {
                    messageError += "el email no puede ser null.";
                }

                if (string.IsNullOrEmpty(messageError) && UsuarioExiste > 0)
                    messageError += "El email: " + newUsuario.Email + " existe.";

                if (string.IsNullOrEmpty(messageError) && (newUsuario.UsuarioRol.Count() == 0 || newUsuario.UsuariosAreas.Count() == 0))
                    messageError += "Un usuario debe tener al menos un rol y un área.";

                if (messageError.Length > 0)
                    return new GenericResponse<bool>() { Code = 501, Error = messageError };


                if ((newUsuario.Id) > 0)
                {
                    Usuarios Usuario = context.Usuarios.Find(newUsuario.Id);
                    Usuario.Email = newUsuario.Email;
                    Usuario.Password = newUsuario.Password;
                    Usuario.CheckAD = newUsuario.CheckAD;

                    var changeRemove = context.UsuarioRol.Where(x => x.IdUsuario == newUsuario.Id);
                    context.UsuarioRol.RemoveRange(changeRemove);

                    var changeRemoveA = context.UsuariosAreas.Where(x => x.IdUsuario == newUsuario.Id);
                    context.UsuariosAreas.RemoveRange(changeRemoveA);

                    Usuario.UsuarioRol = newUsuario.UsuarioRol;
                    Usuario.UsuariosAreas = newUsuario.UsuariosAreas;
                }
                else
                {
                    context.Usuarios.Add(newUsuario);
                }


                context.SaveChanges();

                return new GenericResponse<bool>() { Code = 200, Result = true };
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return new GenericResponse<bool>() { Code = 500, Error = ex.Message };
            }
        }

        public int? GetUsuarioByUserName(string UserName)
        {
            try
            {
                OrsnaDatabaseContext db = new OrsnaDatabaseContext(con);
                return db.Usuarios.Where(x => x.Email.Equals(UserName)).Select(s=>s.Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }

        public IList<Usuarios> GetAll(int page, string FilterEmail, string Order, string ColumnOrder)
        {
            try
            {

                ICollection<Usuarios> usuarios = GetUsuariosFilter(FilterEmail, Order, ColumnOrder);

                if (page > 1)
                    usuarios = usuarios.Skip(cantidadElementosPorPagina * (page - 1)).ToList();

                if (page > 0)
                    usuarios = usuarios.Take(cantidadElementosPorPagina).ToList();

                return usuarios.ToList();

            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public ICollection<VMUsuario> GetAll()
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                ICollection<VMUsuario> Usuarios = VMUsuario.MapList(context.Usuarios.Where(x => x.Estado == true).ToList(), con);
                return Usuarios;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }

        public int GetCountFilterElements(int page, string FilterEmail, string Order, string ColumnOrder)
        {
            var count = GetUsuariosFilter(FilterEmail, Order, ColumnOrder).Count();
            return count;
        }
        public int GetCountPages(int page, string FilterEmail, string Order, string ColumnOrder)
        {
            var resp = GetUsuariosFilter(FilterEmail, Order, ColumnOrder).Count();
            int rest = (resp % cantidadElementosPorPagina) > 0 ? 1 : 0;
            int pages = (resp / cantidadElementosPorPagina) + rest;
            return pages;
        }
        public List<Usuarios> GetUsuariosFilter(string FilterEmail, string Order, string ColumnOrder)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                var query = context.Usuarios.Where(x => 1 == 1);

                if (!string.IsNullOrEmpty(FilterEmail))
                    query = query.Where(x => x.Email.Contains(FilterEmail));

                if (Order == "asc")
                    switch (ColumnOrder)
                    {
                        case "Email":
                            query = query.OrderBy(x => x.Email);
                            break;
                        default:
                            query = query.OrderBy(x => x.Id);
                            break;
                    }
                else
                    switch (ColumnOrder)
                    {
                        case "Password":
                            query = query.OrderByDescending(x => x.Password);
                            break;
                        case "Email":
                            query = query.OrderByDescending(x => x.Email);
                            break;
                        default:
                            query = query.OrderByDescending(x => x.Id);
                            break;
                    }
                return query.ToList();
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }

        public async Task<Usuarios> Authenticate(string username, string password)
        {
            OrsnaDatabaseContext db = new OrsnaDatabaseContext(con);
            var user = await Task.Run(() => db.Usuarios.SingleOrDefault(x => x.Email == username && x.Password == password && !x.CheckAD));

            // return null if user not found
            if (user == null)
            {
                user = await Task.Run(() => db.Usuarios.SingleOrDefault(x => x.Email == username && x.CheckAD));
                if (user == null)
                    return null;

                var pathLDAP = AppSettingsConfig.Configuration.GetValue<string>("MyConfig:LdadPath");

                using (DirectoryEntry adsEntry = new DirectoryEntry(pathLDAP, user.Email.Split('@')[0], password))
                {
                    using (DirectorySearcher adsSearcher = new DirectorySearcher(adsEntry))
                    {
                        adsSearcher.Filter = "(sAMAccountName=" + user.Email.Split('@')[0] + ")";

                        try
                        {
                            SearchResult adsSearchResult = adsSearcher.FindOne();
                        }
                        catch (Exception ex)
                        {
                            Utils.manageExceptionContext(ex);
                            return null;
                        }
                        finally
                        {
                            adsEntry.Close();
                        }
                    }
                }
            }

            // authentication successful so return user details without password
            user.Password = null;
            return user;
        }

        public Usuarios GetUsuarioById(int IdUsuario)
        {
            try
            {
                OrsnaDatabaseContext db = new OrsnaDatabaseContext(con);
                return db.Usuarios.Where(x => x.Id == IdUsuario).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public GenericResponse<bool> Delete(int idUsuario)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);

                Usuarios Usuario = context.Usuarios.Find(idUsuario);
                Usuario.Estado = !Usuario.Estado;
                context.SaveChanges();

                return new GenericResponse<bool>() { Code = 200, Result = true };
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return new GenericResponse<bool>() { Code = 500, Error = ex.Message };
            }
        }
        public ICollection<UsuariosAreas> GetAreas(int idUsuario)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                var areas = context.Areas.Where(x => x.Estado == true).Select(x => x.Id).ToList();
                var query = context.UsuariosAreas.Where(x => x.IdUsuario == idUsuario && areas.Contains(x.IdArea.Value));
                return query.ToList();
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public GenericResponse<bool> SaveAreas(List<UsuariosAreas> newUsuariosAreas)
        {
            try
            {
                int? idUsuario = newUsuariosAreas.First().IdUsuario;

                var db = new OrsnaDatabaseContext(con);
                var changeRemove = db.UsuariosAreas.Where(x => x.IdUsuario == idUsuario);
                db.UsuariosAreas.RemoveRange(changeRemove);

                foreach (var it in newUsuariosAreas)
                {
                    var change = db.UsuariosAreas.Add(new UsuariosAreas());
                    change.Entity.IdUsuario = it.IdUsuario;
                    change.Entity.IdArea = it.IdArea;
                }

                db.SaveChanges();

                return new GenericResponse<bool>() { Code = 200, Result = true };
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return new GenericResponse<bool>() { Code = 500, Error = ex.Message };
            }
        }
        public ICollection<UsuarioRol> GetRoles(int idUsuario)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                var roles = context.Rol.Where(x => x.Estado == true).Select(x => x.Id).ToList();
                var query = context.UsuarioRol.Where(x => x.IdUsuario == idUsuario && roles.Contains(x.IdRol));
                return query.ToList();
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public GenericResponse<bool> SaveRoles(List<UsuarioRol> newUsuariosRoles)
        {
            try
            {
                int? idUsuario = newUsuariosRoles.First().IdUsuario;

                var db = new OrsnaDatabaseContext(con);
                var changeRemove = db.UsuarioRol.Where(x => x.IdUsuario == idUsuario);
                db.UsuarioRol.RemoveRange(changeRemove);

                foreach (var it in newUsuariosRoles)
                {
                    var change = db.UsuarioRol.Add(new UsuarioRol());
                    change.Entity.IdUsuario = it.IdUsuario;
                    change.Entity.IdRol = it.IdRol;
                }

                db.SaveChanges();

                return new GenericResponse<bool>() { Code = 200, Result = true };
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return new GenericResponse<bool>() { Code = 500, Error = ex.Message };
            }
        }
    }
}
