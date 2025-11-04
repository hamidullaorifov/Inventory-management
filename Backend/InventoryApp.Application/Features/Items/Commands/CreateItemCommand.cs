using AutoMapper;
using InventoryApp.Application.Common.Exceptions;
using InventoryApp.Application.DTOs.Item;
using InventoryApp.Application.Interfaces.Repositories;
using InventoryApp.Application.Interfaces.Services;
using InventoryApp.Domain.Entities;
using MediatR;

namespace InventoryApp.Application.Features.Items.Commands;
public record CreateItemCommand(Guid InventoryId, CreateItemDto Dto) : IRequest<Guid>;

public class CreateItemCommandHandler(IUnitOfWork unitOfWork, IAuthService authService, IMapper mapper) : IRequestHandler<CreateItemCommand, Guid>
{
    public async Task<Guid> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        var inventory = await unitOfWork.InventoryRepository.GetByIdAsync(request.InventoryId)
            ?? throw new NotFoundException(nameof(Inventory), request.InventoryId);
        var item = new Item
        {
            Id = Guid.NewGuid(),
            InventoryId = request.InventoryId,
            CreatedById = authService.GetAuthenticatedUserId(),
            CustomId = request.Dto.CustomId ?? string.Empty,
        };
        var fieldValues = new List<ItemFieldValue>();
        foreach (var fv in request.Dto.FieldValues)
        {
            var fieldDef = await unitOfWork.InventoryFieldRepository.GetByIdAsync(fv.FieldDefinitionId)
                ?? throw new BadRequestException($"Field definition {fv.FieldDefinitionId} not found in this inventory.");
            if (fieldDef.InventoryId != request.InventoryId)
            {
                throw new BadRequestException($"Field definition {fv.FieldDefinitionId} does not belong to inventory {request.InventoryId}.");
            }
            var fieldValue = mapper.Map<ItemFieldValue>(fv);
 
            fieldValues.Add(fieldValue);

        }
        await unitOfWork.ItemFieldRepository.AddRangeAsync(fieldValues);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return item.Id;
    }
}