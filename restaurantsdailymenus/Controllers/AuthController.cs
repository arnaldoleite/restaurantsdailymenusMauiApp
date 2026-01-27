using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using restaurantsdailymenus.Helpers;
using restaurantsdailymenus.Models;
using restaurantsdailymenus.Services;
using YamlDotNet.Core.Tokens;

namespace restaurantsdailymenus.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;
    private readonly JwtTokenGenerator _jwt;

    public AuthController(UserService userService, JwtTokenGenerator jwt)
    {
        _userService = userService;
        _jwt = jwt;
    }

    public record RegisterDto(string Username, string Password);
    public record LoginDto(string Username, string Password);

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var msg = "User already exists";
        var existing = await _userService.GetByUsernameAsync(dto.Username);
        if (existing != null)
            return BadRequest(new { msg });

        var user = new User
        {
            Username = dto.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        await _userService.CreateAsync(user);
        msg = "User created";
        return Ok(new { msg });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _userService.GetByUsernameAsync(dto.Username);
        if (user == null)
            return Unauthorized();

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return Unauthorized();

        var token = _jwt.GenerateToken(user);
        return Ok(new { token });
    }
}
