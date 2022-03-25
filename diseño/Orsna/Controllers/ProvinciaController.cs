using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BL.Proyecto;
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
    public class ProvinciaController : Controller
    {
        private IConfiguration configuration;
        public ProvinciaController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }
        [HttpGet]
        public IActionResult Get()
        {
            BLProvincia BL = new BLProvincia(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            ICollection<VMProvincia> getAll = BL.GetAll();
            return Json(new ResultDto<VMProvincia>("success", getAll));
        }
    }
}