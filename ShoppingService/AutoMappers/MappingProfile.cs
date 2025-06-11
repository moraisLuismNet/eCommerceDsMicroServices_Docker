using AutoMapper;
using ShoppingService.DTOs;
using ShoppingService.Models;

namespace ShoppingService.AutoMappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CartDetail, CartDetailDTO>();
            CreateMap<CartDetailDTO, CartDetail>();
            CreateMap<Cart, CartDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();
        }

    }
}

