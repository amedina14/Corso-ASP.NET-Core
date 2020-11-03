using System.Collections.Generic;
using System.Threading.Tasks;
using MyCourse.Models.ViewModels;

namespace MyCourse.Models.Services.Application{
    public interface ICourseService
    {
        // In questa classe si implementano i metodi dell'interfaccia.
        
        // Si consiglia scrivere prima la classe interfaccia e poi la sua implementazione concreta.
        // in questa maniera ci si concentra su come deve essere realizzato il servizio.

        // Firma: tipo del metodo, nome del metodo, ed eventualmente i parametri.
        // Modificatore public implicito se non scritto. (Convenzione)
        Task<List<CourseViewModel>> GetCoursesAsync();

        Task<CourseDetailViewModel> GetCourseAsync(int id);
    }
}