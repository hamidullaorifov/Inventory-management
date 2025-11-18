using AutoMapper;
using InventoryApp.Application.Common.Exceptions;
using InventoryApp.Application.DTOs.Inventory;
using InventoryApp.Application.Interfaces.Repositories;
using InventoryApp.Application.Interfaces.Services;
using InventoryApp.Domain.Entities;
using MediatR;
// TODO: Research about JWT!
namespace InventoryApp.Application.Features.Inventories.Commands;
public record CreateInventoryCommand(InventoryCreateDto Inventory) : IRequest<Guid>;

public class CreateInventoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IAuthService authService) : IRequestHandler<CreateInventoryCommand, Guid>
{
    public async Task<Guid> Handle(CreateInventoryCommand command, CancellationToken cancellationToken)
    {
        if (!await unitOfWork.CategoryRepository.ExistsAsync(c => c.Id == command.Inventory.CategoryId))
        {
            throw new NotFoundException(nameof(InventoryCategory), command.Inventory.CategoryId);
        }
        // TODO: Check user permissions here
        var inventory = mapper.Map<Inventory>(command.Inventory);
        inventory.OwnerId = authService.GetAuthenticatedUserId();

        // Handle tags
        if (command.Inventory.Tags.Count != 0)
        {
            var existingTags = await unitOfWork.TagRepository.GetAllAsync(t => command.Inventory.Tags.Contains(t.Name));

            var newTagNames = command.Inventory.Tags.Except(existingTags.Select(t => t.Name));
            var newTags = newTagNames.Select(t => new Tag { Name = t }).ToList();

            inventory.Tags = [.. existingTags, .. newTags];

            if (newTags.Count != 0)
            {
                await unitOfWork.TagRepository.AddRangeAsync(newTags);
            }
        }

        await unitOfWork.InventoryRepository.AddAsync(inventory);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return inventory.Id;
    }
}
