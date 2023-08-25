using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Takip.Web.Code;
using Takip.Web.Code.Rest;
using Takip.Web.Models;

namespace Takip.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login() => View();

        public IActionResult Unauthorized() => View();

        public IActionResult GirisYap(LoginModel model)
        {
            UserRestClient client = new UserRestClient();
            dynamic result = client.Login(model.EMail, model.Password);

            bool success = result.success;

            if (success)
            {
                Repo.Session.Email = model.EMail;
                Repo.Session.Name = (string)result.name;
                Repo.Session.Token = (string)result.data;
                Repo.Session.Role = (string)result.role;

                return RedirectToAction("Index", "Home", new { area = "Admin"});
            }
            else
            {
                ViewBag.LoginError = (string)result.message;
                return View("Login");
            }

            // View -> Controller -> UserRestClient -> RestCharp ile webapi'ye bağlanıyor -> Database
        }

        public IActionResult Logout()
        {
            Repo.Session.Email = "";
            Repo.Session.Name = "";
            Repo.Session.Token = "";
            Repo.Session.Role = "";

            return RedirectToAction("Login", "Account");
        }
    }
}
