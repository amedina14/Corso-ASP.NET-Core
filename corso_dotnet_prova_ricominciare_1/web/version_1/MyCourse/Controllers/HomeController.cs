using Microsoft.AspNetCore.Mvc;

namespace MyCourse.Controllers
{
    public class HomeController : Controller{
        public IActionResult Index()
        {
            ViewBag.title = "Home";
            return View();
        }

    }
}