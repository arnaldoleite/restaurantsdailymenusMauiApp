

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace restaurantsdailymenus.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class TestController : ControllerBase
{
    [Authorize]
    [HttpGet("secure")]
    public IActionResult SecureEndpoint()
    {
        return Ok("You are authenticated!");
    }
}
