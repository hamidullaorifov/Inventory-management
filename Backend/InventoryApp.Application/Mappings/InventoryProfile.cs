using AutoMapper;
using InventoryApp.Application.DTOs.Inventory;
using InventoryApp.Domain.Entities;

namespace InventoryApp.Application.Mappings;
public class InventoryProfile : Profile
{
    public InventoryProfile()
    {
        CreateMap<Inventory, InventoryListDto>();
        CreateMap<InventoryCreateDto, Inventory>();
    }
}
