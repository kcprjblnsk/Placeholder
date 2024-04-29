using URLStatus.Application.Interfaces;
using URLStatus.Infrastructure.Auth;

namespace URLStatus.WebAPI.Application.Auth;

public static class JwtAuthDataProviderConfiguration
{
    public static IServiceCollection AddJwtAuthenticationDataProvider(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CookieSettings>(configuration.GetSection("CookieSettings"));
        services.AddScoped<IAuthenticationDataProvider, JwtAuthenticationDataProvider>();

        return services;
    }
}