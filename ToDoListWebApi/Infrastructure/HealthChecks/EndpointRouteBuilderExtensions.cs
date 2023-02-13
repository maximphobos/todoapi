﻿using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace ToDoListWebApi.Infrastructure.HealthChecks;

public static class EndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapHealthCheck(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHealthChecks("health/startup", new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        endpoints.MapHealthChecks("health/liveness", new HealthCheckOptions
        {
            Predicate = _ => false
        });

        endpoints.MapHealthChecks("health/readiness", new HealthCheckOptions
        {
            Predicate = r => r.Tags.Contains("readiness"),
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        return endpoints;
    }
}
