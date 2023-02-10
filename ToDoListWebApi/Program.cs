using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using ToDoListWebApi.Infrastructure.Identity;
using ToDoListWebApi.Infrastructure.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices()
.AddFluentValidationServices()
.AddSwaggerServices()
.AddLoggerServices()
.AddDbContextServices()
.AddRepositoryServices()
.AddInfrastructureServices()
.AddIdentity()
.AddCustomAuthentication(builder);

builder.Services.AddHealthChecks();

RolesConfiguration.CreateUserRoles(builder.Services).Wait();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/healthz", new HealthCheckOptions
{
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    }
}).RequireAuthorization();

app.Run();
