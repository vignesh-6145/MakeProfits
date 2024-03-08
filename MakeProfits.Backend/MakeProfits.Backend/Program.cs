
using Serilog;
using MakeProfits.Repository;
using MakeProfits.Backend.Utillity;
using MakeProfits.Backend.Repository;

namespace MakeProfits.Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Logging at Program.cs
            var Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);

            //Enabling Cors
            builder.Host.UseSerilog();

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder =>
                {
                    builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
                });
            });
            //Adding API utilities
            builder.Services.AddScoped<IInvestmentsUtility,InvestmentsUtility>();


            //Database connections

            string? connectionString = builder.Configuration.GetConnectionString("DBConnection");
            builder.Services.AddSingleton(new UserDataAccess(connectionString));
            builder.Services.AddSingleton<IInvestmentDataAccess,InvestmentDataAccess>();
            builder.Services.AddSingleton<IAdvisorDataAccess, AdvisorDataAccess>();
                  
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseSerilogRequestLogging();

            //Middleware to log the requests
            app.Use(async (context, next) =>
            {
                Log.Information("Received {RequestMethod} {RequestPath}",context.Request.Method,context.Request.Path);
                await next(context);
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("AllowAllOrigins");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
            Log.CloseAndFlush();
        }
    }
}
