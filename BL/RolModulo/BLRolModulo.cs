using System;
using System.Collections.Generic;
using BD.ViewModels;
using BD.Models;
using System.Linq;

namespace BL.RolModulo
{
    public class BLRolModulo:BLBase
    {

        public BLRolModulo(string stringConnection, string userId) : base(stringConnection, userId) { }

        public GenericResponse<bool> Save(BD.Models.RolModulo newRolModulo)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                var messageError = "";
                var rolModuloExiste = context.RolModulo.Where(x => x.IdRol  == newRolModulo.IdRol && x.Id != newRolModulo.Id).Count();

                if (rolModuloExiste > 0)
                    messageError += "El nombre del rol modulo: " + newRolModulo.IdRol + " existe.";

                if (messageError.Length > 0)
                    return new GenericResponse<bool>() { Code = 501, Error = messageError };

/*
                if ((newRolModulo.Id) > 0)
                {
                    var rol = context.RolModulo.Where(x => x.IdRol == newRolModulo.Id).FirstOrDefault();
                    rol.Id = newRolModulo.Id;
                    rol.Nombre = newRolModulo.Nombre;
                    rol.Estado = true;
                }
                else
                {
                    newRol.Estado = true;
                    context.Rol.Add(newRol);
                }
                */
                context.SaveChanges();

                return new GenericResponse<bool>() { Code = 200, Result = true };
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return new GenericResponse<bool>() { Code = 500, Error = ex.Message };
            }
        }
        public IList<BD.Models.RolModulo> GetAll(int page, string FilterNomRol, string Order, string ColumnOrder)
        {
            try
            {

                ICollection<BD.Models.RolModulo> rolmodulos = GetRolFilter(FilterNomRol, Order, ColumnOrder);

                if (page > 1)
                    rolmodulos = rolmodulos.Skip(cantidadElementosPorPagina * (page - 1)).ToList();

                if (page > 0)
                    rolmodulos = rolmodulos.Take(cantidadElementosPorPagina).ToList();

                return rolmodulos.ToList();

            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public ICollection<VMRolModulo> GetAll()
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                ICollection<VMRolModulo> roles = VMRolModulo.MapList(context.RolModulo.ToList(), con);
                return roles;
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public int GetCountPages(int page, string FilterNomRol, string Order, string ColumnOrder)
        {
            var resp = GetRolFilter(FilterNomRol, Order, ColumnOrder).Count();
            int rest = (resp % cantidadElementosPorPagina) > 0 ? 1 : 0;
            int pages = (resp / cantidadElementosPorPagina) + rest;
            return pages;
        }
        public List<BD.Models.RolModulo> GetRolFilter(string FilterNomRol, string Order, string ColumnOrder)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);

                var query = context.RolModulo.ToList();
                /*
                var join = 

                var query = context.RolModulo.Where(x=> 

                if (!string.IsNullOrEmpty(FilterNomRol))
                    query = query.Where(x => );
                    */
             
                return query.ToList();
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
        public BD.Models.RolModulo GetRolModuloById(int IdRolModulo)
        {
            try
            {
                OrsnaDatabaseContext db = new OrsnaDatabaseContext(con);
                return db.RolModulo.Find(IdRolModulo);
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }
 
        public GenericResponse<bool> Delete(int IdRolModulo)
        {
            try
            {
                var db = new OrsnaDatabaseContext(con);
                var change = db.RolModulo.FirstOrDefault(x => x.Id == IdRolModulo);
                db.Remove (change);
                db.SaveChanges();

                return new GenericResponse<bool>() { Code = 200, Result = true };
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return new GenericResponse<bool>() { Code = 500, Error = ex.Message };
            }
        }
        public IList<BD.Models.RolModulo> GetByIdRol(int idRol)
        {
            try
            {
                OrsnaDatabaseContext context = new OrsnaDatabaseContext(con);
                var query = context.RolModulo.Where(x => x.IdRol == idRol);
                return query.ToList();
            }
            catch (Exception ex)
            {
                Utils.manageExceptionContext(ex);
                return null;
            }
        }

    }
}
