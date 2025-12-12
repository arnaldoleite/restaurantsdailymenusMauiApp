using Microsoft.AspNetCore.Mvc;
using restaurantsdailymenus.Models;
using restaurantsdailymenus.Services;
using restaurantsdailymenus.Helpers;
using BCrypt.Net;

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
        var existing = await _userService.GetByUsernameAsync(dto.Username);
        if (existing != null)
            return BadRequest("User already exists");

        var user = new User
        {
            Username = dto.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        await _userService.CreateAsync(user);
        return Ok("User created");
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
