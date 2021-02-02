using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyCourse.Models.Exceptions;

namespace MyCourse.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index(){

            // Ottenere informazioni sulle eccezioni
            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            // feature può ottenere Error e/o Path, info dell'errore.
            switch (feature.Error)
            {
                // Nel caso in cui l'eccezione sia InvalidOperationException 
                case CourseNotFoundException exc:
                    ViewData["Title"] = "Corso non trovato";
                    Response.StatusCode = 404;
                    return View("CourseNotFound");

                default:
                    ViewData["Title"] = "Errore";
                    return View();
            }
        }
    }
}