using AutoMapper;
using AutoMapper.QueryableExtensions;
using InventoryApp.Application.Common.Models;
using InventoryApp.Application.DTOs.Inventory;
using InventoryApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Application.Features.Inventories.Queries;
public record GetInventoriesQuery : IRequest<PagedResult<InventoryListDto>>
{
    public string? Search { get; set; }
    public int? Limit { get; set; }
    public int? Offset { get; set; }
}
public class GetInventoriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetInventoriesQuery, PagedResult<InventoryListDto>>
{
    public async Task<PagedResult<InventoryListDto>> Handle(GetInventoriesQuery request, CancellationToken cancellationToken)
    {
        var query = unitOfWork.InventoryRepository.Query();
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            query = query.Where(i =>
            EF.Functions.ILike(i.Name, $"%{request.Search}%") || EF.Functions.Like(i.Description, $"%{request.Search}%"));
        }
        var totalCount = await query.CountAsync(cancellationToken);
        if (request.Offset.HasValue && request.Offset > 0)
        {
            query = query.Skip(request.Offset.Value);
        }
        if (request.Limit.HasValue && request.Limit > 0)
        {
            query = query.Take(request.Limit.Value);
        }
        var items = await query
            .ProjectTo<InventoryListDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        return new PagedResult<InventoryListDto>(items, totalCount);
    }
}
