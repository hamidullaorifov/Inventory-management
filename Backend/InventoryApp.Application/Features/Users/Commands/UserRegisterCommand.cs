using AutoMapper;
using InventoryApp.Application.DTOs.Auth;
using InventoryApp.Application.Interfaces.Repositories;
using InventoryApp.Domain.Entities;
using MediatR;

namespace InventoryApp.Application.Features.Users.Commands;
public record UserRegisterCommand(RegisterDto RegisterDto) : IRequest<Guid>;

public class UserRegisterCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UserRegisterCommand, Guid>
{
    public async Task<Guid> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    { 
        var user = mapper.Map<User>(request.RegisterDto);
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.RegisterDto.Password);
        
        await unitOfWork.UserRepository.AddAsync(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        // TODO: Send confirmation email
        return user.Id;
    }
}
