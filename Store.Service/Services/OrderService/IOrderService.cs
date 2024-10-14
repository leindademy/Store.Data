using Store.Data.Entities;
using Store.Service.Services.OrderService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.OrderService
{
    public interface IOrderService
    {
        Task<OrderDetailsDto> CreateOrderAsync(OrderDto input); // Create Order
        Task<IReadOnlyList<OrderDetailsDto>> GetAllOrdersForUserAsync(string buyerEmail);//Retrieve all orders for a specific user
        Task<OrderDetailsDto> GetOrderByIdAndEmailAsync(Guid id, string? email); // Retrieve Order By Id
        Task <IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodsAsync();//Retrieve DeliveryMethod

    }
}
