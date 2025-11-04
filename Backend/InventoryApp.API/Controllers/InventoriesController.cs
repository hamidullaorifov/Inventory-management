using InventoryApp.Application.DTOs.Inventory;
using InventoryApp.Application.Features.Inventories.Commands;
using InventoryApp.Application.Features.Inventories.Queries;
using InventoryApp.Application.Features.InventoryFields.Commands;
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetInventoryById([FromRoute] Guid id)
    {
        var query = new GetInventoryByIdQuery(id);
        var result = await mediator.Send(query);
        return Ok(result);
    }
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateInventory([FromRoute] Guid id, [FromBody] InventoryUpdateDto dto)
    {
        var command = new UpdateInventoryCommand(id, dto);
        await mediator.Send(command);
        return NoContent();
    }
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInventory([FromRoute] Guid id)
    {
        var command = new DeleteInventoryCommand(id);
        await mediator.Send(command);
        return NoContent();
    }

    [Authorize]
    [HttpPost("{inventoryId}/fields")]
    public async Task<IActionResult> AddCustomField([FromRoute] Guid inventoryId, [FromBody] InventoryFieldCreateDto dto)
    {
        var command = new AddInventoryFieldCommand(inventoryId, dto);
        var result = await mediator.Send(command);
        return Ok(new { FieldId = result });
    }
    [Authorize]
    [HttpDelete("{inventoryId}/fields/{fieldId}")]
    public async Task<IActionResult> DeleteCustomField([FromRoute] Guid inventoryId, [FromRoute] Guid fieldId)
    {
        var command = new DeleteInventoryFieldCommand(inventoryId, fieldId);
        await mediator.Send(command);
        return NoContent();
    }
    [Authorize]
    [HttpPut("{inventoryId}/fields/{fieldId}")]
    public async Task<IActionResult> UpdateCustomField([FromRoute] Guid inventoryId, [FromRoute] Guid fieldId, [FromBody] InventoryFieldUpdateDto dto)
    {
        var command = new UpdateInventoryFieldCommand(inventoryId, fieldId, dto);
        await mediator.Send(command);
        return NoContent();
    }

}
