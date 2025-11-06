using InventoryApp.Application.Common.Exceptions;
using InventoryApp.Application.DTOs.Item;
using InventoryApp.Application.Interfaces.Repositories;
using InventoryApp.Application.Interfaces.Services;
using InventoryApp.Domain.Entities;
using InventoryApp.Domain.Enums;
using MediatR;

namespace InventoryApp.Application.Features.Items.Commands;
public record CreateItemCommand(Guid InventoryId, CreateItemDto Dto) : IRequest<Guid>;

public class CreateItemCommandHandler(
    IUnitOfWork unitOfWork,
    IAuthService authService,
    ICustomIdService customIdService) : IRequestHandler<CreateItemCommand, Guid>
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
            UpdatedById = authService.GetAuthenticatedUserId(),
            CustomId = string.IsNullOrWhiteSpace(request.Dto.CustomId)
                ? await customIdService.GenerateCustomIdAsync(inventory)
                : request.Dto.CustomId,
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

            var fieldValue = new ItemFieldValue
            {
                ItemId = item.Id,
                FieldDefinitionId = fv.FieldDefinitionId,
            };
            if (fieldDef.Type == FieldType.Number)
            {
                fieldValue.NumberValue = fv.NumberValue;
            }
            else if (fieldDef.Type == FieldType.Boolean)
            {
                fieldValue.BoolValue = fv.BoolValue;
            }
            else
            {
                fieldValue.StringValue = fv.StringValue;
            }
            fieldValues.Add(fieldValue);

        }
        await unitOfWork.ItemRepository.AddAsync(item);
        await unitOfWork.ItemFieldRepository.AddRangeAsync(fieldValues);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return item.Id;
    }
}