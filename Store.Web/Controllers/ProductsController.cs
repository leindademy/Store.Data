using Microsoft.AspNetCore.Mvc;
using Store.Repository.Specifications.ProductSpecs;
using Store.Service.Helper;
using Store.Service.Services.ProductService.ProductServices;
using Store.Service.Services.ProductService.ProductServices.Dtos;
using Store.Web.Helper;

namespace Store.Web.Controllers
{
    
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        
        
        public async Task<ActionResult<IReadOnlyList<BrandTypeDetailsDto>>>GetAllBrands()
            =>Ok(await _productService.GetAllBrandsAsync());

        [HttpGet]
        
        public async Task<ActionResult<IReadOnlyList<BrandTypeDetailsDto>>> GetAllTypes()
            => Ok(await _productService.GetAllTypesAsync());

        [HttpGet]
        [Cache(50)]
        public async Task<PaginatedResultDto<ProductDetailsDto>> GetAllProducts([FromQuery] ProductSpecification input)
            => (await _productService.GetAllProductsAsync(input));

        [HttpGet]
        public async Task<ActionResult<ProductDetailsDto>> GetAllById(int? id)  
          => Ok(await _productService.GetProductBtIdAsync(id));

       

    }
}
