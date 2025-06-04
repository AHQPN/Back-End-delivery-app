using AutoMapper;
using Backend_Mobile_App.Controllers;
using Backend_Mobile_App.DTOs;
using Backend_Mobile_App.Models;

namespace Backend_Mobile_App.Mapping
{
    public class MapConf : Profile
    {
        public MapConf() {
            CreateMap<Order, OrderCreateDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<Payment, PaymentDto>().ReverseMap();
            
        }
    }
}
