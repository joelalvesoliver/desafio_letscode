using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Reflection;

namespace Lets.Code.WebApi.DependencyInjection
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var dateVersion = File.GetCreationTime(Assembly.GetExecutingAssembly().Location);
            var deprecated = description.IsDeprecated ? " This API version has been deprecated." : string.Empty;
            var repositoryUrl = "https://github.com/joelalvesoliver/desafio_letscode";

            var openApiInfo = new OpenApiInfo
            {
                Title = "API Desafio Lets Code",
                Description = "API de Desafio Lets code" + deprecated,
                Version = description.ApiVersion.ToString(),
                License = new OpenApiLicense { Name = $"Version generation date {dateVersion:dd/MM/yyyy HH:mm:ss}" },
                Contact = new OpenApiContact()
                {
                    Name = "Lets Code",
                    Email = "joel.alves.oliver@gmail.com",
                    Url = new Uri(repositoryUrl)
                }
            };


            return openApiInfo;
        }
    }
}
