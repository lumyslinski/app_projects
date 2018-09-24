/*
 * Star Wars API
 *
 * This is a REST API for managing Star Wars characters
 *
 * OpenAPI spec version: 1.0.0
 * Contact: luk@mysl.tech
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using RestApp.Data;
using RestApp.Data.Contracts;
using RestApp.Data.Database;
using RestApp.Data.Repositories;
using RestApp.Data.Services;
using RestApp.Filters;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;


namespace RestApp
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        private readonly IHostingEnvironment hostingEnv;

        private IConfiguration Configuration { get; }

        private const string ExceptionsOnStartup = "Startup";
        private const string ExceptionsOnConfigureServices = "ConfigureServices";
        private readonly Dictionary<string, List<Exception>> exceptions;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="env"></param>
        /// <param name="configuration"></param>
        public Startup(IHostingEnvironment env, IConfiguration configuration)
        {
            this.exceptions = new Dictionary<string, List<Exception>>
            {
                { ExceptionsOnStartup, new List<Exception>() },
                { ExceptionsOnConfigureServices, new List<Exception>() },
            };
            try
            {
                hostingEnv = env;
                Configuration = configuration;
            }
            catch (Exception exception)
            {
                exceptions[ExceptionsOnStartup].Add(exception);
            }
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                    // Add framework services.
                    services.AddMvc().AddJsonOptions(opts =>
                    {
                        opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                        opts.SerializerSettings.Converters.Add(new StringEnumConverter
                        {
                            CamelCaseText = true
                        });
                    });
                    #region database
                    services.AddEntityFrameworkSqlServer();
                    //TODO:
                    //send a question to .NET core team why if I use design-time factory the web app can not find an instance of DbContext?
                    //according to the documentation https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dbcontext-creation#from-a-design-time-factory
                    //framework should search firstly in application's startup project and then for the class which implemenets IDesignTimeDbContextFactory
                    var dbContext = ApplicationDbContextContainer.GetInstance();
                    services.AddSingleton(dbContext);
                    services.AddScoped<ICharacterRepository, CharacterRepository>();
                    services.AddScoped<IEpisodeRepository, EpisodeRepository>();
                    services.AddScoped<ICharacterService, CharacterService>();
                    #endregion
                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseSqlServer(Configuration.GetConnectionString("default"),
                            sqlServerOptions => sqlServerOptions.MigrationsAssembly("RestApp.Data"));
                    });
                    services.AddSwaggerGen(c =>
                    {
                        c.SwaggerDoc("1.0.0", new Info
                        {
                            Version = "1.0.0",
                            Title = "Star Wars API",
                            Description = "Star Wars API (ASP.NET Core 2.1)",
                            Contact = new Contact()
                            {
                                Name = "Swagger Codegen Contributors",
                                Url = "https://github.com/swagger-api/swagger-codegen",
                                Email = "luk@mysl.tech"
                            },
                            TermsOfService = ""
                        });
                        c.CustomSchemaIds(type => type.FriendlyId(true));
                        c.DescribeAllEnumsAsStrings();
                        c.IncludeXmlComments($"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}{hostingEnv.ApplicationName}.xml");
                        // Sets the basePath property in the Swagger document generated
                        c.DocumentFilter<BasePathFilter>("/api_v1");
                        // Include DataAnnotation attributes on Controller Action parameters as Swagger validation rules (e.g required, pattern, ..)
                        // Use [ValidateModelState] on Actions to actually validate it in C# as well!
                        c.OperationFilter<GeneratePathParamsValidationFilter>();
                    });
            }
            catch (Exception exception)
            {
                exceptions[ExceptionsOnConfigureServices].Add(exception);
            }
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
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
            app.UseMvc();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    //TODO: Either use the SwaggerGen generated Swagger contract (generated from C# classes)
                    c.SwaggerEndpoint("/swagger/1.0.0/swagger.json", "Star Wars API");
                });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseHsts();
            }
        }
    }
}
