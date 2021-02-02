using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyCourse.Models.Exceptions;
using MyCourse.Models.Options;
using MyCourse.Models.Services.Infrastructure;
using MyCourse.Models.ViewModels;

namespace MyCourse.Models.Services.Application
{
    public class MemoryCacheCourseService : ICachedCourseService
    {
        private readonly IOptionsMonitor<TempoDiCachingOptions> tempoDiCachingOptions;
        private readonly ICourseService courseService;
        private readonly IMemoryCache memoryCache;


        // Classe che riceve 2 servizi: ICourseService che ottiene gli oggtt dal DB, IMemoryCache invece li ottiene dalla Cache 
        public MemoryCacheCourseService(IOptionsMonitor<TempoDiCachingOptions> tempoDiCachingOptions, ICourseService courseService, IMemoryCache memoryCache)
        {
            this.tempoDiCachingOptions = tempoDiCachingOptions;
            this.courseService = courseService;
            this.memoryCache = memoryCache;
        }

        public Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
            double secondi = tempoDiCachingOptions.CurrentValue.Secondi;
            return memoryCache.GetOrCreateAsync($"Course{id}", cacheEntry => 
            {
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(secondi));
                return courseService.GetCourseAsync(id);
            });
        }

        public Task<List<CourseViewModel>> GetCoursesAsync()
        {
            double secondi = tempoDiCachingOptions.CurrentValue.Secondi;            
            return memoryCache.GetOrCreateAsync($"Courses", cacheEntry => 
            {
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(secondi)); //Esercizio: provate a recuperare il valore 60 usando il servizio di configurazione
                return courseService.GetCoursesAsync();
            });
        }
    }
}