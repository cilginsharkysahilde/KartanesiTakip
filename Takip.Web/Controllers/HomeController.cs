using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Takip.Web.Models;
using Takip.Web.Code.Filters;

namespace Takip.Web.Controllers
{
    [AuthActionFilter]
    public class HomeController : Controller
    {

        public HomeController() { }

        public IActionResult Index() => RedirectToAction("Login", "Account");

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}