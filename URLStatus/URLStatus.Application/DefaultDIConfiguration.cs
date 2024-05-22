using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using URLStatus.Application.Interfaces;
using URLStatus.Application.Logic.Abstractions;
using URLStatus.Application.Services;
using URLStatus.Application.Validators;

namespace URLStatus.Application
{
    public static class DefaultDIConfiguration
    {
        public static IServiceCollection AddApplicationCollection(this IServiceCollection services)
        {
            services.AddScoped<ICurrentAccountProvider, CurrentAccountProvider>();
            services.AddScoped<MonitoredUrlChecker>();
            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining(typeof(BaseQueryHandler));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
