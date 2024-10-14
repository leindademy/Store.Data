using AutoMapper;
using Store.Data.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.OrderService.Dtos
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<ShippingAddress, AddressDto>().ReverseMap();

            CreateMap<Order, OrderDetailsDto>()
                 .ForMember(des => des.DeliveryMethodName, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
                 .ForMember(des => des.ShippingPrice, opt => opt.MapFrom(src => src.DeliveryMethod.Price));

            CreateMap<OrderItem, OrderItemDto>()
                 .ForMember(des => des.ProductItemId, opt => opt.MapFrom(src => src.ProductItem.ProductId))
                 .ForMember(des => des.ProductName, opt => opt.MapFrom(src => src.ProductItem.ProductName))
                 .ForMember(des => des.PictureUrl, opt => opt.MapFrom(src => src.ProductItem.PictureUrl))
                 .ForMember(des => des.ProductName, opt => opt.MapFrom<OrderItemPictureUrlResolver>()).ReverseMap();
        }
    }
}
