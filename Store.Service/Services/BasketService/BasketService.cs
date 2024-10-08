using AutoMapper;
using Store.Repository.Basket;
using Store.Repository.Basket.Models;
using Store.Service.Services.BasketService.Dtos;
namespace Store.Service.Services.BasketService
{

    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        public BasketService(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
         => await _basketRepository.DeleteBasketAsync(basketId);

        public async Task<CustomerBasketDto> GetBasketAsync(string basketId)
        {
            var basket = _basketRepository.GetBasketAsync(basketId);
            if (basket == null)
                return new CustomerBasketDto();
            var mappedbasket = _mapper.Map<CustomerBasketDto>(basket);
            return mappedbasket;
        }

        public async Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto input)
        {
            if (input.Id is null)
                input.Id = GenerateRandomBasketId();

            var customerBasket = _mapper.Map<CustomerBasket>(input);

            var updatedBasket = await _basketRepository.UpdateBasketAsync(customerBasket);

            var mappedUpdatedBasket = _mapper.Map<CustomerBasketDto>(updatedBasket);

            return mappedUpdatedBasket;
        }
        private string GenerateRandomBasketId()
        {
            Random random = new Random();
            int randomDigit = random.Next(1000, 10000);

            return $"BS - {randomDigit}";
        }
    }
}

