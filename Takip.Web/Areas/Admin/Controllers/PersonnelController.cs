using Microsoft.AspNetCore.Mvc;
using Takip.Web.Code.Rest;
using Takip.Web.Code;
using Takip.Web.Models;

namespace Takip.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PersonnelController : Controller
    {
        public IActionResult AccountDetails() => View();

        //public IActionResult Edit(AccountEditModel model)
        //{
        //    UserRestClient client = new UserRestClient();
        //    dynamic result = client.Login(model.EMail, model.Password);

        //    bool success = result.success;

        //    if (success)
        //    {
        //        Repo.Session.Email = model.EMail;
        //        Repo.Session.Name = (string)result.name;
        //        Repo.Session.Token = (string)result.data;
        //        Repo.Session.Role = (string)result.role;

        //        ViewBag.Detay = result.data;

        //        return RedirectToAction("Index", "Home", new { area = "Admin" });
        //    }
        //    else
        //    {
        //        ViewBag.LoginError = (string)result.message;
        //        return View("Login");
        //    }
        //}
    }
}
