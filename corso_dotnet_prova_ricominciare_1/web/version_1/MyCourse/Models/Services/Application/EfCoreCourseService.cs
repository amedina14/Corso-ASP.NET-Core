using System.Collections.Generic;
using System.Threading.Tasks;
using MyCourse.Models.Infrastructure;
using MyCourse.Models.ViewModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MyCourse.Models.Services.Application
{
    public class EfCoreCourseService: ICourseService {

        private readonly MyCourseDbContext dbContext;
        public EfCoreCourseService(MyCourseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<CourseViewModel>> GetCoursesAsync()
        {
            // Proiezione: a fronte di un entita course, ottenere istanze del CourseViewModel (select)
            /*
                IQueryable:
                Crea l'albero di espressioni per aiutare il provider LINQ sqlite a tradurre in SQL.
                Interroga al DB prima di ottenere i risultati da esso.
            */
            IQueryable<CourseViewModel> queryLinq = dbContext.Courses.Select(course => 
            new CourseViewModel 
            {
                /*
                    Se incapsuliamo le propieta in un metodo (FromEntity()), la query sarà inefficienti,
                    Perchè prenderà tutti i campi e non riuscirà ad entrare al metodo.
                */
                Id = course.Id,
                Title = course.Title,
                ImagePath = course.ImagePath,
                Author = course.Author,
                Rating = course.Rating,
                CurrentPrice = course.CurrentPrice,
                FullPrice = course.FullPrice
            });

            // Deferred excecution - Esecuzione differita
            List<CourseViewModel> courses = await queryLinq.ToListAsync(); //Qui viene inviata la query al database, quando manifestiamo l'intenzione di voler leggere i risultati

            return courses;
            // throw new System.NotImplementedException();
        }

        public async Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
            CourseDetailViewModel viewModel = await dbContext.Courses
            /*
                Eager loading:
                La query non legge le lezioni del corso dal metodo, quindi
                si interrogano le lezioni a parte con un altro method extension.
                .Include(course => course.Lessons)
            */
            .Where(course => course.Id == id)
            .Select(course => new CourseDetailViewModel{
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Author = course.Author,
                ImagePath = course.ImagePath,
                Rating = course.Rating,
                CurrentPrice = course.CurrentPrice,
                FullPrice = course.FullPrice,
                Lessons = course.Lessons.Select(lesson => new CourseLessonViewModel {
                    Id = lesson.Id,
                    TitleLesson = lesson.Title,
                    Duration = lesson.Duration
                }).ToList()
            })
            /*            
            .FirstOrDefaultAsync() // return primo element o il valore default della proprieta/elemento 
            .SingleOrDefaultAsync() // return primo elemento o il valore default della proprieta/elemento, se elemento > 1, exception.
            .FirstAsync() // ritorna primo elemento lista, se elemento > 1, exception, se il valore della proprieta/elemento è default, exception.
            */
            .SingleAsync(); // ritorna primo elemento, no ripetuto, no default.

            return viewModel;
            // throw new System.NotImplementedException();
        }    }
}