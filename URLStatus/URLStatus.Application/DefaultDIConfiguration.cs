using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using URLStatus.Application.Interfaces;
using URLStatus.Application.Services;

namespace URLStatus.Application
{
    public static class DefaultDIConfiguration
    {
        public static IServiceCollection AddApplicationCollection(this IServiceCollection services)
        {
            services.AddScoped<ICurrentAccountProvider, CurrentAccountProvider>();
            return services;
        }
    }
}
