using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SellerService.Entities;
using SellerService.GlobalExceptionFilter;
using SellerService.Manager;
using SellerService.Repositories;

namespace SellerService
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
                options.UseSqlServer(Configuration.GetConnectionString("SellerDBContext1")));
            services.AddTransient<ISellerRepository, SellerRepository>();
            services.AddTransient<ISellerManager, SellerManager>();
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options =>
                 options.AllowAnyOrigin()
                         .AllowAnyMethod()
                         .AllowAnyHeader());

            });
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddMvc(
                config =>
                {
                    config.Filters.Add(typeof(CustomExceptionFilter));
                }
            );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
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
                //// Set the comments path for the Swagger JSON and UI.
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);

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
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "Seller API V2");
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
