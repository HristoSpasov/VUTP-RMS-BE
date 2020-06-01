namespace RMS.API
{
    using System;
    using System.Collections.Generic;
    using Autofac;
    using Infrastructure.Extensions;
    using Infrastructure.Middleware;
    using IOC;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using RMS.API.Models.Helpers;
    using RMS.Services;
    using RMS.Services.Contracts;
    using Serilog;

    /// <summary>
    /// .NET core startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">Server configuration properties</param>
        /// <param name="hostingEnvironment">Hosting environment information</param>
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            this.Configuration = configuration;
            this.HostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Gets application configuration properties.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Gets hosting environment
        /// </summary>
        public IHostingEnvironment HostingEnvironment { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Services to add</param>
        /// <returns>Service provider</returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.RegisterMvc();
            services.RegisterCompression();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.ConfigureApiBadModelBehavior();
            services.ConfigureIdentity();
            services.ConfigureJWTAuthentication(this.Configuration);
            services.ConfigureAuthorizationClaimPolicies();
            services.RegisterSwaggerDocumentation(this.Configuration);

            services.AddSingleton<CacheService>();

            services.AddSingleton<JwtIssuerOptions>(this.Configuration.GetSection(nameof(JwtIssuerOptions)).Get<JwtIssuerOptions>());

            var connectionString = this.Configuration.GetConnectionString("VUTP-RMS-SQL");
            var container = AutoFacConfig.Initialize(connectionString, this.HostingEnvironment, services);
            return container.Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            }
            else
            {
                app.UseMiddleware(typeof(ErrorHandlingMiddleware));
                app.UseHsts();
            }

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add(
                    "Content-Security-Policy",
                    $"default-src 'self'; img-src 'self' data: blob:; object-src 'none'; script-src 'self' 'unsafe-inline' 'unsafe-eval'; connect-src 'self' {this.Configuration["Headers:CSP-API"]}; frame-src 'self'");

                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");

                context.Response.Headers.Add("X-Xss-Protection", "1");

                context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");

                await next();
            });

            app.UseResponseCompression();

            loggerFactory.AddSerilog();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();
            var allowedDomains = this.Configuration.GetSection("CORS").Get<List<string>>().ToArray();

            app.UseCors(builder =>
            {
                builder
                    .WithOrigins(allowedDomains)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });

            var documentationAuthorizedIps = this.Configuration.GetSection("DocumentationWhiteList").Get<List<string>>();

            app.UseMiddleware<SwaggerAuthorizationMiddleware>(documentationAuthorizedIps);
            app.UseSwagger();

            app.UseHttpsRedirection();
            app.UseSwaggerUI(cfg =>
            {
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    cfg.SwaggerEndpoint($"/swagger/" + $"VUTP-RMS-APISpecification{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }

                cfg.RoutePrefix = string.Empty;
            });

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}