using AutoMapper;
using InventoryApp.Application.Common.Exceptions;
using InventoryApp.Application.DTOs.Inventory;
using InventoryApp.Application.Interfaces.Repositories;
using InventoryApp.Domain.Entities;
using MediatR;


namespace InventoryApp.Application.Features.Inventories.Commands;
public record UpdateInventoryCommand(Guid Id, InventoryUpdateDto Dto) : IRequest<Unit>;
public class UpdateInventoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateInventoryCommand, Unit>
{
    public async Task<Unit> Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
    {
        var inventory = await unitOfWork.InventoryRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Inventory), request.Id);

        // TODO: Check user permissions here
        mapper.Map(request.Dto, inventory);

        if (!await unitOfWork.CategoryRepository.ExistsAsync(c => c.Id == request.Dto.CategoryId))
        {
            throw new NotFoundException(nameof(InventoryCategory), request.Dto.CategoryId);
        }
        mapper.Map(request.Dto, inventory);
        await unitOfWork.InventoryRepository.UpdateAsync(inventory);
        inventory.Tags.Clear();
        if (request.Dto.Tags.Any())
        {
            var existingTags = await unitOfWork.TagRepository.GetAllAsync(t =>
                request.Dto.Tags.Contains(t.Name));

            var newTagNames = request.Dto.Tags.Except(existingTags.Select(t => t.Name));
            var newTags = newTagNames.Select(t => new Tag { Name = t }).ToList();

            if (newTags.Any())
            {
                await unitOfWork.TagRepository.AddRangeAsync(newTags);
            }

            inventory.Tags = [.. existingTags, .. newTags];
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
