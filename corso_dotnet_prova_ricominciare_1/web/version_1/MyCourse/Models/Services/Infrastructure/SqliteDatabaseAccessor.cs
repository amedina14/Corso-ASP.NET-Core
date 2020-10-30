using System;
using System.Data;
using MyCourse.Models.Services.Application;

namespace MyCourse.Models.Services.Infrastructure{

    public class SqliteDatabaseAccessor : IDatabaseAccessor{

        public DataSet Query(string query){
            throw new NotImplementedException();            
        }
    }
}