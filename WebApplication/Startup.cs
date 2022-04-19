using System;
using System.IO;
using BusinessLogic.Services;
using BusinessLogic.Services.Technical;
using BusinessLogicInterfaces;
using Entities.Repositories;
using EntitiesInterfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MyLogger;
using WebApplication.Controllers;
using WebApplication.ExceptionHandlerMiddleware;
using AppContext = Entities.Repositories.AppContext;

namespace WebApplication
{
    public class Startup
    {
        private readonly IConfiguration Configuration;
        private static readonly string _connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "DockerUniversityCRM", Version = "v2.0.0.0"});
                
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "WebApplication.xml");
                c.IncludeXmlComments(filePath);
            });
            services.AddTransient<IConfiguration>(sp =>
            {
                IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
                configurationBuilder.AddJsonFile("appsettings.json");
                return configurationBuilder.Build();
            });
            AddLogger(services);
            AddTransientNotificationWay(services);
            AddTransientServices(services);
            AddTransientRepositories(services);
            services.AddDbContext<AppContext>(option => option.UseNpgsql(_connectionString));
        }

        private void AddLogger(IServiceCollection services)
        {
            services.AddTransient<ILogger<HandleExceptionMiddleware>, StandartLogger<HandleExceptionMiddleware>>();
            
            services.AddTransient<ILogger<HomeworkServices>, StandartLogger<HomeworkServices>>();
            services.AddTransient<ILogger<LectionServices>, StandartLogger<LectionServices>>();
            services.AddTransient<ILogger<LectorServices>, StandartLogger<LectorServices>>();
            services.AddTransient<ILogger<StudentServices>, StandartLogger<StudentServices>>();
            services.AddTransient<ILogger<UniversityServices>, StandartLogger<UniversityServices>>();
            services.AddTransient<ILogger<AssessmentServices>, StandartLogger<AssessmentServices>>();
            
            services.AddTransient<ILogger<AppContext>, StandartLogger<AppContext>>();
            services.AddTransient<ILogger<HomeworkRepository>, StandartLogger<HomeworkRepository>>();
            services.AddTransient<ILogger<LectionRepository>, StandartLogger<LectionRepository>>();
            services.AddTransient<ILogger<LectorRepository>, StandartLogger<LectorRepository>>();
            services.AddTransient<ILogger<MarkAndVisitedRepository>, StandartLogger<MarkAndVisitedRepository>>();
            services.AddTransient<ILogger<StudentRepository>, StandartLogger<StudentRepository>>();
            services.AddTransient<ILogger<UniversityRepository>, StandartLogger<UniversityRepository>>();
            
            services.AddTransient<ILogger<Assessment>, StandartLogger<Assessment>>();
            services.AddTransient<ILogger<Homework>, StandartLogger<Homework>>();
            services.AddTransient<ILogger<Lection>, StandartLogger<Lection>>();
            services.AddTransient<ILogger<Lector>, StandartLogger<Lector>>();
            services.AddTransient<ILogger<Student>, StandartLogger<Student>>();
            services.AddTransient<ILogger<University>, StandartLogger<University>>();
        }

        private void AddTransientNotificationWay(IServiceCollection services)
        {
            services.AddTransient<INotification, SmsNotificationServices>();
            services.AddTransient<INotification, EMailNotificationServices>(); 
        }
        
        private void AddTransientServices(IServiceCollection services)
        {
            services.AddTransient<IHomeworkServices, HomeworkServices>();
            services.AddTransient<ILectionServices, LectionServices>();
            services.AddTransient<ILectorServices, LectorServices>();
            services.AddTransient<IStudentServices, StudentServices>();
            services.AddTransient<IUniversityServices, UniversityServices>();
            services.AddTransient<IAssessmentServices, AssessmentServices>();
            services.AddTransient<IVisitReport, VisitReport>();
        }
        
        private void AddTransientRepositories(IServiceCollection services)
        {
            services.AddTransient<IHomeworkRepository, HomeworkRepository>();
            services.AddTransient<ILectionRepository, LectionRepository>();
            services.AddTransient<ILectorRepository, LectorRepository>();
            services.AddTransient<IMarkAndVisitedRepository, MarkAndVisitedRepository>();
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IUniversityRepository, UniversityRepository>();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
             app.UseHandleException();
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication v1"));
            }
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}