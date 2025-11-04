using AutoMapper;
using InventoryApp.Application.DTOs.Inventory;
using InventoryApp.Application.Interfaces.Repositories;
using MediatR;

namespace InventoryApp.Application.Features.Inventories.Queries;
public record GetInventoryByIdQuery(Guid InventoryId) : IRequest<InventoryDetailsDto>;

public class GetInventoryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetInventoryByIdQuery, InventoryDetailsDto>
{
    public async Task<InventoryDetailsDto> Handle(GetInventoryByIdQuery request, CancellationToken cancellationToken)
    {
        var inventory = await unitOfWork.InventoryRepository.GetDetailsAsync(request.InventoryId);
        return mapper.Map<InventoryDetailsDto>(inventory);
    }
}
