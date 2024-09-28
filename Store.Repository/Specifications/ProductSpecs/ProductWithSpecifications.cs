using Store.Data.Entities;

namespace Store.Repository.Specifications.ProductSpecs
{
    public class ProductWithSpecifications : BaseSpecifications<Product>
    {
        public ProductWithSpecifications(ProductSpecification spec) 
            : base(Product => (!spec.BrandId.HasValue || Product.BrandId == spec.BrandId.Value) &&
                              (!spec.TypeId.HasValue || Product.TypeId == spec.TypeId.Value) &&                                                                                
                              (string.IsNullOrEmpty(spec.Search) || Product.Name.Trim().ToLower().Contains(spec.Search))
        )
        {
            AddInclude(x => x.Brand);
            AddInclude(x => x.Type);
            AddOrderBy(x => x.Name);

            ApplyPagination(spec.PageSize * (spec.PageIndex - 1), spec.PageSize);

            if (string.IsNullOrEmpty(spec.Sort))
            {
                switch (spec.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }
        }

        public ProductWithSpecifications(int? id) : base(Product => Product.id == id) //GetById

        {
            AddInclude(x => x.Brand);
            AddInclude(x => x.Type);

        }
    }
}
