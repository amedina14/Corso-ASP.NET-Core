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
            string query = "SELECT * FROM COURSES;";
            DataSet dataset = db.Query(query);
            throw new NotImplementedException();
        }

        public CourseDetailViewModel GetCourse(int id)
        {
            throw new NotImplementedException();
        }

    }
}