using Serilog;
using URLStatus.Application;
using URLStatus.Application.Logic.Abstractions;
using URLStatus.Infrastructure.Auth;
using URLStatus.Infrastructure.Persistence;
using URLStatus.WebAPI.Application.Auth;
using URLStatus.WebAPI.Middlewares;

namespace URLStatus.WebAPI
{
    public class Program
    {
        public static string APP_NAME = "URLStatus.WebAPI";
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.WithProperty("Application", APP_NAME)
                .Enrich.WithProperty("MachineName", Environment.MachineName)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateBootstrapLogger();
               

            var builder = WebApplication.CreateBuilder(args);

            if (builder.Environment.IsDevelopment())
            {
                builder.Configuration.AddJsonFile("appsettings.Development.local.json");
            }

            builder.Host.UseSerilog((context, services, configuration) =>
                configuration.Enrich.WithProperty("Application",APP_NAME)
                    .Enrich.WithProperty("MachineName",Environment.MachineName)
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext());

            // Add services to the container.
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddDatabaseCache();
            builder.Services.AddSqlDatabase(builder.Configuration.GetConnectionString("MainDbSql")!);
            builder.Services.AddControllers();
            builder.Services.AddJwtAuth(builder.Configuration);
            builder.Services.AddJwtAuthenticationDataProvider(builder.Configuration);
            builder.Services.AddPasswordManager();
            

            builder.Services.AddMediatR(c =>
            {
                c.RegisterServicesFromAssemblyContaining(typeof(BaseCommandHandler));
            });

            builder.Services.AddApplicationCollection();
            
            
            builder.Services.AddValidators();

            builder.Services.AddSwaggerGen(o =>
            {
                o.CustomSchemaIds(x =>
                {
                    var name = x.FullName;
                    if (name != null)
                    {
                        name = name.Replace("+", "_"); //swagger fixbug cant work with +
                    }

                    return name;
                });
            });



            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

            }


            app.UseExceptionResultMiddleware(); //exception so it has to be higher than methods below

            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}





//https://github.com/dotnet/sqlclient/issues/2239 account error fix 
