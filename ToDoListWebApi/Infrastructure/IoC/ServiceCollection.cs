using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using System.Text;
using ToDoListWebApi.Infrastructure.Mapping;
using ToDoListWebApi.Persistence.Contexts;
using ToDoListWebApi.Persistence.Identity;
using ToDoListWebApi.Persistence.Models.Identity;
using ToDoListWebApi.Persistence.Repositories;
using ToDoListWebApi.Services.UserService;
using ToDoListWebApi.Services.ToDoListService;

namespace ToDoListWebApi.Infrastructure.IoC;

public static class ServiceCollection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddScoped<IToDoListService, ToDoListService>();
        services.AddTransient<IUserService, UserService>();

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
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDo WebApi", Version = "v1" });
            c.AddFluentValidationRulesScoped();
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
        });

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
        services.AddDbContext<ApplicationDbContext>();
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

    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>(
         option =>
         {
             option.Password.RequireDigit = false;
             option.Password.RequiredLength = 6;
             option.Password.RequireNonAlphanumeric = false;
             option.Password.RequireUppercase = false;
             option.Password.RequireLowercase = false;
         }).AddEntityFrameworkStores<ApplicationDbContext>()
     .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(
            p => p.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz" + "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
                                                    + "0123456789!#$%&'*+-/=?^_`{|}~.@");

        return services;
    }

    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = true;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder?.Configuration["Jwt:SigningKey"]!)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.FromMinutes(30)
            };
        });

        return services;
    }
}
