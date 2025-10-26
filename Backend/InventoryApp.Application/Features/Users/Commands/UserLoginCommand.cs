using InventoryApp.Application.DTOs.Auth;
using InventoryApp.Application.Interfaces.Repositories;
using InventoryApp.Application.Interfaces.Services;
using MediatR;

namespace InventoryApp.Application.Features.Users.Commands;
public record UserLoginCommand : IRequest<LoginResponseDto>
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}

public class UserLoginCommandHandler(IUnitOfWork unitOfWork, IAuthService authService) : IRequestHandler<UserLoginCommand, LoginResponseDto>
{
    public async Task<LoginResponseDto> Handle(UserLoginCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetByEmailAsync(request.Email)
            ?? throw new UnauthorizedAccessException("Invalid email or password.");
        var result = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        if (!result)
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        if (user.IsBlocked)
        {
            throw new UnauthorizedAccessException("User is blocked.");
        }

        var token = authService.GenerateToken(user);

        return new LoginResponseDto
        {
            Token = token,
            FullName = user.FullName,
            Email = user.Email,
            Language = user.Language,
            Theme = user.Theme,
            ProfilePictureUrl = user.ProfilePictureUrl
        };
    }
}