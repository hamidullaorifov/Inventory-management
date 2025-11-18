using AutoMapper;
using InventoryApp.Application.DTOs.Inventory;
using InventoryApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Application.Features.Inventories.Queries;
public record GetInventoryCategoriesQuery : IRequest<List<InventoryCategoryDto>>;

public class GetInventoryCategoriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetInventoryCategoriesQuery, List<InventoryCategoryDto>>
{
    public async Task<List<InventoryCategoryDto>> Handle(GetInventoryCategoriesQuery query, CancellationToken cancellationToken)
    {
        var categories = await unitOfWork.CategoryRepository.Query().ToListAsync(cancellationToken);
        return mapper.Map<List<InventoryCategoryDto>>(categories);
    }
}


