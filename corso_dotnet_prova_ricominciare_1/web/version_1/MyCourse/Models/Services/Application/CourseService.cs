using System;
using System.Collections.Generic;
using MyCourse.Models.Enums;
using MyCourse.Models.ValueTypes;
using MyCourse.Models.ViewModels;

namespace MyCourse.Models.Services.Application
{
    public class CourseService
    {
        public List<CourseViewModel> getCourse()
        {
            var courseList = new List<CourseViewModel>();
            var rand = new Random();
            for (int i = 0; i < 20; i++)
            {
                var price = Convert.ToDecimal(rand.NextDouble() * 10 + 10);
                var course = new CourseViewModel
                {
                    Id = i,
                    Title = $"Corso {i}",
                    FullPrice = new Money(Currency.EUR, price),
                    CurrentPrice = new Money(Currency.EUR, rand.NextDouble() > 0.5 ? price : price - 1),
                    Rating = (double)Decimal.Round((decimal)(rand.NextDouble() * 5.0), 1),
                    Author = "Name Surname",
                    ImagePath = "~/img/bug.png"
                };
                courseList.Add(course);
            }
            return courseList;
        }
    }
}