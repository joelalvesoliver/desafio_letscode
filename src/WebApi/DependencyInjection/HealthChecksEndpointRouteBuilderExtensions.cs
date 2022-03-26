using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lets.Code.WebApi.DependencyInjection
{
    public static class HealthChecksEndpointRouteBuilderExtensions
    {
        public static void MapApplicationHealthChecks(this IEndpointRouteBuilder endpoints, string pattern, Func<HealthCheckRegistration, bool> predicate = default)
            => endpoints.MapHealthChecks(
                pattern: pattern,
                options: new HealthCheckOptions()
                {
                    AllowCachingResponses = false,
                    ResponseWriter = WriteResponse,
                    Predicate = predicate,
                    ResultStatusCodes =
                    {
                        [HealthStatus.Healthy] = StatusCodes.Status200OK,
                        [HealthStatus.Degraded] = StatusCodes.Status500InternalServerError,
                        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                    }
                });

        public static async Task WriteResponse(HttpContext context, HealthReport result)
        {
            context.Response.ContentType = "application/json; charset=utf-8";

            var results = result.Entries.ToDictionary(entry => entry.Key, entry => new
            {
                status = entry.Value.Status.ToString(),
                description = entry.Value.Description,
                data = entry.Value.Data
            });

            await JsonSerializer.SerializeAsync(context.Response.Body, results, cancellationToken: context.RequestAborted);
        }
    }
}
