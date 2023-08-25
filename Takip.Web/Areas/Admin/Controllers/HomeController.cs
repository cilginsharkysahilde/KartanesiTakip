using Microsoft.AspNetCore.Mvc;
using Takip.Web.Code.Filters;
using Takip.Web.Code.Rest;

namespace Takip.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            IndexRestClient client = new IndexRestClient();
            dynamic result1 = client.PersonnelCount();

            bool success1 = result1.success;
            if (success1)
                ViewBag.PersonnelCount = result1.data;

            dynamic result2 = client.ServiceCount();

            bool success2 = result2.success;
            if (success2)
                ViewBag.ServiceCount = result2.data;

            dynamic result3 = client.ProductCount();

            bool success3 = result3.success;
            if (success3)
                ViewBag.ProductCount = result3.data;

            dynamic result4 = client.CustomerCount();

            bool success4 = result4.success;
            if (success4)
                ViewBag.CustomerCount = result4.data;
            return View();
        }
        [AuthActionFilter(Role = "Admin")]
        public IActionResult Role() => View();
        [AuthActionFilter(Role = "Admin")]
        public IActionResult Category() => View();
        [AuthActionFilter]
        public IActionResult Customer() => View();
        [AuthActionFilter]
        public IActionResult Service() => View();
        [AuthActionFilter(Role = "Admin")]
        public IActionResult Product() => View();
        [AuthActionFilter(Role = "Admin")]
        public IActionResult Personnel() => View();

    }
}
