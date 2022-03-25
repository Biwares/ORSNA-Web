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
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private IConfiguration configuration;
        public UsuarioController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }
        [HttpGet]
        public IActionResult Get()
        {
            BLUsuario BL = new BLUsuario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
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

                BLUsuario BussUsuario = new BLUsuario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
                GenericResponse<bool> response = BussUsuario.Save(data);
                return Json(response);
            }
        }
        [HttpGet("[action]")]
        public JsonResult GetAll(int page, string FilterEmail, string Order, string ColumnOrder)
        {
            BLUsuario BussUsuario = new BLUsuario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            IList<Usuarios> data = BussUsuario.GetAll(page, FilterEmail, Order, ColumnOrder);
            return Json(data);
        }
        [HttpGet("[action]")]
        public int GetCountPages(int page, string FilterEmail, string Order, string ColumnOrder)
        {
            BLUsuario BussUsuario = new BLUsuario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            return BussUsuario.GetCountPages(page, FilterEmail, Order, ColumnOrder);
        }
        [HttpGet("[action]")]
        public JsonResult GetUsuarioById(int idUsuario)
        {
            BLUsuario BussUsuario = new BLUsuario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            return Json(BussUsuario.GetUsuarioById(idUsuario));
        }
        [HttpDelete("[action]")]
        public JsonResult Delete(int id)
        {
            BLUsuario bussUsuario = new BLUsuario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            return Json(bussUsuario.Delete(id));
        }
        /*[HttpGet("[action]")]
        public JsonResult Prueba()
        {
            return Json("200");
        }*/
    }
}