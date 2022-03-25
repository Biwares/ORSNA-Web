using System;
using System.Collections.Generic;
using BD.ViewModels;
using BD.Models;
using System.Linq;

namespace BL.Rol
{
    public class BLRol : BLBase
    {
        public BLRol(string stringConnection, string userId) : base(stringConnection, userId) { }

        public GenericResponse<bool> Save(BD.Models.Rol newRol)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                var messageError = "";
                var rolExiste = context.Rol.Where(x => x.Nombre == newRol.Nombre && x.Id != newRol.Id && x.Estado == true).Count();

                if (rolExiste > 0)
                    messageError += "El nombre del rol: " + newRol.Nombre + " existe.";

                if (!newRol.Modulos.Any(x => x.Editar == true || x.Ver == true || x.Eliminar == true))
                    messageError += "Debe seleccionar al menos un permiso para poder guardar el rol.";

                if (messageError.Length > 0)
                    return new GenericResponse<bool>() { Code = 501, Error = messageError };


                if ((newRol.Id) > 0)
                {
                    var rol = context.Rol.Where(x => x.Id == newRol.Id).FirstOrDefault();
                    rol.Id = newRol.Id;
                    rol.Nombre = newRol.Nombre;
                    rol.Estado = true;

                    var changeRemoveA = context.RolModulo.Where(x => x.IdRol == rol.Id);
                    context.RolModulo.RemoveRange(changeRemoveA);
                    rol.Modulos = newRol.Modulos;
                }
                else
                {
                    newRol.Estado = true;
                    context.Rol.Add(newRol);
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
        public IList<BD.Models.Rol> GetAll(int page, string FilterNomRol, string Order, string ColumnOrder)
        {
            try
            {
                ICollection<BD.Models.Rol> roles = GetRolFilter(FilterNomRol, Order, ColumnOrder);

                if (page > 1)
                    roles = roles.Skip(cantidadElementosPorPagina * (page - 1)).ToList();

                if (page > 0)
                    roles = roles.Take(cantidadElementosPorPagina).ToList();

                return roles.ToList();
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public ICollection<VMRol> GetAll()
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                ICollection<VMRol> roles = VMRol.MapList(context.Rol.Where(x => x.Estado == true).ToList(), con);
                return roles;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }

        public int GetCountFilterElements(int page, string FilterNomRol, string Order, string ColumnOrder)
        {
            var count = GetRolFilter(FilterNomRol, Order, ColumnOrder).Count();
            return count;
        }
        public int GetCountPages(int page, string FilterNomRol, string Order, string ColumnOrder)
        {
            var resp = GetRolFilter(FilterNomRol, Order, ColumnOrder).Count();
            int rest = (resp % cantidadElementosPorPagina) > 0 ? 1 : 0;
            int pages = (resp / cantidadElementosPorPagina) + rest;
            return pages;
        }
        public List<BD.Models.Rol> GetRolFilter(string FilterNomRol, string Order, string ColumnOrder)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);

                var query = context.Rol.Where(x => x.Estado == true);

                if (!string.IsNullOrEmpty(FilterNomRol))
                    query = query.Where(x => x.Nombre.Contains(FilterNomRol));

                if (Order == "asc")
                    switch (ColumnOrder)
                    {
                        case "NomRol":
                            query = query.OrderBy(x => x.Nombre);
                            break;
                        default:
                            query = query.OrderBy(x => x.Id);
                            break;
                    }
                else
                    switch (ColumnOrder)
                    {
                        case "NomRol":
                            query = query.OrderByDescending(x => x.Nombre);
                            break;
                        default:
                            query = query.OrderByDescending(x => x.Id);
                            break;
                    }

                return query.ToList();
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                OrsnaDatabaseContext db2 = new OrsnaDatabaseContext(con);
                db2.Log.Add(new Log() { Fecha = DateTime.Now, Ubicacion = "BLRol", Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();

                return null;
            }
        }
        public BD.Models.Rol GetRolById(int IdRol)
        {
            try
            {
                OrsnaDatabaseContext db = new OrsnaDatabaseContext(con);
                return db.Rol.Find(IdRol);
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);

                return null;
            }
        }
        public GenericResponse<bool> Delete(int IdRol)
        {
            try
            {
                var db = new OrsnaDatabaseContext(con);
                string messageError = string.Empty;
                if (db.UsuarioRol.Any(w => w.IdRol == IdRol && w.Usuario.Estado.HasValue && w.Usuario.Estado.Value))
                {
                    messageError += "El rol no se puede eliminar porque tiene usuarios asociados.";
                    return new GenericResponse<bool>() { Code = 501, Error = messageError };
                }

                var change = db.Rol.FirstOrDefault(x => x.Id == IdRol);
                change.Estado = false;
                db.SaveChanges();

                return new GenericResponse<bool>() { Code = 200, Result = true };
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return new GenericResponse<bool>() { Code = 500, Error = ex.Message };
            }
        }

        public ICollection<VMRolModulo> GetModulos(int idRol)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                List<BD.Models.RolModulo> modulos = new List<BD.Models.RolModulo>();

                if (context.RolModulo.Any(x => x.IdRol == idRol))
                {
                    modulos = context.RolModulo.Where(x => x.IdRol == idRol).ToList();
                }
                else
                {
                    foreach (var item in context.Modulos.ToList())
                    {
                        modulos.Add(new BD.Models.RolModulo()
                        {
                            Editar = false,
                            Ver = false,
                            Eliminar = false,
                            IdModulo = item.Id,
                            Modulo = item,
                        });
                    }
                }
                ICollection<VMRolModulo> roles = VMRolModulo.MapListModulo(modulos, con);
                return roles;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
    }
}
