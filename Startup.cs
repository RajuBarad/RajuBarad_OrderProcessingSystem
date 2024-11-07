using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OrderProcessingSystem.Interface;
using OrderProcessingSystem.Services;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderProcessingSystem
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
           
            // Add Swagger services to the container
            services.AddSwaggerGen(options =>
            {
                // Add a basic Swagger metadata
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Order Processing API",
                    Version = "v1",
                    Description = "API for managing orders, customers, and products."
                });

                // Optional: Add comments to the Swagger UI from the XML doc file (for better API documentation)
                //var xmlFile = $"{System.AppDomain.CurrentDomain.BaseDirectory}/OrderProcessingSystem.xml";
                //var xmlPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, xmlFile);
                //options.IncludeXmlComments(xmlPath);
            });
            // Register ApplicationDbContext with dependency injection
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Register your repositories
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            // Register your services
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderService, OrderService>();

            // Register Serilog for logging
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog());

            // Add Web API services
            //services.AddControllers();
            services.AddControllersWithViews()
                    .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
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
            // Enable Swagger middleware to serve the Swagger UI and Swagger JSON
            app.UseSwagger();

            // Enable the Swagger UI middleware to display the Swagger UI at /swagger
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Order Processing API v1");
                options.RoutePrefix = string.Empty; // To serve the Swagger UI at the root URL (optional)
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
