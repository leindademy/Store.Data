using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.Data.Entities;
using Store.Data.Entities.OrderEntities;
using Store.Repository.Interfaces;
using Store.Repository.Specifications.OrderSpec;
using Store.Service.Services.BasketService;
using Store.Service.Services.BasketService.Dtos;
using Store.Service.Services.OrderService.Dtos;
using Stripe;
using Product = Store.Data.Entities.Product;

namespace Store.Service.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOFWork _unitOFWork;
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;

        public PaymentService(IConfiguration configuration,IUnitOFWork unitOFWork, IBasketService basketService,IMapper mapper)
        {
            _configuration = configuration;
            _unitOFWork = unitOFWork;
            _basketService = basketService;
            _mapper = mapper;
        }
        public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(CustomerBasketDto basket)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe::SecretKey"];

            if(basket is null)
                throw new Exception("Basket is Empty");

            var deliveryMethod = await _unitOFWork.Repository<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethodId.Value);

            if (deliveryMethod is null)
                throw new Exception("Delivery Method Not Provided");

            decimal ShippingPrice = deliveryMethod.Price;

            //check of The price => [The basket price is the same as the actual product price.]
            foreach (var item in basket.BasketItemDto)
            {
                var product = await _unitOFWork.Repository<Product, int>().GetByIdAsync(item.ProductId);

                if (basket.ShippingPrice != product.Price)
                    basket.ShippingPrice = product.Price;
            }

            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;

            if(string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.BasketItemDto.Sum(item => item.Quantity * (item.Price * 100)) + (long)(ShippingPrice * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                paymentIntent = await service.CreateAsync(options);

                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)basket.BasketItemDto.Sum(item => item.Quantity * (item.Price * 100)) + (long)(ShippingPrice * 100),
                };
                await service.UpdateAsync(basket.PaymentIntentId, options);
            }
            await _basketService.UpdateBasketAsync(basket);
            return basket;
        }
        public async Task<OrderDetailsDto> UpdateOrderPaymentFailed(string paymentIntentId)
        {
            var specs = new OrderWithPaymentIntentSpecification(paymentIntentId);

            var order = await _unitOFWork.Repository<Order, Guid>().GetWithSpecificationByIdAsync(specs);

            if (order is null)
                throw new Exception("Order Does Not Exist");

            order.OrderPaymentStatus = OrderPaymentStatus.Failed;

            _unitOFWork.Repository<Order,Guid>().Update(order);

            await _unitOFWork.CompleteAsync();

            var mappedOrder = _mapper.Map<OrderDetailsDto>(order);

            return mappedOrder;  

        }
        public async Task<OrderDetailsDto> UpdateOrderPaymentSucceeded(string paymentIntentId)
        {
            var specs = new OrderWithPaymentIntentSpecification(paymentIntentId);

            var order = await _unitOFWork.Repository<Order, Guid>().GetWithSpecificationByIdAsync(specs);

            if (order is null)
                throw new Exception("Order Does Not Exist");

            order.OrderPaymentStatus = OrderPaymentStatus.Received;

            _unitOFWork.Repository<Order, Guid>().Update(order);

            await _unitOFWork.CompleteAsync();

            await _basketService.DeleteBasketAsync(order.BasketId); //After the success of the operation, the basket is deleted.

            var mappedOrder = _mapper.Map<OrderDetailsDto>(order);

            return mappedOrder;
        }
    }
}
