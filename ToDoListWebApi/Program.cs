using ToDoListWebApi.Persistence.Contexts;
using ToDoListWebApi.Persistence.Repositories;
using ToDoListWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

// DbContext and Repositories
builder.Services.AddDbContext<ToDoListContext>();
builder.Services.AddTransient<IToDoListRepository, ToDoListRepository>();

//Register custom services
builder.Services.AddScoped<IToDoListRepository, ToDoListRepository>();
builder.Services.AddScoped<IToDoListService, ToDoListService>();


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
