using InventoryApp.Application.Common.Exceptions;
using InventoryApp.Application.DTOs.Inventory;
using InventoryApp.Application.Interfaces.Repositories;
using InventoryApp.Domain.Entities;
using MediatR;

namespace InventoryApp.Application.Features.Inventories.Commands;
public record AddInventoryAccessCommand(Guid InventoryId, AddInventoryAccessDto Dto) : IRequest<Unit>;

public class AddInventoryAccessCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddInventoryAccessCommand, Unit>
{
    public async Task<Unit> Handle(AddInventoryAccessCommand request, CancellationToken cancellationToken)
    {
        var inventory = await unitOfWork.InventoryRepository.GetByIdAsync(request.InventoryId)
            ?? throw new NotFoundException(nameof(Inventory), request.InventoryId);
        var user = await unitOfWork.UserRepository.GetByIdAsync(request.Dto.UserId)
            ?? throw new NotFoundException(nameof(User), request.Dto.UserId);
        var existingAccess = await unitOfWork.InventoryAccessRepository.GetByUserAndInventoryAsync(request.Dto.UserId, request.InventoryId);
        if (existingAccess != null)
        {
            throw new BadRequestException($"User {request.Dto.UserId} already has access to inventory {request.InventoryId}.");
        }
        var inventoryAccess = new InventoryAccess
        {
            InventoryId = request.InventoryId,
            UserId = request.Dto.UserId,
            CanWrite = request.Dto.CanWrite
        };
        await unitOfWork.InventoryAccessRepository.AddAsync(inventoryAccess);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}