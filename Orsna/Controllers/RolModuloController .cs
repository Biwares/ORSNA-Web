using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BL.RolModulo;
using BD.ViewModels;
using BD.Models;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Extensions.Configuration;
using Orsna.Helpers;

namespace Orsna.Controllers
{
    public class RolModuloController : BaseController
    {
        public RolModuloController(IConfiguration iConfig) : base(iConfig)
        {
            configuration = iConfig;
        }
        [HttpGet]
        public IActionResult Get()
        {
            BLRolModulo BL = new BLRolModulo(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            ICollection<VMRolModulo> getAll = BL.GetAll();
            return Json(new ResultDto<VMRolModulo>("success", getAll));
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

                RolModulo data = (JsonConvert.DeserializeObject<RolModulo>(reader.ReadToEnd()));

                BLRolModulo bl = new BLRolModulo(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
                GenericResponse<bool> response = bl.Save(data);
                return Json(response);
            }
        }
        [HttpGet("[action]")]
        public JsonResult GetAll(int page, string FilterNombre, string Order, string ColumnOrder)
        {
            BLRolModulo bl = new BLRolModulo(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            IList<RolModulo> data = bl.GetAll(page, FilterNombre, Order, ColumnOrder);
            return Json(data);
        }
        [HttpGet("[action]")]
        public int GetCountPages(int page, string FilterNombre, string Order, string ColumnOrder)
        {
            BLRolModulo bl = new BLRolModulo(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return bl.GetCountPages(page, FilterNombre, Order, ColumnOrder);
        }
        [HttpGet("[action]")]
        public JsonResult GetRolModuloById(int idRolModulo)
        {
            BLRolModulo bl = new BLRolModulo(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return Json(bl.GetRolModuloById(idRolModulo));
        }
        [HttpDelete("[action]")]
        public JsonResult Delete(int id)
        {
            BLRolModulo bl = new BLRolModulo(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return Json(bl.Delete(id));
        }
    }
}