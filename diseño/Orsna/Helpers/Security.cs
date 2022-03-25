using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Orsna.Helpers
{
    public class Security
    {
        public readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public Security(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        

        public void Validate(string u, string i)
        {
            try
            {
                var user = _session.GetString("usuario_userid");
                var id = _session.GetString("id");
                
                if (u!=user || i!=id)
                    throw new InvalidOperationException("_401");
            }
            catch
            {
                throw new InvalidOperationException("401");
            }
        }
    }
}
