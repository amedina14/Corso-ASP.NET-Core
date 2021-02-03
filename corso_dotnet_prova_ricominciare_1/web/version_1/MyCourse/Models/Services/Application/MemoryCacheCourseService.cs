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
            // TODO: ricordati di usare memoryCache.Remove($"Course{id}") quando aggiorni il corso (entita del DB)

            double secondi = tempoDiCachingOptions.CurrentValue.Secondi;
            return memoryCache.GetOrCreateAsync($"Course{id}", cacheEntry => 
            {
                /*
                    Al raggiungimento dei mille oggetti, non si aggiungono più ogg in cache ed essa si rinnova quando 
                    scade il tempo, o quando si liberano oggetti invocando il metodo remove.
                    Si genera un oggetto per ogni richiesta. 1 unità su 1000 units.
                    Si stailisce un rango di unita per cache che è più veloce di stimare l'occupazione di ram di
                    ogni richiesta. 

                    Per esempio 233mb/10'827 req = 0.0215 mb per request (* 1 request)
                    Se SizeLimit: 100 mb = 100b * 1024kb * 1024mb = 104'857'600 bytes 
                    0.0215 mb = 21.5 kb = 21'500 byte
                    
                    104'857'600 b / 21500 b = 4'877 bytes che dovrebbe occupare +o- ogni oggetto della cache (4.877 kb)
                    4'877 b capienza per oggetto in cache.
                    21500 b dimensione effettiva di ogni request.

                    quindi => cacheEntry.SetSize(4'877);    
                */
                cacheEntry.SetSize(1); // Peso in proporzione verso i raggiungimento della portata di 1000 oggetti massimi in cache        
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(secondi));
                return courseService.GetCourseAsync(id);
            });
        }

        public Task<List<CourseViewModel>> GetCoursesAsync()
        {
            // TODO: ricordati di usare memoryCache.Remove($"Course{id}") quando aggiorni il corso (entita del DB)
            // Lo useremmo nelle maschere di modifiche, cioè quando l'info cambia andremmo a rimuovere l'oggetto in cache

            double secondi = tempoDiCachingOptions.CurrentValue.Secondi;            
            return memoryCache.GetOrCreateAsync($"Courses", cacheEntry => 
            {
                cacheEntry.SetSize(1);
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(secondi)); //Esercizio: provate a recuperare il valore 60 usando il servizio di configurazione
                return courseService.GetCoursesAsync();
            });
        }
    }
}