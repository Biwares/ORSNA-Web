using BL.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Configuration;
using Orsna.Helpers;

namespace Orsna.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BaseController : Controller
    {
        protected string userId = string.Empty;
        protected IConfiguration configuration;

        public BaseController(IConfiguration iConfig) : base()
        {
            configuration = iConfig;
        }

        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            base.OnActionExecuting(ctx);
            StringValues authtoken;
            HttpContext.Request.Headers.TryGetValue("Authorization", out authtoken);
            if (authtoken.Count > 0)
            {
                var authInfo = BasicAuthenticationHandler.getUserNameAndPasswordFromHeaders(authtoken.ToString());
                BLUsuario userService = new BLUsuario(configuration.GetValue<string>("MyConfig:OrsnaDatabaseEntities"));
                userId = authInfo.Item1;
            }
        }

    }
}