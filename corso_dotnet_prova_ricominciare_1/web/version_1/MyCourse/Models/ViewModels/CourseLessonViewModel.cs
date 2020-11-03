using System;
using System.Data;

namespace MyCourse.Models.ViewModels
{
    public class CourseLessonViewModel
    {
        public int Id { get; set; }
        public string TitleLesson { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }

        public static CourseLessonViewModel FromDataRow(DataRow lessonRow)
        {
            var courseLessonViewModel = new CourseLessonViewModel {
                Id = Convert.ToInt32(lessonRow["Id"]),
                TitleLesson = Convert.ToString(lessonRow["Title"]),
                Description = Convert.ToString(lessonRow["Description"]),
                Duration = TimeSpan.Parse(Convert.ToString(lessonRow["Duration"])),
            };

            return courseLessonViewModel;
        }
    }
}