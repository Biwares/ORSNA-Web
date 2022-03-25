using BD.Models;
using BD.ViewModels;
using BL.Area;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Orsna.Helpers;
using System;
using System.Collections.Generic;
using System.IO;

namespace Orsna.Controllers
{
    public class CuentaController : BaseController
    {
        public CuentaController(IConfiguration iConfig) : base(iConfig)
        {
            configuration = iConfig;
        }
        [HttpGet]
        public IActionResult Get()
        {
            BLCuenta BL = new BLCuenta(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
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
                BLCuenta bl = new BLCuenta(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
                GenericResponse<bool> response = bl.Save(data);
                return Json(response);
            }
        }
        [HttpGet("[action]")]
        public JsonResult GetAll(string page, string FilterNroCuenta, string FilterNombre, string FilterTipoLibranza, string FilterGrupoAeropuerto, string Order, string ColumnOrder)
        {
            BLCuenta bl = new BLCuenta(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            if (string.IsNullOrEmpty(FilterTipoLibranza))
                FilterTipoLibranza = "0";
            if (string.IsNullOrEmpty(FilterGrupoAeropuerto))
                FilterGrupoAeropuerto = "0";
            ICollection<VMCuenta> data = bl.GetAll(string.IsNullOrEmpty(page) ? 0 :  int.Parse(page), FilterNroCuenta, FilterNombre, int.Parse(FilterTipoLibranza), int.Parse(FilterGrupoAeropuerto), Order, ColumnOrder);
            return Json(new ResultDto<VMCuenta>("success", data));
            //return Json(data);
        }
        [HttpGet("[action]")]
        public int GetCountPages(string page, string FilterNroCuenta, string FilterNombre, string FilterTipoLibranza, string FilterGrupoAeropuerto, string Order, string ColumnOrder)
        {
            if (string.IsNullOrEmpty(FilterTipoLibranza))
                FilterTipoLibranza = "0";
            if (string.IsNullOrEmpty(FilterGrupoAeropuerto))
                FilterGrupoAeropuerto = "0";
            BLCuenta bl = new BLCuenta(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return bl.GetCountPages(int.Parse(page), FilterNroCuenta, FilterNombre, int.Parse(FilterTipoLibranza), int.Parse(FilterGrupoAeropuerto), Order, ColumnOrder);
        }
        [HttpGet("[action]")]
        public int GetCountFilterElements(string page, string FilterNroCuenta, string FilterNombre, string FilterTipoLibranza, string FilterGrupoAeropuerto, string Order, string ColumnOrder)
        {
            if (string.IsNullOrEmpty(FilterTipoLibranza))
                FilterTipoLibranza = "0";
            if (string.IsNullOrEmpty(FilterGrupoAeropuerto))
                FilterGrupoAeropuerto = "0";
            BLCuenta bl = new BLCuenta(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return bl.GetCountFilterElements(int.Parse(page), FilterNroCuenta, FilterNombre, int.Parse(FilterTipoLibranza), int.Parse(FilterGrupoAeropuerto), Order, ColumnOrder);
        }
        [HttpGet("[action]")]
        public JsonResult GetCuentaById(int idCuenta)
        {
            BLCuenta bl = new BLCuenta(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return Json(bl.GetCuentaById(idCuenta));
        }
        [HttpGet("[action]")]
        public IActionResult GetAerosToCuenta(int idCuenta)
        {
            BLCuenta bl = new BLCuenta(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return Ok(bl.GetAerosToCuenta(idCuenta));
        }
        [HttpDelete("[action]")]
        public JsonResult Delete(int id)
        {
            BLCuenta bl = new BLCuenta(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"), userId);
            return Json(bl.Delete(id));
        }
    }
}