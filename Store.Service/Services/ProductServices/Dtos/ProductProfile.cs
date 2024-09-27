using AutoMapper;
using Store.Data.Entities;
using Store.Service.Services.ProductServices.Dtos;
namespace Store.Service.Services.ProductService.ProductServices.Dtos
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDetailsDto>()
                .ForMember(des => des.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(des => des.TypeName, opt => opt.MapFrom(src => src.Type.Name))
                .ForMember(des => des.PictureUrl, opt => opt.MapFrom<ProductPictureUrlResolver>());


            CreateMap<ProductBrand, BrandTypeDetailsDto>();
            CreateMap<ProductType, BrandTypeDetailsDto>();
        }
    }
}
