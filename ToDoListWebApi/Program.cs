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

builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>()
    .AddDbContextCheck<ToDoListContext>()
    .AddSqlServer(configuration.GetConnectionString("DefaultConnection")!);

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

app.MapHealthCheck();

app.Run();
