using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using ToDoListWebApi.Infrastructure.Mapping;
using ToDoListWebApi.Persistence.Contexts;
using ToDoListWebApi.Persistence.Repositories;
using ToDoListWebApi.Services;
using ToDoListWebApi.ViewModels.ToDoListViewModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//Fluent validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddSwaggerGen(c => c.AddFluentValidationRulesScoped());

builder.Services.AddFluentValidationRulesToSwagger();

//ILogger
builder.Services.AddSingleton(sp => sp.GetRequiredService<ILoggerFactory>().CreateLogger("DefaultLogger"));

// DbContext and Repositories
builder.Services.AddDbContext<ToDoListContext>();
builder.Services.AddTransient<IToDoListRepository, ToDoListRepository>();

//Register custom services
builder.Services.AddScoped<IToDoListService, ToDoListService>();

//Infrastructure
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddScoped<IModelMapper, ModelMapper>();

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
