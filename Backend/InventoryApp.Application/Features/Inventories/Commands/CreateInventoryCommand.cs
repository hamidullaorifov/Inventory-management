using AutoMapper;
using InventoryApp.Application.DTOs.Inventory;
using InventoryApp.Application.Interfaces.Repositories;
using InventoryApp.Application.Interfaces.Services;
using InventoryApp.Domain.Entities;
using MediatR;

namespace InventoryApp.Application.Features.Inventories.Commands;
public record CreateInventoryCommand(InventoryCreateDto Inventory) : IRequest<Guid>;

public class CreateInventoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IAuthService authService) : IRequestHandler<CreateInventoryCommand, Guid>
{
    public async Task<Guid> Handle(CreateInventoryCommand command, CancellationToken cancellationToken)
    {
        var inventory = mapper.Map<Inventory>(command.Inventory);
        inventory.OwnerId = authService.GetAuthenticatedUserId();
        await unitOfWork.InventoryRepository.AddAsync(inventory);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return inventory.Id;
    }
}
