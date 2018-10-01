using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ImageApp.Data.Contracts;
using ImageApp.Data.Database;
using ImageApp.Data.Models;
using ImageApp.Data.Repositories;
using ImageApp.Data.Services;
using ImageApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ImageApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public static string GoogleVisionApiUrl { get; private set; }
        private const string ExceptionsOnStartup = "Startup";
        private const string ExceptionsOnConfigureServices = "ConfigureServices";
        private readonly Dictionary<string, List<Exception>> exceptions;

        public Startup(IConfiguration configuration)
        {
            this.exceptions = new Dictionary<string, List<Exception>>
            {
                { ExceptionsOnStartup, new List<Exception>() },
                { ExceptionsOnConfigureServices, new List<Exception>() },
            };
            try
            {
                Configuration = configuration;
            }
            catch (Exception exception)
            {
                exceptions[ExceptionsOnStartup].Add(exception);
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                var googleVisionApiSettings = Configuration.GetSection("GoogleVisionApiSection").Get<GoogleVisionApiSettings>();
                if (googleVisionApiSettings != null)
                {
                    GoogleVisionApiUrl = string.Format(googleVisionApiSettings.RestUrl, googleVisionApiSettings.RestToken);
                    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
                    #region database
                    services.AddEntityFrameworkSqlite();
                    services.AddScoped<RepositoryContext>(_ => new RepositoryContextDesignTimeDbContextFactory().Create());
                    services.AddScoped<IImageRepository, ImageRepository>();
                    services.AddScoped<IImageDetailsRepository, ImageDetailsRepository>();
                    services.AddScoped<IImageDetailsWebRepository, ImageDetailsWebRepository>();
                    services.AddScoped<IImageWebMatchesRepository, ImageWebMatchesRepository>();
                    services.AddScoped<IImageService, ImageService>();
                    #endregion
                    services.AddSpaStaticFiles(conf => { conf.RootPath = "ClientApp/dist"; });
                }
                else
                {
                    throw new Exception("Can not read GoogleVisionApiSection settings! Set required data in secrets.json");
                }
            }
            catch (Exception exception)
            {
                exceptions[ExceptionsOnConfigureServices].Add(exception);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (this.exceptions.Any(p => p.Value.Any()))
            {
                app.Run(
                    async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "text/plain";

                        foreach (var ex in this.exceptions)
                        {
                            foreach (var val in ex.Value)
                            {
                                await context.Response.WriteAsync($"Error on {ex.Key}: {val.Message}").ConfigureAwait(false);
                            }
                        }
                    });
                return;
            }
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
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "deafult",
                    template: "{controller}/{action=Index}/{id?}"
                );
            });
            app.UseSpa(
                spa =>
                {
                    spa.Options.SourcePath = "ClientApp";
                    if (env.IsDevelopment())
                    {
                        spa.UseAngularCliServer(npmScript: "start");
                    }
                }
            );

            // create a service scope to get an ApplicationDbContext instance using DI and create fresh db
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<RepositoryContext>();
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
            }
        }
    }
}
