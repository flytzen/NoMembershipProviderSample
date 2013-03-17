using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NoMembershipProviderSample.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Login(string userName)
        {
            FormsAuthentication.SetAuthCookie("flytzen", false);
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult SimpleAuthorization()
        {
            return this.Content("You are authorized");
        }

        [Authorize(Users = "flytzen")]
        public ActionResult AuthorizedAsFlytzen()
        {
            return this.Content("You are authorized as flytzen");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult AuthorizedAsAdministrator()
        {
            return this.Content("You are authorized as an administrator");
        }


    }
}
