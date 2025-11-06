using System.Globalization;
using AutoMapper;
using InventoryApp.Application.DTOs.Auth;
using InventoryApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Application.Features.Users.Queries;
public record UserAutocompleteQuery(string Query) : IRequest<List<UserAutocompleteDto>>;

public class UserAutocompleteQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UserAutocompleteQuery, List<UserAutocompleteDto>>
{
    public async Task<List<UserAutocompleteDto>> Handle(UserAutocompleteQuery request, CancellationToken cancellationToken)
    {
        var query = request.Query.ToLower(CultureInfo.CurrentCulture);
        var users = await unitOfWork.UserRepository.Query(u =>
            EF.Functions.Like(u.Email, $"%{query}%") ||
            EF.Functions.Like(u.FullName, $"%{query}%")).ToListAsync(cancellationToken);
        return mapper.Map<List<UserAutocompleteDto>>(users);
    }
}