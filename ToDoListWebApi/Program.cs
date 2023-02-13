using Microsoft.AspNetCore.Localization;
using System.Globalization;
using ToDoListWebApi.Infrastructure.GlobalExceptionHandling;
using ToDoListWebApi.Infrastructure.HealthChecks;
using ToDoListWebApi.Infrastructure.Identity;
using ToDoListWebApi.Infrastructure.IoC;
using ToDoListWebApi.Persistence.Contexts;
using ToDoListWebApi.Persistence.Identity;

var builder = WebApplication.CreateBuilder(args);

IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

builder.Services.AddServices()
.AddFluentValidationServices()
.AddSwaggerServices()
.AddLoggerServices()
.AddDbContextServices(configuration)
.AddRepositoryServices()
.AddInfrastructureServices()
.AddIdentity()
.AddCustomAuthentication(builder);

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>()
    .AddDbContextCheck<ToDoListContext>()
    .AddSqlServer(configuration.GetConnectionString("DefaultConnection")!);

RolesConfiguration.CreateUserRoles(builder.Services).Wait();

var app = builder.Build();

app.UseMiddleware(typeof(GlobalErrorHandlingMiddleware));

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

app.MapHealthCheck();

var supportedCultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures,
    ApplyCurrentCultureToResponseHeaders= true
});

app.Run();
