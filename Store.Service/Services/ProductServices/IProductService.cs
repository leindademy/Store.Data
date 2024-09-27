using Store.Service.Services.ProductService.ProductServices.Dtos;
namespace Store.Service.Services.ProductService.ProductServices
{
    public interface IProductService
    {
        Task<ProductDetailsDto> GetProductBtIdAsync(int? productId);
        Task<IReadOnlyList<ProductDetailsDto>> GetAllProductsAsync();
        Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandsAsync();
        Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypesAsync();
    }
}
