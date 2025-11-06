using AutoMapper;
using InventoryApp.Application.DTOs.Inventory;
using InventoryApp.Application.DTOs.Item;
using InventoryApp.Domain.Entities;

namespace InventoryApp.Application.Mappings;
public class InventoryProfile : Profile
{
    public InventoryProfile()
    {
        CreateMap<Inventory, InventoryListDto>();
        CreateMap<InventoryCreateDto, Inventory>()
            .ForMember(dest => dest.Tags, opt => opt.Ignore());
        CreateMap<Inventory, InventoryDetailsDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.Owner.FullName))
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(t => t.Name)));
        CreateMap<InventoryUpdateDto, Inventory>()
        .ForMember(dest => dest.Tags, opt => opt.Ignore());
        CreateMap<InventoryFieldDefinition, InventoryFieldDto>();

        // Inventory Field Definitions
        CreateMap<InventoryFieldCreateDto, InventoryFieldDefinition>();
        CreateMap<InventoryFieldUpdateDto, InventoryFieldDefinition>();
        CreateMap<FieldValueDto, ItemFieldValue>();
        CreateMap<Item, ItemDetailsDto>();
        CreateMap<ItemFieldValue, FieldValueDto>();

    }
}
