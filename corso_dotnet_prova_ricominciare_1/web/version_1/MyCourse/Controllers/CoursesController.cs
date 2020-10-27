using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MyCourse.Models.Services.Application;
using MyCourse.ViewModels;

namespace MyCourse.Controllers
{
    public class CoursesController : Controller
    {

        public IActionResult index(){
            CourseService corsoServizio = new CourseService();
            List<CourseViewModel> corsi = corsoServizio.getCourse();
            return View(corsi);
        }

        public IActionResult Detail(string id){
            return View();
        }

    }
}
