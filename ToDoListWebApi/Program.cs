using ToDoListWebApi.Infrastructure.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices()
.AddFluentValidationServices()
.AddSwaggerServices()
.AddLoggerServices()
.AddDbContextServices()
.AddRepositoryServices()
.AddInfrastructureServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.Run();
