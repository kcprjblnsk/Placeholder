using Serilog;

namespace URLStatus.WebAPI
{
    public class Program
    {
        public static string APP_NAME = "URLStatus.WebAPI";
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((context, services, configuration) =>
                configuration.Enrich.WithProperty("Application",APP_NAME)
                    .Enrich.WithProperty("MachineName",Environment.MachineName)
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext());

            // Add services to the container.

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
