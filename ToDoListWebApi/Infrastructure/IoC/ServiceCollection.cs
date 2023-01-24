using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using ToDoListWebApi.Infrastructure.Mapping;
using ToDoListWebApi.Persistence.Contexts;
using ToDoListWebApi.Persistence.Repositories;
using ToDoListWebApi.Services;

namespace ToDoListWebApi.Infrastructure.IoC;

public static class ServiceCollection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddScoped<IToDoListService, ToDoListService>();

        return services;
    }

    public static IServiceCollection AddFluentValidationServices(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }

    public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
        services.AddSwaggerGen(c => c.AddFluentValidationRulesScoped());
        services.AddFluentValidationRulesToSwagger();

        return services;
    }

    public static IServiceCollection AddLoggerServices(this IServiceCollection services)
    {
        services.AddSingleton(sp => sp.GetRequiredService<ILoggerFactory>().CreateLogger("DefaultLogger"));

        return services;
    }

    public static IServiceCollection AddDbContextServices(this IServiceCollection services)
    {
        services.AddDbContext<ToDoListContext>();

        return services;
    }

    public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
    {
        services.AddTransient<IToDoListRepository, ToDoListRepository>();

        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MapperProfile));
        services.AddScoped<IModelMapper, ModelMapper>();

        return services;
    }
}
