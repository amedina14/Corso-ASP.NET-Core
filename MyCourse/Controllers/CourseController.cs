using Microsoft.AspNetCore.Mvc;

namespace MyCourse.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult index(){
            return Content("Ciao, sono l'index!");
        }

        public IActionResult Detail(string id){
            return Content($"Sono Detail. Ho ricevuto il corso con id {id}.");
        }
    }
}