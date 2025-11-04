using AutoMapper;
using InventoryApp.Application.Common.Exceptions;
using InventoryApp.Application.DTOs.Inventory;
using InventoryApp.Application.Interfaces.Repositories;
using InventoryApp.Domain.Entities;
using MediatR;

namespace InventoryApp.Application.Features.InventoryFields.Commands;
public record AddInventoryFieldCommand(Guid InventoryId, InventoryFieldCreateDto FieldDto)
    : IRequest<Guid>;

public class AddInventoryFieldCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<AddInventoryFieldCommand, Guid>
{
    public async Task<Guid> Handle(AddInventoryFieldCommand request, CancellationToken cancellationToken)
    {
        var inventory = await unitOfWork.InventoryRepository.GetByIdAsync(request.InventoryId)
            ?? throw new NotFoundException(nameof(Inventory), request.InventoryId);
        var field = mapper.Map<InventoryFieldDefinition>(request.FieldDto);

        field.InventoryId = request.InventoryId;
        await unitOfWork.InventoryFieldRepository.AddAsync(field);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return field.Id;
    }
}
