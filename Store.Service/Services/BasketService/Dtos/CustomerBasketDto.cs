 using Store.Repository.Basket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.BasketService.Dtos
{
    public class CustomerBasketDto
    {
        public string?  Id { get; set; }
        public int? DeliveryMethodId { get; set; }
        public double ShippingPrice { get; set; }
        public List<BasketItemDto> BasketItemDto {get; set; } = new List<BasketItemDto>();
        public string? BasketId { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; } // stripe payment 
    }
}
