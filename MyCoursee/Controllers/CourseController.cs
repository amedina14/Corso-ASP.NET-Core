using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MyCoursee.Models.ViewModels;
using MyCoursee.Models.Services.Application;

namespace MyCoursee.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult index(){
            var corsiServizi = new CourseService();
            List<CourseViewModel> corsi = corsiServizi.GetServices();
            return View(corsi);
        }
 
        public IActionResult Detail(string id){
            //return Content($"Sono Detail. Ho ricevuto il corso con id {id}.");
            return View();
        }
    }
}