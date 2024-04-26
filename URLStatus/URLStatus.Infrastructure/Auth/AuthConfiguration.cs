﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCoreSecondLevelCacheInterceptor;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using URLStatus.Application.Interfaces;
using URLStatus.Infrastructure.Persistence;

namespace URLStatus.Infrastructure.Auth
{
    public static class AuthConfiguration
    {
        public static IServiceCollection AddJwtAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtAuthenticationOptions>(configuration.GetSection("JwtAuthentication"));
            services.AddSingleton<JwtManager>();

            return services;
        }
        public static IServiceCollection AddPasswordManager(this IServiceCollection services)
        {
            services.AddScoped(typeof(IPasswordHasher<>),typeof(PasswordHasher<>));
            services.AddScoped<IPasswordManager, PasswordManager>();

            return services;
        }
    }
}
