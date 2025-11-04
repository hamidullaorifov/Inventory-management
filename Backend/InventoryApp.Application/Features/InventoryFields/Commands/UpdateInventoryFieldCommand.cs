using AutoMapper;
using InventoryApp.Application.Common.Exceptions;
using InventoryApp.Application.DTOs.Inventory;
using InventoryApp.Application.Interfaces.Repositories;
using InventoryApp.Domain.Entities;
using MediatR;

namespace InventoryApp.Application.Features.InventoryFields.Commands;
public record UpdateInventoryFieldCommand(
    Guid InventoryId,
    Guid FieldId,
    InventoryFieldUpdateDto Dto
) : IRequest<Unit>;

public class UpdateInventoryFieldCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateInventoryFieldCommand, Unit>
{
    public async Task<Unit> Handle(UpdateInventoryFieldCommand request, CancellationToken cancellationToken)
    {
        var field = await unitOfWork.InventoryFieldRepository.GetByIdAsync(request.FieldId)
            ?? throw new NotFoundException(nameof(InventoryFieldDefinition), request.FieldId);
        mapper.Map(request.Dto, field);
        await unitOfWork.InventoryFieldRepository.UpdateAsync(field);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
