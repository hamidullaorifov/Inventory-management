using AutoMapper;
using InventoryApp.Application.DTOs.Auth;
using InventoryApp.Domain.Entities;

namespace InventoryApp.Application.Mappings;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterDto, User>();
        CreateMap<User, UserAutocompleteDto>();
        CreateMap<User, UserAccessDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));
    }
}
