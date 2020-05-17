using AutoMapper;
using MarketPlace.Persistence;
using MarketPlace.Core.Interfaces;
using MarketPlace.Persistence.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using MarketPlace.Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MarketPlace.Helpers;

namespace MarketPlace
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
                builder = builder.AddUserSecrets<Startup>();

            builder = builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<PhotoSettings>(Configuration.GetSection("PhotoSettings"));
            
            services.AddAutoMapper();

            services.AddDbContext<MarketPlaceDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultDb")));

            services.AddScoped<IVehicleRepository, VehicleRepository>();

            services.AddScoped<IUnitofWork, UnitofWork>();

            services.AddScoped<IPhotoRepository, PhotoRepository>();

            services.AddTransient<IPhotoService, PhotoService>();

            services.AddTransient<IPhotoStorage, FileSystemPhotoStorage>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Add Auth0 Services

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(options =>
            //{
            //    options.Authority = "https://bijshakya77.auth0.com/";
            //    options.Audience = "https://api.marketplace.com";
            //});

            //limit authorization
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy(Policies.RequireAdminRole, policy => policy.RequireClaim("https://marketplace.com/roles", "Admin"));
            //});

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            //Enable authentication middleware
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                 //To learn more about options for serving an Angular SPA from ASP.NET Core,
                 //see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                     //set time for Angular CLI TimeOut

                    spa.Options.StartupTimeout = new TimeSpan(0, 0, 100);
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
