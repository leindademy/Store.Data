using Microsoft.AspNetCore.Mvc;
using Store.Repository.Interfaces;
using Store.Repository.Repositories;
using Store.Service.Services.ProductService.ProductServices.Dtos;
using Store.Service.Services.ProductService.ProductServices;
using Store.Service.Services.ProductServices;
using Store.Service.HandleResponses;
using Store.Service.Services.CashService;
using Store.Service.Services.BasketService.Dtos;
using Store.Service.Services.BasketService;
using Store.Repository.Basket;
using Store.Service.Services.TokenService;
using Store.Service.Services.UserService;
using Store.Service.Services.OrderService.Dtos;
using Store.Service.Services.OrderService;
using Store.Service.Services.PaymentService;

namespace Store.Web.Extentions
{
    public static class ApplicationServiceExtention
    { 
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOFWork, UnitOFWork>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICashService, CashService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddAutoMapper(typeof(ProductProfile));
            services.AddAutoMapper(typeof(BasketProfile));
            services.AddAutoMapper(typeof(OrderProfile));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                                .Where(model => model.Value?.Errors.Count > 0)
                                .SelectMany(model => model.Value.Errors)
                                .Select(error => error.ErrorMessage)
                                .ToList();

                    var eroorsResponse = new ValidationErrorRespose
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(eroorsResponse);
                };
            });
            return services;
        }
    }
}
