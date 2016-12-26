using System;
using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;

namespace com.rherlt.NetCoreBasicAuth.BasicAuthentication
{
    public class BasicAuthenticationAttribute : BasicAuthenticationBaseAttribute
    {
        protected string Username { get; set; }
        protected string Password { get; set; }
     
        public BasicAuthenticationAttribute(string username, string password)
        {
            Username = username;
            Password = password; 
        }

        protected override bool Authorize(ActionExecutingContext actionExecutingContext, NetworkCredential credential)
        {
            return (string.Equals(credential.UserName, Username, StringComparison.OrdinalIgnoreCase) &&
                    credential.Password == Password);
        }
    }
}
