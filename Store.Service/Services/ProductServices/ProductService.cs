using AutoMapper;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Repository.Specifications.ProductSpecs;
using Store.Service.Helper;
using Store.Service.Services.ProductService.ProductServices;
using Store.Service.Services.ProductService.ProductServices.Dtos;
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
            var brands = await _unitOFWork.Repository<ProductBrand, int>().GetAllAsync();
            var mappedBrands = _mapper.Map<IReadOnlyList<BrandTypeDetailsDto>>(brands);
            return mappedBrands;
        }

        public async Task<PaginatedResultDto<ProductDetailsDto>> GetAllProductsAsync(ProductSpecification input)
        {
            var spec = new ProductWithSpecifications(input);

            var products = await _unitOFWork.Repository<ProductEntity, int>().GetAllWithSpecificationAsync(spec);

            var CountSpecs = new ProductWithCountSpecification(input);

            var Count = await _unitOFWork.Repository<ProductEntity, int>().GetCountAsync(CountSpecs);

            var mappedproducts = _mapper.Map<IReadOnlyList<ProductDetailsDto>>(products);

            return new PaginatedResultDto<ProductDetailsDto>(input.PageSize, input.PageIndex,Count, mappedproducts);
        }

        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypesAsync()
        {
            var types = await _unitOFWork.Repository<ProductType, int>().GetAllAsync();
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
            var spec = new ProductWithSpecifications(productId);

            var product = await _unitOFWork.Repository<ProductEntity, int>().GetWithSpecificationByIdAsync(spec);
            if (product is null)
                throw new Exception("Product Is Null");

            var mappedProduct = _mapper.Map<ProductDetailsDto>(product);
            return mappedProduct;
        }
    }
}
