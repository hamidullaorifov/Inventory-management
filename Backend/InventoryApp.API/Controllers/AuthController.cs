using InventoryApp.Application.DTOs.Auth;
using InventoryApp.Application.Features.Users.Commands;
using InventoryApp.Application.Features.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApp.API.Controllers;
[Route("api/accounts")]
[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var command = new UserRegisterCommand(dto);
        var userId = await mediator.Send(command);
        return Ok(new { Id = userId });
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("autocomplete")]
    public async Task<ActionResult<List<UserAutocompleteDto>>> Autocomplete([FromQuery] string q)
    {
        var result = await mediator.Send(new UserAutocompleteQuery(q));
        return Ok(result);
    }
}
