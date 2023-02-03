using Microsoft.AspNetCore.Identity;

namespace ToDoListWebApi.Infrastructure.Identity;

public static class RolesConfiguration
{
    public static async Task CreateUserRoles(this IServiceCollection services)
    {
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var adminRoleCheck = await roleManager.RoleExistsAsync(UserRoles.Administrators);
        if (!adminRoleCheck)
        {
            var roleResult = await roleManager.CreateAsync(new IdentityRole(UserRoles.Administrators));
            if (!roleResult.Succeeded) throw new ApplicationException(roleResult.Errors.Aggregate(string.Empty, (current, err) => current + err.Description));
        }

        var webApiRoleCheck = await roleManager.RoleExistsAsync(UserRoles.WebApi);
        if (!webApiRoleCheck)
        {
            var roleResult = await roleManager.CreateAsync(new IdentityRole(UserRoles.WebApi));
            if (!roleResult.Succeeded) throw new ApplicationException(roleResult.Errors.Aggregate(string.Empty, (current, err) => current + err.Description));
        }     
    }
}
