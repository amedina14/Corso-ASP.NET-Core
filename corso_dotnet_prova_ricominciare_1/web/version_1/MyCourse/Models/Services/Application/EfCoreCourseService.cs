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
            List<CourseViewModel> courses = await dbContext.Courses.Select(course => 
            new CourseViewModel 
            {
                Id = course.Id,
                Title = course.Title,
                ImagePath = course.ImagePath,
                Author = course.Author,
                Rating = course.Rating,
                CurrentPrice = course.CurrentPrice,
                FullPrice = course.FullPrice
            })
            .ToListAsync();

            return courses;
            // throw new System.NotImplementedException();
        }

        public async Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
            CourseDetailViewModel viewModel = await dbContext.Courses
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