using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<IActionResult> index(){
            /**
            * Fortemente accoppiato
            * CourseService corsoServizio = new CourseService();
            * ViewData["title"] = "Catalogo dei corsi";
            */
            ViewBag.title = "Catalogo dei corsi";
            List<CourseViewModel> corsi = await corsoServizio.GetCoursesAsync();
            return View(corsi);
        }

        public async Task<IActionResult> Detail(int id){
            //var corsoServizio = new CourseService();
            CourseDetailViewModel detailViewModel = await corsoServizio.GetCourseAsync(id);
            ViewBag.title = detailViewModel.Title;
            // Se non passo l'oggetto alla view, lancia NullReferenceException
            return View(detailViewModel);
        }

    }
}
