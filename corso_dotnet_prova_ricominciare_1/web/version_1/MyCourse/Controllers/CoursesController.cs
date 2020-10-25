using Microsoft.AspNetCore.Mvc;

namespace MyCourse.Controllers
{
    public class CoursesController : Controller
    {

        public IActionResult index(){
            return Content("Controller");
        }

        public IActionResult Detail(string id){
            return Content($"Detail corso {id}");
        }

    }
}
