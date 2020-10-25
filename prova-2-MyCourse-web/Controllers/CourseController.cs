using Microsoft.AspNetCore.Mvc;

namespace MyCourse.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult index(){
            return View();
        }
 
        public IActionResult Detail(string id){
            //return Content($"Sono Detail. Ho ricevuto il corso con id {id}.");
            return View();
        }
    }
}