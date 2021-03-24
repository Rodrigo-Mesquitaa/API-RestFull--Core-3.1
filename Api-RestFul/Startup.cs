using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Api_RestFul.Contexts;
using Api_RestFul.Notification;
using Api_RestFul.Service;
using Api_RestFul.Service.Interfaces;
using System;
using System.IO;
using System.Reflection;

namespace Api_RestFul
{
    public class Startup
    {
        private readonly IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _environment = environment;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.IgnoreNullValues = true;
            });

            services.AddProblemDetails(options => options.IncludeExceptionDetails = (context, exception) => _environment.IsDevelopment());
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "Api_RestFul - Example",
                    Description = "Swagger surface",
                    Contact = new OpenApiContact()
                    {
                        Name = "Rodrigo Mesquita",
                        Email = "rodrigo09.mesquita@gmail.com",
                      
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "MIT",
                    },

                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

            });
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddEntityFrameworkInMemoryD.AddDbContext<RestfulContext>(o => o.UseInMemoryDatabase("api-restful"));
            services.AddLogging();
            services.AddAutoMapper(typeof(Startup));
            RegisterServices(services);
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IDomainNotificationMediatorService, DomainNotificationMediatorService>();
            services.AddTransient<IDummyUserService, DummyUserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SSO Api Management");
                c.OAuthClientId("Swagger");
                c.OAuthClientSecret("swagger");
                c.OAuthAppName("SSO Management Api");
                c.OAuthUseBasicAuthenticationWithAccessCodeGrant();
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

}
