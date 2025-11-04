using InventoryApp.Application.Common.Exceptions;
using InventoryApp.Application.Interfaces.Repositories;
using InventoryApp.Domain.Entities;
using MediatR;

namespace InventoryApp.Application.Features.Inventories.Commands;
public record DeleteInventoryFieldCommand(Guid InventoryId, Guid FieldId)
    : IRequest<Unit>;
public class DeleteInventoryFieldCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteInventoryFieldCommand, Unit>
    {
    public async Task<Unit> Handle(DeleteInventoryFieldCommand request, CancellationToken cancellationToken)
    {
        var inventory = await unitOfWork.InventoryRepository.GetByIdAsync(request.InventoryId)
            ?? throw new NotFoundException(nameof(Inventory), request.InventoryId);
        var field = await unitOfWork.InventoryFieldRepository.GetByIdAsync(request.FieldId)
            ?? throw new NotFoundException(nameof(InventoryFieldDefinition), request.FieldId);
        if (field.InventoryId != inventory.Id)
        {
            throw new BadRequestException("Field does not belong to the specified inventory.");
        }
        await unitOfWork.InventoryFieldRepository.DeleteAsync(field);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
