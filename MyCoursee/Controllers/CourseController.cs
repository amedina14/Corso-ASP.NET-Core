using Microsoft.AspNetCore.Mvc;
//using MyCourse.Models.Services.Application;

namespace MyCoursee.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult index(){
            var corsiServizi = new CourseService();
            return View();
        }
 
        public IActionResult Detail(string id){
            //return Content($"Sono Detail. Ho ricevuto il corso con id {id}.");
            return View();
        }
    }
}