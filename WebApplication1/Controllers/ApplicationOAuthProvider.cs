using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading;
using System.Security.Principal;
using Microsoft.Owin.Security.OAuth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EmailWeb.Models;
namespace EmailWeb.Controllers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
       //public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
       // {
       //     context.Validated();

       // }
       // public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
       // {

       //     var user = new UserStore<User>(new webMailContext());

       // }

    }
}
