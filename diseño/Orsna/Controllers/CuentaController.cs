using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BL.Area;
using BL.Proyecto;
using BD.ViewModels;
using BD.Models;
using Newtonsoft.Json;
using System.IO;
using Orsna.Helpers;
using Microsoft.Extensions.Configuration;

namespace Orsna.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : Controller
    {
        private IConfiguration configuration;
        public CuentaController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }
        [HttpGet]
        public IActionResult Get()
        {
            BLCuenta BL = new BLCuenta(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            ICollection<VMCuenta> getAll = BL.GetAll();
            return Json(new ResultDto<VMCuenta>("success", getAll));
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

                Cuentas data = (JsonConvert.DeserializeObject<Cuentas>(reader.ReadToEnd()));
                data.FechaCreacion = DateTime.Now;
                BLCuenta bl = new BLCuenta(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
                GenericResponse<bool> response = bl.Save(data);
                return Json(response);
            }
        }
        [HttpGet("[action]")]
        public JsonResult GetAll(string page, string FilterNroCuenta, string FilterNombre, string FilterTipoLibranza, string FilterGrupoAeropuerto, string Order, string ColumnOrder)
        {
            BLCuenta bl = new BLCuenta(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            if (string.IsNullOrEmpty(FilterTipoLibranza))
                FilterTipoLibranza = "0";
            if (string.IsNullOrEmpty(FilterGrupoAeropuerto))
                FilterGrupoAeropuerto = "0";
            ICollection<VMCuenta> data = bl.GetAll( int.Parse(page), FilterNroCuenta, FilterNombre, int.Parse(FilterTipoLibranza), int.Parse(FilterGrupoAeropuerto), Order, ColumnOrder);
            return Json(data);
        }
        [HttpGet("[action]")]
        public int GetCountPages(string page, string FilterNroCuenta, string FilterNombre, string FilterTipoLibranza, string FilterGrupoAeropuerto, string Order, string ColumnOrder)
        {
            if (string.IsNullOrEmpty(FilterTipoLibranza))
                FilterTipoLibranza = "0";
            if (string.IsNullOrEmpty(FilterGrupoAeropuerto))
                FilterGrupoAeropuerto = "0";
            BLCuenta bl = new BLCuenta(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            return bl.GetCountPages(int.Parse(page), FilterNroCuenta, FilterNombre, int.Parse(FilterTipoLibranza), int.Parse(FilterGrupoAeropuerto), Order, ColumnOrder);
        }
        [HttpGet("[action]")]
        public JsonResult GetCuentaById(int idCuenta)
        {
            BLCuenta bl = new BLCuenta(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            return Json(bl.GetCuentaById(idCuenta));
        }
        [HttpGet("[action]")]
        public IActionResult GetAerosToCuenta(int idCuenta)
        {
            BLCuenta bl = new BLCuenta(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            return Ok(bl.GetAerosToCuenta(idCuenta));
        }
        [HttpDelete("[action]")]
        public JsonResult Delete(int id)
        {
            BLCuenta bl = new BLCuenta(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            return Json(bl.Delete(id));
        }
    }
}