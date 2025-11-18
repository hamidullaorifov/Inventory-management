using AutoMapper;
using InventoryApp.Application.DTOs.Auth;
using InventoryApp.Application.Interfaces.Repositories;
using MediatR;

namespace InventoryApp.Application.Features.Inventories.Queries;
public record GetInventoryAccessQuery(Guid InventoryId) : IRequest<List<UserAccessDto>>;

public class GetInventoryAccessQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetInventoryAccessQuery, List<UserAccessDto>>
{
    public async Task<List<UserAccessDto>> Handle(GetInventoryAccessQuery request, CancellationToken cancellationToken)
    {
        var accessList = await unitOfWork.InventoryAccessRepository.GetUsersWithAccessToInventoryAsync(request.InventoryId);
        return mapper.Map<List<UserAccessDto>>(accessList);
    }
}
