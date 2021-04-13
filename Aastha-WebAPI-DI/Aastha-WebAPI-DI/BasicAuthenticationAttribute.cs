using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Aastha_WebAPI_DI
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if(actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                //to get the authentication token i.e it gets username and password in basic 64 encoding and is separated like username:password
                string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                string decodedStringAuthToekn = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
                string[] usernamePasswordArr = decodedStringAuthToekn.Split(':');
                string username = usernamePasswordArr[0];
                string password = usernamePasswordArr[1];

                if(EmployeeSecurity.Login(username, password) == true)
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
                }
                else
                {
                    actionContext.Response = actionContext.Request
                       .CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}