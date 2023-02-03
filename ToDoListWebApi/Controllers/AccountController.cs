using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoListWebApi.Services.UserService.Requests;
using ToDoListWebApi.Services.UserService;
using ToDoListWebApi.ViewModels.AccountViewModels;
using ToDoListWebApi.Infrastructure.Identity;

namespace ToDoListWebApi.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;

    public AccountController(IUserService userService,
        ILogger<AccountController> logger,
        IConfiguration configuration)
    {
        _userService = userService;
        _logger = logger;
        _configuration = configuration;
    }

    [Route("login")]
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> Login(LoginViewModel model)
    {
        var result = await _userService.PasswordSignInAsync(new PasswordSignInAsyncRequest()
        {
            UserName = model.UserName,
            UserPassword = model.UserPassword,
            RememberMe = true,
            LockoutOnFailure = false
        });

        if (result.Succeeded)
        {
            _logger.LogInformation($"User {model.UserName} was logged in.");

            var loggedUser = await _userService.GetUserByName(new BaseUserRequest()
            {
                UserName = model.UserName
            });

            string? role = UserRoles.WebApi;
            if (loggedUser != null && loggedUser.UserRoles.Any())
                role = Enumerable.FirstOrDefault(loggedUser.UserRoles);

            IdentityOptions _options = new IdentityOptions();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                  new Claim("UserID", loggedUser?.Id!),
                  new Claim(_options.ClaimsIdentity.RoleClaimType, role!),
                  new Claim(_options.ClaimsIdentity.UserNameClaimType, loggedUser?.UserName!)
                }),

                Expires = role == UserRoles.WebApi ? DateTime.UtcNow.AddMinutes(5) : DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]!)),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return Ok(new { token });
        }
        else
        {
            _logger.LogInformation($"There was an error of logged in user {model.UserName}.");

            return Unauthorized();
        }
    }


    [Route("register")]
    [HttpPost]
    public async Task<IActionResult> Register(RegisterUserViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _userService.CreateUserAsync(new CreateUserAsyncRequest()
            {
                UserName = model.UserName,
                UserEmail = model.UserEmail,
                UserPassword = model.UserPassword
            });

            if (result.Succeeded)
            {
                _logger.LogInformation($"Created a new user account: {model.UserName}");

                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        _logger.LogInformation($"There was an error during creating new user account: {model.UserName}");

        return BadRequest();
    }
}
