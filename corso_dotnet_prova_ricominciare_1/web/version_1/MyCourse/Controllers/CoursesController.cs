using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MyCourse.Models.Services.Application;
using MyCourse.Models.ViewModels;

namespace MyCourse.Controllers
{
    public class CoursesController : Controller
    {

        public IActionResult index(){
            CourseService corsoServizio = new CourseService();
            List<CourseViewModel> corsi = corsoServizio.getCourses();
            return View(corsi);
        }

        public IActionResult Detail(int id){
            var corsoServizio = new CourseService();
            CourseDetailViewModel detailViewModel = corsoServizio.GetCourse(id);
            return View(detailViewModel);
            // Se non passo l'oggetto alla view, lancia NullReferenceException
        }

    }
}
