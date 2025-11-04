using InventoryApp.Application.Common.Exceptions;
using InventoryApp.Application.Interfaces.Repositories;
using InventoryApp.Domain.Entities;
using MediatR;

namespace InventoryApp.Application.Features.Inventories.Commands;

public record DeleteInventoryCommand(Guid Id) : IRequest<Unit>;

public class DeleteInventoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteInventoryCommand, Unit>
{

    public async Task<Unit> Handle(DeleteInventoryCommand request, CancellationToken cancellationToken)
    {
        var inventory = await unitOfWork.InventoryRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Inventory), request.Id);
        // TODO: Check user permissions here
        await unitOfWork.InventoryRepository.DeleteAsync(inventory);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
