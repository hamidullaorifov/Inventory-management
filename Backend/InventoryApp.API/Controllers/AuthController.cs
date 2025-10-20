using InventoryApp.Application.Features.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApp.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UserRegisterCommand command)
    {
        var userId = await mediator.Send(command);
        return Ok(new { Id = userId });
    }
}
