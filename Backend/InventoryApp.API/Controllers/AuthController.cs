using InventoryApp.Application.Features.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApp.API.Controllers;
[Route("api/accounts")]
[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterCommand command)
    {
        var userId = await mediator.Send(command);
        return Ok(new { Id = userId });
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }
}
