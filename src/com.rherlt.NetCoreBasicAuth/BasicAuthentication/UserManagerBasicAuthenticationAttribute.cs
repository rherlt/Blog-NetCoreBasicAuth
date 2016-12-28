using System;
using System.Net;
using System.Reflection;
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
        /// <param name="identityUserType">The concrete IdentityUser type to use as a generic UserManager, e.g. typeof(ApplicationUser)</param>
        public UserManagerBasicAuthenticationAttribute(Type identityUserType)
        {
            if (!typeof(IdentityUser).GetTypeInfo().IsAssignableFrom(identityUserType))
                throw new ArgumentException($"The argument {identityUserType} has to be a subclass of Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUser!");

            IdentityUserType = identityUserType;
            //Create a concrete instance of generic class UserManager<IdentityUser>
            UserManagerType = typeof(UserManager<>).MakeGenericType(IdentityUserType);
        }

        public Type IdentityUserType { get; protected set; }

        public Type UserManagerType { get; protected set; }

        protected override bool Authorize(ActionExecutingContext actionExecutingContext, NetworkCredential credential)
        {
            var isValid = false;

            dynamic userManager = actionExecutingContext.HttpContext.RequestServices.GetService(UserManagerType);
            var user = userManager?.FindByNameAsync(credential.UserName).Result;
            if (user != null)
                isValid = userManager.CheckPasswordAsync(user, credential.Password).Result;

            return isValid;
        }
    }
}
