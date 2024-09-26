using AutoMapper;
using VinylShop.Core.Models;
using VinylShop.DataAccess.Entities;

namespace VinylShop.DataAccess.Mappings;

public class DataBaseMappings : Profile
{
    public DataBaseMappings()
    {
        CreateMap<OrderEntity, Order>();
        CreateMap<OrderItemEntity, OrderItem>();
        CreateMap<PaymentEntity, Payment>();
        CreateMap<ShipmentEntity, Shipment>();
        CreateMap<UserEntity, User>();
        CreateMap<VinylEntity, Vinyl>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image));;
    }
}