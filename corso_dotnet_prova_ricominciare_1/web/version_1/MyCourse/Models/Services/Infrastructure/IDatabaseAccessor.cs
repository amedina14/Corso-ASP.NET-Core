using System;
using System.Collections.Generic;
using System.Data;
using MyCourse.Models.ViewModels;

namespace MyCourse.Models.Services.Infrastructure{
    public interface IDatabaseAccessor
    {
        DataSet Query(FormattableString query);
    }
}