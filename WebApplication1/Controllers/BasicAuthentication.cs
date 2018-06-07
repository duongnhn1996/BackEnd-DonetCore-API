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

namespace EmailWeb.Controllers
{
    public class BasicAuthentication : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            } 
            else
            {
                string authenticationToke = actionContext.Request.Headers.Authorization.Parameter;
                // decode base64
               string decodedAuthenticationToken =  Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToke));
                string[] usernamePasswordArray=decodedAuthenticationToken.Split(':'); // username:password
                string username = usernamePasswordArray[0];
                string password = usernamePasswordArray[1];
                if (UserSecurity.Login(username, password))
                {
                    // return true
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);

                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}
