using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using NoMembershipProviderSample.Models;

namespace NoMembershipProviderSample
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            var ctx = HttpContext.Current;
            if (ctx.Request.IsAuthenticated)
            {
                string[] roles = LookupRolesForUser(ctx.User.Identity.Name);
                var newUser = new GenericPrincipal(ctx.User.Identity, roles);
                ctx.User = Thread.CurrentPrincipal = newUser;
            }
        }

        private string[] LookupRolesForUser(string name)
        {
            var repo = new AccountRepository(); // In the real world, you would probably use service locator pattern and call DependencyResolver here
            var user = repo.FindByName(name);
            if (user != null)
            {
                return user.Roles;
            }

            return new string[0];  // Alternatively throw an exception
        }
    }
}