using System;
using System.Collections.Generic;
using MyCourse.Models.Enums;
using MyCourse.Models.ValueTypes;
using MyCourse.Models.ViewModels;

namespace MyCourse.Models.Services.Application
{
    public class CourseService
    {
        public List<CourseViewModel> getCourses()
        {
            var courseList = new List<CourseViewModel>();
            var rand = new Random();
            for (int i = 1; i <= 20; i++)
            {
                var price = Convert.ToDecimal(rand.NextDouble() * 10 + 120);
                var course = new CourseViewModel
                {
                    Id = i,
                    Title = $"Corso {i}",
                    FullPrice = new Money(Currency.EUR, price),
                    CurrentPrice = new Money(Currency.EUR, rand.NextDouble() > 0.5 ? price : price - 110),
                    Rating = (double)Decimal.Round((decimal)(rand.NextDouble() * 5.0), 1),
                    Author = "Name Surname",
                    ImagePath = "~/img/bug.png"
                };
                courseList.Add(course);
            }
            return courseList;
        }

        public CourseDetailViewModel GetCourse(int id){
            var rand = new Random();
            var price = Convert.ToDecimal(rand.NextDouble() * 10 + 120);
            var corso = new CourseDetailViewModel(){
                Id = id,
                Title = $"Corso {id}",
                FullPrice = new Money(Currency.EUR, price),
                CurrentPrice = new Money(Currency.EUR, rand.NextDouble() > 0.5 ? price : price - 110),
                Rating = rand.Next(10, 50) / 10.0,
                Description = $"Description {id}",
                Lezioni = new List<CourseLessonViewModel>(),
                Author = "Nome Cognome",
                ImagePath = "~/img/bug.png",
            };
            
            for(var i = 1 ; i <= 5; i++){
                var lezione = new CourseLessonViewModel(){
                    TitleLesson = $"Lezione {i}",
                    Duration = TimeSpan.FromSeconds(rand.Next(40,90))
                };
                corso.Lezioni.Add(lezione);
            }

            return corso;
        }
    }
}