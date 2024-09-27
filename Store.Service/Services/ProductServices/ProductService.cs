using AutoMapper;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Service.Services.ProductService.ProductServices;
using Store.Service.Services.ProductService.ProductServices.Dtos;
using System.Collections.Generic;
using ProductEntity = Store.Data.Entities.Product;

namespace Store.Service.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IUnitOFWork _unitOFWork ;
        private readonly IMapper _mapper;
        public ProductService(IUnitOFWork unitOFWork, IMapper mapper)
        {
            _unitOFWork = unitOFWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOFWork.Repository<ProductBrand, int>().GetAllAsNotTrackAsinc();
            var mappedBrands = _mapper.Map<IReadOnlyList<BrandTypeDetailsDto>>(brands);
            return mappedBrands;
        }

        public async Task<IReadOnlyList<ProductDetailsDto>> GetAllProductsAsync()
        {
            var products = await _unitOFWork.Repository<ProductEntity, int>().GetAllAsNotTrackAsinc();
            var mappedproducts = _mapper.Map<IReadOnlyList<ProductDetailsDto>>(products);
            return mappedproducts;
        }

        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypesAsync()
        {
            var types = await _unitOFWork.Repository<ProductType, int>().GetAllAsNotTrackAsinc();
            var mappedTypes = types.Select(x => new BrandTypeDetailsDto
            {
                Id = x.id,
                Name = x.Name,
                CreatedAt = x.CreatedAt

            }).ToList();
            return mappedTypes;
        }

        public async Task<ProductDetailsDto> GetProductBtIdAsync(int? productId)
        {
            if (productId == null)
                throw new Exception("ProductId Is Null");
            var product = await _unitOFWork.Repository<ProductEntity, int>().GetByIdAsinc(productId.Value);
            if (product is null)
                throw new Exception("Product Is Null");

            var mappedProduct = _mapper.Map<ProductDetailsDto>(product);
            return mappedProduct;
        }
    }
}
