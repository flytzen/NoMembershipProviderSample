using System;
using System.Web.Mvc;
using System.Web.Security;
using NoMembershipProviderSample.Models;

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
        public ActionResult Login(string userName, string password, string returnUrl)
        {
            var repo = new AccountRepository();

            var user = repo.FindByName(userName);
            if (user != null && user.ValidatePassword(password))
            {
                FormsAuthentication.SetAuthCookie(userName, false);
                if (returnUrl != null && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("Index");
            }
            
            ModelState.AddModelError("", "Invalid user name or password");
            return View();
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

        [HttpGet]
        public ActionResult CreateUser()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult CreateUser(string userName, string password, string roles)
        {
            var newUser = new Account()
                {
                    UserName = userName,
                    Roles = (String.IsNullOrWhiteSpace(roles) ? new string[0] : roles.Split(','))
                };
            newUser.SetPassword(password);
            var repo = new AccountRepository();
            repo.AddAccount(newUser);
            return RedirectToAction("Index");
        }

    }
}
