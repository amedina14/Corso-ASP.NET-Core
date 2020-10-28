using System;
using System.Collections.Generic;
using System.Linq;

namespace MyCourse.Models.ViewModels
{
    public class CourseDetailViewModel : CourseViewModel{
        public string Description { get; set; }
        public List<CourseLessonViewModel> Lezioni { get; set; }
        public TimeSpan TotalCourseDuration { 
            get => TimeSpan.FromSeconds(Lezioni?.Sum(l => l.Duration.TotalSeconds) ?? 0);
        }
    }
}