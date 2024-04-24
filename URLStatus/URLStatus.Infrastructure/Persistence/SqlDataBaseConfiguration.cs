using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using URLStatus.Application.Interfaces;

namespace URLStatus.Infrastructure.Persistence
{
    public static class SqlDataBaseConfiguration
    {
        public static IServiceCollection AddSqlDatabase(this IServiceCollection services, string connectionString)
        {
            Action<IServiceProvider, DbContextOptionsBuilder> sqlOptions = (serviceProvider, options) =>
                options.UseSqlServer(connectionString,
                    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)); 
            //one query to extract multiple data from multiple tables by joins (faster performance, less communication with database)

            services.AddDbContext<IApplicationDbContext, MainDbContext>(sqlOptions);
            return services;
        }
    }
}
