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

        public Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}