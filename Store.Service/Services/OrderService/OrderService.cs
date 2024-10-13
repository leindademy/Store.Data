using AutoMapper;
using Store.Data.Entities;
using Store.Data.Entities.OrderEntities;
using Store.Repository.Interfaces;
using Store.Repository.Specifications.OrderSpec;
using Store.Service.Services.BasketService;
using Store.Service.Services.OrderService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IBasketService _basketService;
        private readonly IUnitOFWork _unitOFWork;
        private readonly IMapper _mapper;

        public OrderService(IBasketService basketService,IUnitOFWork unitOFWork,IMapper mapper)
        {
            _basketService = basketService;
            _unitOFWork = unitOFWork;
            _mapper = mapper;
        }
        public async Task<OrderDetailsDto> CreateOrderAsync(OrderDto input)
        {
           //Get Basket
           var basket = await _basketService.GetBasketAsync(input.BasketId);

            if(basket is null)
                throw new Exception("Basket Not Exist");


            #region Fill Order Item List With Items In Basket
            var orderItems = new List<OrderItemDto>();

            foreach (var basketItem in basket.BasketItemDto)
            {
                var productItem = await _unitOFWork.Repository<Product, int>().GetByIdAsync(basketItem.ProductId);

                if (productItem is null)
                    throw new Exception($"Product Eith Id : {basketItem.ProductId} Not Exist");

                var itemOrdered = new ProductItem
                {
                    ProductId = productItem.id,
                    ProductName = productItem.PictureUrl,
                    PictureUrl = productItem.PictureUrl
                };
                var orderItem = new OrderItem
                {
                    Price = productItem.Price,
                    Quantity = basketItem.Quantity,
                    ProductItem = itemOrdered
                };
                var mappedaOrderItem = _mapper.Map<OrderItemDto>(orderItem);

                orderItems.Add(mappedaOrderItem);



            }
            #endregion

            #region Get Delivery Method
            var deliveryMethod = await _unitOFWork.Repository<DeliveryMethod, int>().GetByIdAsync(input.DeliveryMethodId);

            if (deliveryMethod is null)
                throw new Exception("Delivery Method Not Provided");

            #endregion

            #region Calculate SubTotal
            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);
            #endregion

            #region To Do ===>> Payment
            #endregion

            #region Create Order
            var mappedShippingAddress = _mapper.Map<ShippingAddress>(input.ShippingAddress);

            var mappedOrderItems = _mapper.Map<List<OrderItem>>(orderItems);

            var order = new Order
            {
                DeliveryMethodId = deliveryMethod.id,
                OrderItems = mappedOrderItems,
                SubTotal = subTotal,
                BasketId = input.BasketId,
                BuyerEmail = input.BuyerEmail,
                ShippingAddress = mappedShippingAddress,
            };
            await _unitOFWork.Repository<Order, Guid>().TaskAddAsync(order);

            await _unitOFWork.CompleteAsync();

            var mappedOrder = _mapper.Map<OrderDetailsDto>(order);

            return mappedOrder;
            #endregion

        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodsAsync()
        
          =>  await _unitOFWork.Repository<DeliveryMethod, int>().GetAllAsync();
        

        public async Task<OrderDetailsDto> GetOrderByIdAsync(Guid id)
        {
            var specs = new OrderWithItemSpecification(id);

            var orders = await _unitOFWork.Repository<Order, Guid>().GetWithSpecificationByIdAsync(specs);

            if (orders is null)
                throw new Exception($"There Is Order With Id {id}");

            var mappedOrders = _mapper.Map<OrderDetailsDto> (orders);

            return mappedOrders;
        }

        public async Task<IReadOnlyList<OrderDetailsDto>> GetAllOrdersForUserAsync(string buyerEmail)
        {
            var specs = new OrderWithItemSpecification(buyerEmail);

            var orders =await _unitOFWork.Repository<Order, Guid>().GetAllWithSpecificationAsync(specs);

            if (!orders.Any())
                throw new Exception("You Don't Have Any Orders !");

            var mappedOrders = _mapper.Map<List<OrderDetailsDto>>(orders);

            return mappedOrders;


        }
    }
}
