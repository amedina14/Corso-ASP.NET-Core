using System;
using System.Data;
using System.Collections.Generic;
using MyCourse.Models.Enums;
using MyCourse.Models.ValueTypes;
using MyCourse.Models.ViewModels;
using MyCourse.Models.Services.Infrastructure;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MyCourse.Models.Options;
using Microsoft.Extensions.Logging;

namespace MyCourse.Models.Services.Application
{
    public class AdoNetCourseService : ICourseService
    {
        // Crea proprieta logger del servizio di riferimento
        private readonly ILogger<AdoNetCourseService> logger;

        // Implementando D.I del servizio infrastructure sul servizio AdoNetCourseService.
        public readonly IDatabaseAccessor db;
        private readonly IOptionsMonitor<CoursesOptions> coursesOptions;

        public AdoNetCourseService(ILogger<AdoNetCourseService> logger, IDatabaseAccessor db, IOptionsMonitor<CoursesOptions> coursesOptions)
        {
            this.coursesOptions = coursesOptions;
            this.logger = logger;
            this.db = db;
        }

        public async Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
            // Servizio logging strutturato: Impostare parametri di log
            logger.LogInformation("Course {id} requested", id);

            /* La query fornisce 2 tabelle: Corsi e lezioni. */
            FormattableString query = $@"SELECT Id, Title, Description, ImagePath, Author, Rating, FullPrice_Amount, FullPrice_Currency, CurrentPrice_Amount, CurrentPrice_Currency FROM Courses WHERE Id={id}
            ; SELECT Id, Title, Description, Duration FROM Lessons WHERE CourseId={id}";

            DataSet dataSet = await db.QueryAsync(query);

            // Course
            var courseTable = dataSet.Tables[0];
            if (courseTable.Rows.Count != 1)
            {
                throw new InvalidOperationException($"Did not return exactly 1 row for Course {id}");
            }

            // Prende la riga del corso
            var courseRow = courseTable.Rows[0];
            // crea l'oggetto principale della pagina: Dettaglio corso.
            var courseDetailViewModel = CourseDetailViewModel.FromDataRow(courseRow); // Prende dati del corso dal ViewModel

            // Course Lessons
            var lessonsDataTable = dataSet.Tables[1];
            foreach (DataRow lessonRow in lessonsDataTable.Rows)
            {
                CourseLessonViewModel lessonViewModel = CourseLessonViewModel.FromDataRow(lessonRow);
                courseDetailViewModel.Lessons.Add(lessonViewModel);
            }

            return courseDetailViewModel;
        }

        public async Task<List<CourseViewModel>> GetCoursesAsync()
        {
            // Otteniamo la tabella risultante, dal interfaccia IDatabaseAccessor che implementa il metodo Query.
            FormattableString query = $"SELECT Id, Title, ImagePath, Author, Rating, FullPrice_Amount, CurrentPrice_Amount, FullPrice_Currency, CurrentPrice_Currency FROM COURSES;";
            DataSet dataSet = await db.QueryAsync(query);

            // Ottiene il registro del corso [0]
            var dataTable = dataSet.Tables[0];

            // Lista dei corsi dove aggiungerli
            var courseList = new List<CourseViewModel>();
            // Percorriamo tutte le rows del corso
            foreach (DataRow courseRow in dataTable.Rows)
            {
                // Logica di mapping inserita in FromDataRow(courseRow)
                CourseViewModel course = CourseViewModel.FromDataRow(courseRow);
                courseList.Add(course);
            }

            return courseList;
        }

    }
}