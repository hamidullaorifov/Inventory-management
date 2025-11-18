using InventoryApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Application.Features.Inventories.Queries;
public record TagAutocompleteQuery(string Query) : IRequest<List<string>>;

public class TagAutocompleteQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<TagAutocompleteQuery, List<string>>
{
    public async Task<List<string>> Handle(TagAutocompleteQuery query, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(query.Query))
        {
            return [];
        }

        var pattern = $"%{query.Query}%";
        var tags = await unitOfWork.TagRepository.GetAllAsync(t =>
            EF.Functions.ILike(t.Name, pattern));
        return [.. tags.Select(t => t.Name)];
    }
}