using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BL.Usuario;
using BD.ViewModels;
using BD.Models;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Extensions.Configuration;
using Orsna.Helpers;

namespace Orsna.Controllers
{
    public class UsuarioController : BaseController
    {
        public UsuarioController(IConfiguration iConfig) : base(iConfig)
        {
            configuration = iConfig;
        }
        [HttpGet]
        public IActionResult Get()
        {
            BLUsuario BL = new BLUsuario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            ICollection<VMUsuario> getAll = BL.GetAll();
            return Json(new ResultDto<VMUsuario>("success", getAll));
        }
        [HttpPost("[action]")]
        public JsonResult Save()
        {
            using (var mem = new MemoryStream())
            using (var reader = new StreamReader(mem))
            {
                Request.Body.CopyTo(mem);

                var body = reader.ReadToEnd();

                mem.Seek(0, SeekOrigin.Begin);

                Usuarios data = (JsonConvert.DeserializeObject<Usuarios>(reader.ReadToEnd()));

                BLUsuario BussUsuario = new BLUsuario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
                GenericResponse<bool> response = BussUsuario.Save(data);
                return Json(response);
            }
        }
        [HttpGet("[action]")]
        public JsonResult GetAll(int page, string FilterEmail, string Order, string ColumnOrder)
        {
            BLUsuario BussUsuario = new BLUsuario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            IList<Usuarios> data = BussUsuario.GetAll(page, FilterEmail, Order, ColumnOrder);
            return Json(data);
        }
        [HttpGet("[action]")]
        public int GetCountPages(int page, string FilterEmail, string Order, string ColumnOrder)
        {
            BLUsuario BussUsuario = new BLUsuario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return BussUsuario.GetCountPages(page, FilterEmail, Order, ColumnOrder);
        }
        [HttpGet("[action]")]
        public int GetCountFilterElements(int page, string FilterEmail, string Order, string ColumnOrder)
        {
            BLUsuario BussUsuario = new BLUsuario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return BussUsuario.GetCountFilterElements(page, FilterEmail, Order, ColumnOrder);
        }
        [HttpGet("[action]")]
        public JsonResult GetUsuarioById(int idUsuario)
        {
            BLUsuario BussUsuario = new BLUsuario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return Json(BussUsuario.GetUsuarioById(idUsuario));
        }
        [HttpDelete("[action]")]
        public JsonResult Delete(int id)
        {
            BLUsuario bussUsuario = new BLUsuario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return Json(bussUsuario.Delete(id));
        }

        [HttpGet("[action]")]
        public JsonResult GetAreas(int idUsuario)
        {
            BLUsuario bl = new BLUsuario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return Json(bl.GetAreas(idUsuario));
        }

        [HttpPost("[action]")]
        public JsonResult SaveAreas()
        {
            using (var mem = new MemoryStream())
            using (var reader = new StreamReader(mem))
            {
                Request.Body.CopyTo(mem);

                var body = reader.ReadToEnd();

                mem.Seek(0, SeekOrigin.Begin);

                Areas area = (JsonConvert.DeserializeObject<Areas>(reader.ReadToEnd()));
                List<UsuariosAreas> data = new List<UsuariosAreas>();

                BLUsuario  bl = new BLUsuario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
                GenericResponse<bool> response = bl.SaveAreas(data);
                return Json(response);
            }
        }

        [HttpGet("[action]")]
        public JsonResult GetRoles(int idUsuario)
        {
            BLUsuario bl = new BLUsuario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return Json(bl.GetRoles(idUsuario));
        }

        [HttpPost("[action]")]
        public JsonResult SaveRoles()
        {
            using (var mem = new MemoryStream())
            using (var reader = new StreamReader(mem))
            {
                Request.Body.CopyTo(mem);

                var body = reader.ReadToEnd();

                mem.Seek(0, SeekOrigin.Begin);

                Rol rol = (JsonConvert.DeserializeObject<Rol>(reader.ReadToEnd()));
                List<UsuarioRol> data = new List<UsuarioRol>();

                BLUsuario bl = new BLUsuario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
                GenericResponse<bool> response = bl.SaveRoles(data);
                return Json(response);
            }
        }
    }
}