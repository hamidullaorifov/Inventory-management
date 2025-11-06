using InventoryApp.Application.Interfaces.Repositories;
using MediatR;

namespace InventoryApp.Application.Features.Inventories.Commands;
public record RemoveInventoryAccessCommand(Guid InventoryId, Guid UserId) : IRequest<Unit>;

public class RemoveInventoryAccessCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<RemoveInventoryAccessCommand, Unit>
{
    public async Task<Unit> Handle(RemoveInventoryAccessCommand request, CancellationToken cancellationToken)
    {
        var access = await unitOfWork.InventoryAccessRepository.GetByUserAndInventoryAsync(request.UserId, request.InventoryId);
        if (access == null)
        {
            return Unit.Value;
        }

        await unitOfWork.InventoryAccessRepository.DeleteAsync(access);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}