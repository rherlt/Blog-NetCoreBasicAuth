using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.rherlt.NetCoreBasicAuth.BasicAuthentication;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace com.rherlt.NetCoreBasicAuth.WebApplication.Controllers
{
    [Route("api/[controller]")]
    [BasicAuthentication("rherlt", "passw0rd123")]
    public class AuthController : Controller
    {
        // GET: api/values
        [HttpGet]
        public string Get()
        {
            return $"Authorized User:{HttpContext.User.Identity.Name}";
        }
    }
}
