using System;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace com.rherlt.NetCoreBasicAuth.BasicAuthentication
{
    public class UserManagerBasicAuthenticationAttribute : BasicAuthenticationBaseAttribute
    {
        /// <summary>
        /// Authorzies HTTP requests against UserManager
        /// </summary>
        /// <param name="userManagerWithGenericIdentityUser">The concrete UserManager Type, e.g. typeof(UserManager&lt;ApplicationUser&gt;)</param>
        public UserManagerBasicAuthenticationAttribute(Type userManagerWithGenericIdentityUser)
        {
            UserManagerWithGenericIdentityUser = userManagerWithGenericIdentityUser;
        }

        public Type UserManagerWithGenericIdentityUser { get; protected set; }

        protected override bool Authorize(ActionExecutingContext actionExecutingContext, NetworkCredential credential)
        {
            var isValid = false;

            dynamic userManager = actionExecutingContext.HttpContext.RequestServices.GetService(UserManagerWithGenericIdentityUser);
            var user = userManager?.FindByNameAsync(credential.UserName).Result;
            if (user != null)
                isValid = userManager.CheckPasswordAsync(user, credential.Password).Result;

            return isValid;
        }
    }
}
