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

app.Run();
