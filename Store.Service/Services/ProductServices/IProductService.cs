using Store.Repository.Specifications.ProductSpecs;
using Store.Service.Helper;
using Store.Service.Services.ProductService.ProductServices.Dtos;
namespace Store.Service.Services.ProductService.ProductServices
{
    public interface IProductService
    {
        Task<ProductDetailsDto> GetProductBtIdAsync(int? productId);
        Task<PaginatedResultDto<ProductDetailsDto>> GetAllProductsAsync(ProductSpecification spec);
        Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandsAsync();
        Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypesAsync();
    }
}
