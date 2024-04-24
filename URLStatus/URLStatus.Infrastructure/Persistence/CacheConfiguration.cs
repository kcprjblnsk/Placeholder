using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCoreSecondLevelCacheInterceptor;
using Microsoft.Extensions.DependencyInjection;

namespace URLStatus.Infrastructure.Persistence
{
    public static class CacheConfiguration
    {
        public static IServiceCollection AddDatabaseCache(this IServiceCollection services)
        {
            services.AddEFSecondLevelCache(o =>
                o.UseMemoryCacheProvider(CacheExpirationMode.Absolute, TimeSpan.FromMinutes(5))
                    .DisableLogging(false)
                    .UseCacheKeyPrefix("EF_"));
            return services;
        }
    }
}
