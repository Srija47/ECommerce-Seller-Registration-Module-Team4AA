using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AccountService.Repositories;
using Microsoft.OpenApi.Models;
using AccountService.GlobalExceptionFilter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.IO;
using AccountService.Manager;
using AccountService.Entities;
using AutoMapper;

namespace AccountService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<SellerDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SellerDBContext")));
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IAccountManager, AccountManager>();
            services.AddAutoMapper(typeof(Startup));
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options =>
                 options.AllowAnyOrigin()
                         .AllowAnyMethod()
                         .AllowAnyHeader());

            });
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddMvc(
                config => {
                    config.Filters.Add(typeof(CustomExceptionFilter));
                }
            );

            services.AddSwaggerGen(c =>
            {
                //c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                //c.RoutePrefix = string.Empty;
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Seller API",
                    Description = "Provides services to seller",
                    TermsOfService = new Uri("https://seller.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Team 4AA",
                        Email = string.Empty,
                        Url = new Uri("https://seller.com/spboyer"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://seller.com/license"),
                    }
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
          
            app.UseRouting();

            app.UseAuthorization();
            app.UseCors("AllowOrigin");
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
