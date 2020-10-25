using Microsoft.AspNetCore.Mvc;

namespace MyCoursee.Controllers
{
    public class HomeController : Controller {
        public IActionResult index(){
            //return Content("Sono la index della Home-Controller");
            return View();
        }
    }
}