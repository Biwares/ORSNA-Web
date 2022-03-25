using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BD.ViewModels;
using BD.Models;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Extensions.Configuration;
using Orsna.Helpers;
using BL.Rol;

namespace Orsna.Controllers
{
    public class RolController : BaseController
    {
        public RolController(IConfiguration iConfig) : base(iConfig)
        {
            configuration = iConfig;
        }
        [HttpGet]
        public IActionResult Get()
        {
            BLRol BL = new BLRol(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            ICollection<VMRol> getAll = BL.GetAll();
            return Json(new ResultDto<VMRol>("success", getAll));
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

                Rol data = (JsonConvert.DeserializeObject<Rol>(reader.ReadToEnd()));

                BLRol  bl = new BLRol (configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
                GenericResponse<bool> response = bl.Save(data);
                return Json(response);
            }
        }
        [HttpGet("[action]")]
        public JsonResult GetAll(int page, string FilterNombre, string Order, string ColumnOrder)
        {
            BLRol  bl = new BLRol (configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            IList<Rol> data = bl.GetAll(page, FilterNombre, Order, ColumnOrder);
            return Json(data);
        }
        [HttpGet("[action]")]
        public int GetCountPages(int page, string FilterNombre, string Order, string ColumnOrder)
        {
            BLRol bl = new BLRol(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return bl.GetCountPages(page, FilterNombre, Order, ColumnOrder);
        }
        [HttpGet("[action]")]
        public int GetCountFilterElements(int page, string FilterNombre, string Order, string ColumnOrder)
        {
            BLRol bl = new BLRol(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return bl.GetCountFilterElements(page, FilterNombre, Order, ColumnOrder);
        }
        [HttpGet("[action]")]
        public JsonResult GetRolById(int idRol)
        {
            BLRol bl = new BLRol(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return Json(bl.GetRolById(idRol));
        }
        [HttpDelete("[action]")]
        public JsonResult Delete(int id)
        {
            BLRol bl = new BLRol(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return Json(bl.Delete(id));
        }

        [HttpGet("[action]")]
        public JsonResult GetModulos(int idRol)
        {
            BLRol bl = new BLRol(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return Json(bl.GetModulos(idRol));
        }
    }
}