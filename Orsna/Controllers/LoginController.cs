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
using ElmahCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using BD.Models;
using BL.Usuario;
using BL;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Orsna.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoginController : Controller
    {
        private IConfiguration configuration;
        public LoginController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }
        [HttpGet("[action]")]
        public JsonResult Logout()
        {
            HttpContext.Session.Clear();
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            Request.Headers["Authentication"] = "";
            return Json("OK");
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]Usuarios userParam)
        {
            BLUsuario userService = new BLUsuario(AppSettingsConfig.Configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
            var user = await userService.Authenticate(userParam.Email, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Usuario o clave incorrecta!" });

            return Ok(user);
        }

    }

}
