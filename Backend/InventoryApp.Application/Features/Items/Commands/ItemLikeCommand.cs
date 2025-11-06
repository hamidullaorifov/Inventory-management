using InventoryApp.Application.Interfaces.Repositories;
using InventoryApp.Application.Interfaces.Services;
using InventoryApp.Domain.Entities;
using MediatR;

namespace InventoryApp.Application.Features.Items.Commands;
public record ItemLikeCommand(Guid ItemId) : IRequest<Unit>;

public class ItemLikeCommandHandler(
    IUnitOfWork unitOfWork,
    IAuthService authService) : IRequestHandler<ItemLikeCommand, Unit>
{
    public async Task<Unit> Handle(ItemLikeCommand request, CancellationToken cancellationToken)
    {
        var userId = authService.GetAuthenticatedUserId();
        var existingLike = await unitOfWork.ItemLikeRepository.GetByItemAndUserAsync(request.ItemId, userId);
        if (existingLike != null)
        {
            await unitOfWork.ItemLikeRepository.DeleteAsync(existingLike);
        }
        else
        {
            var itemLike = new ItemLike
            {
                ItemId = request.ItemId,
                UserId = userId,
                LikedAt = DateTime.UtcNow
            };
            await unitOfWork.ItemLikeRepository.AddAsync(itemLike);
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
