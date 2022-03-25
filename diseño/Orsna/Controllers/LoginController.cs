using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BL.Seguridad;
using BD.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using Orsna.Helpers;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Orsna.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private IConfiguration configuration;
        public LoginController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }
        [HttpPost("[action]")]
        public JsonResult Do()
        {
            //return mis;
            using (var mem = new MemoryStream())
            using (var reader = new StreamReader(mem))
            {
                Request.Body.CopyTo(mem);

                var body = reader.ReadToEnd();

                mem.Seek(0, SeekOrigin.Begin);

                Login data = (JsonConvert.DeserializeObject<Login>(reader.ReadToEnd()));

                BLSeguridad seguridad = new BLSeguridad(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));

                GenericResponse<LoginResponse> login = seguridad.CheckUserPass(data.user, data.password);
                if (login.Result.Estado == true)
                {
                    HttpContext.Session.SetString("usuario_userid", login.Result.IdUsuario.ToString());
                    HttpContext.Session.SetString("user", login.Result.username);
                }
                return Json(login);
            }
        }

        [HttpPost("[action]")]
        public JsonResult Navigate(string user, string id)
        {
            using (var mem = new MemoryStream())
            using (var reader = new StreamReader(mem))
            {
                Login data = new Login();
                var autHeader = (Request.Headers["Authentication"].ToString()).Split(';');
                if (autHeader.Length > 0)
                {
                    data.id = autHeader[0];
                    data.user = autHeader[1];
                }
                else
                {
                    Request.Body.CopyTo(mem);
                    var header = Request.Headers;

                    var body = reader.ReadToEnd();

                    mem.Seek(0, SeekOrigin.Begin);

                    data = (JsonConvert.DeserializeObject<Login>(reader.ReadToEnd()));
                }
                //TODO: validar la funcion llamando la clase desde otros controladores, para los request de la api
                Validate(data.user, data.id);

                return Json("True");
            }
        }
        public void Validate(string u, string i)
        {
            try
            {
                var id = HttpContext.Session.GetString("usuario_userid").ToString().Trim();
                var user = HttpContext.Session.GetString("user").ToString().Trim();

                if (u != user || i != id)
                    throw new InvalidOperationException("_401");
            }
            catch (Exception)
            {
                throw new InvalidOperationException("401");
            }
        }
    }
}
