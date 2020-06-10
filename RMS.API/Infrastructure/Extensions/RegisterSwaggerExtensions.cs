namespace RMS.API.Infrastructure.Extensions
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.AspNetCore.Mvc.Versioning;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using RMS.API.Infrastructure.Filters;
    using Swashbuckle.AspNetCore.Swagger;

    public static class RegisterSwaggerExtensions
    {
        /// <summary>
        /// Configure swagger documentation. Add Azure Oauth2 authentication and API versioning.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <param name="configuration">Configuration.</param>
        public static void RegisterSwaggerDocumentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddVersionedApiExplorer(setupAction =>
            {
                setupAction.GroupNameFormat = "'v'VV";
                setupAction.SubstituteApiVersionInUrl = true;
            });

            services.AddApiVersioning(setupAction =>
            {
                setupAction.AssumeDefaultVersionWhenUnspecified = true;
                setupAction.DefaultApiVersion = new ApiVersion(1, 0);
                setupAction.ReportApiVersions = true;
            });

            var apiVersionDescriptionProvider =
                services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();

            services.AddSwaggerGen(swg =>
            {
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    swg.SwaggerDoc($"VUTP-RMS-APISpecification{description.GroupName}", new Info { Title = "VUTP RMS API", Description = "VUTP RMS Server Documentation", Version = description.ApiVersion.ToString(), Contact = new Contact() { Name = "UTP", Url = "http://www.utp.bg/" } });
                }

                var xmlDocPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @".\RMS.API.xml";
                swg.IncludeXmlComments(xmlDocPath);
                swg.DescribeAllEnumsAsStrings();

                swg.DocInclusionPredicate((documentName, apiDescription) =>
                {
                    var actionApiVersionModel = apiDescription.ActionDescriptor
                        .GetApiVersionModel(ApiVersionMapping.Explicit | ApiVersionMapping.Implicit);

                    if (actionApiVersionModel == null)
                    {
                        return true;
                    }

                    if (actionApiVersionModel.DeclaredApiVersions.Any())
                    {
                        return actionApiVersionModel.DeclaredApiVersions.Any(v =>
                            $"VUTP-RMS-APISpecificationv{v.ToString()}" == documentName);
                    }

                    return actionApiVersionModel.ImplementedApiVersions.Any(v =>
                        $"VUTP-RMS-APISpecificationv{v.ToString()}" == documentName);
                });

                swg.AddSecurityDefinition("Bearer", new ApiKeyScheme { In = "header", Description = "Please enter into field the word 'Bearer' following by space and JWT", Name = "Authorization", Type = "apiKey" });
        
                swg.OperationFilter<AuthResponsesOperationFilter>();
            });
        }
    }
}