using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MyCourse.Models.Services.Application;
using MyCourse.Models.ViewModels;

namespace MyCourse.Controllers
{
    public class CoursesController : Controller
    {

        // Debolmente accopiato: DI
        private readonly ICourseService corsoServizio;

        public CoursesController(ICourseService corsoServizio){
            this.corsoServizio = corsoServizio;
        }

        public IActionResult index(){
            /**
            * Fortemente accoppiato
            * CourseService corsoServizio = new CourseService();
            * ViewData["title"] = "Catalogo dei corsi";
            */
            ViewBag.title = "Catalogo dei corsi";
            List<CourseViewModel> corsi = corsoServizio.GetCourses();
            return View(corsi);
        }

        public IActionResult Detail(int id){
            //var corsoServizio = new CourseService();
            CourseDetailViewModel detailViewModel = corsoServizio.GetCourse(id);
            ViewBag.title = detailViewModel.Title;
            // Se non passo l'oggetto alla view, lancia NullReferenceException
            return View(detailViewModel);
        }

    }
}
