using InventoryApp.Application.Features.Inventories.Commands;
using InventoryApp.Application.Features.Inventories.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApp.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class InventoriesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetInventories([FromQuery] GetInventoriesQuery query)
    {
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateInventory([FromBody] CreateInventoryCommand command)
    {
        var inventoryId = await mediator.Send(command);
        return Ok(new {InventoryId = inventoryId });
    }
}
