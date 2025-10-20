using AutoMapper;
using InventoryApp.Application.DTOs.Auth;
using InventoryApp.Domain.Entities;

namespace InventoryApp.Application.Mappings;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterDto, User>();
    }
}
