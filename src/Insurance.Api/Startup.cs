using AutoMapper;
using Insurance.Api.API;
using Insurance.Api.Automapper;
using Insurance.Api.Interfaces;
using Insurance.Api.Middleware;
using Insurance.Api.Services;
using Insurance.Api.Wrapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System;

namespace Insurance.Api
{
    /// <summary>
    /// Startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// gets Configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="env">env.</param>
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                               .SetBasePath(env.ContentRootPath)
                               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                               .AddEnvironmentVariables();

            Configuration = builder.Build();

            // Initialize Serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateBootstrapLogger();
        }

        // This method gets called by the runtime. Use this method to add services to the container.

        /// <summary>
        /// ConfigureServices.
        /// </summary>
        /// <param name="services">services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API Name", Version = "v1" });
            });

            // automapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Mapping());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<IInsuranceService, InsuranceService>();
            services.AddScoped<IProductAPIService, ProductAPIService>();
            services.AddScoped<IHttpClientWrapper, HttpClientWrapper>();

            services.AddHttpClient();

            // services.AddHttpClient<IBussinessRuleService, BussinessRuleService>(c =>
            //    c.BaseAddress = new Uri(Configuration.GetValue<string>("ProductApi")));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        /// <summary>
        /// Configure.
        /// </summary>
        /// <param name="app">app.</param>
        /// <param name="env">env.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "InsuranceAPI");
            });
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
