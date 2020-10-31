using System;
using System.Data;
using System.Collections.Generic;
using MyCourse.Models.Enums;
using MyCourse.Models.ValueTypes;
using MyCourse.Models.ViewModels;
using MyCourse.Models.Services.Infrastructure;

namespace MyCourse.Models.Services.Application{
    public class AdoNetCourseService : ICourseService{

        // Implementando D.I del servizio infrastructure sul servizio AdoNetCourseService.
        public readonly IDatabaseAccessor db;

        public AdoNetCourseService(IDatabaseAccessor db)
        {
            this.db = db;
        }

        public List<CourseViewModel> GetCourses()
        {
            // Otteniamo la tabella risultante
            string query = "SELECT Id, Title, ImagePath, Author, Rating, FullPrice_Amount, CurrentPrice_Amount, FullPrice_Currency, CurrentPrice_Currency FROM COURSES;";
            DataSet dataSet = db.Query(query);

            // Ottiene il registro del corso [0]
            var dataTable = dataSet.Tables[0];
            
            // Lista dei corsi dove aggiungerli
            var courseList = new List<CourseViewModel>();
            // Percorriamo tutte le rows del corso
            foreach(DataRow courseRow in dataTable.Rows){
                // Logica di mapping inserita in FromDataRow(courseRow)
                CourseViewModel course = CourseViewModel.FromDataRow(courseRow);
                courseList.Add(course);
            }

            return courseList;
        }

        public CourseDetailViewModel GetCourse(int id)
        {
            throw new NotImplementedException();
        }

    }
}