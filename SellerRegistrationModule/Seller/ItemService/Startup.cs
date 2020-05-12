using System;
using ItemService.Entities;
using ItemService.GlobalExceptionFilter;
using ItemService.Manager;
using ItemService.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.IO;

namespace ItemService
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
                options.UseSqlServer(Configuration.GetConnectionString("SellerDBContext2")));
            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddTransient<IItemManager, ItemManager>();
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
                // Set the comments path for the Swagger JSON and UI.
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
