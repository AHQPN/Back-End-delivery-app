using AutoMapper;
using Backend_Mobile_App.Controllers;
using Backend_Mobile_App.DTOs;
using Backend_Mobile_App.Models;

namespace Backend_Mobile_App.Mapping
{
    public class MapConf : Profile
    {
        public MapConf() {
            CreateMap<Order, OrderCreateDto>()
            .ForMember(dest => dest.SourceLocation, opt => opt.MapFrom(src => src.SourceLocation))
            .ForMember(dest => dest.DestinationLocation, opt => opt.MapFrom(src => src.DestinationLocation))
            .ReverseMap()
            .ForMember(dest => dest.SourceLocation, opt => opt.Ignore()) 
            .ForMember(dest => dest.DestinationLocation, opt => opt.Ignore());

            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<Payment, PaymentDto>().ReverseMap();
            CreateMap<Location, LocationDTO>().ReverseMap();
        }
    }
}
