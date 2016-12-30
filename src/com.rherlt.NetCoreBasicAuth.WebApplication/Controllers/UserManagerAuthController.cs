using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using com.rherlt.NetCoreBasicAuth.BasicAuthentication;
using com.rherlt.NetCoreBasicAuth.WebApplication.Models;
using Microsoft.AspNetCore.Identity;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace com.rherlt.NetCoreBasicAuth.WebApplication.Controllers
{
    [Route("api/[controller]")]
    public class UserManagerAuthController : Controller
    {
        // GET: api/UserManagerAuth
        [HttpGet]
        [UserManagerBasicAuthentication(typeof(ApplicationUser))]
        public string Get()
        {
            return $"Authorized User:{HttpContext.User.Identity.Name}";
        }
    }
}
