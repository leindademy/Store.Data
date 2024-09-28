using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specifications.ProductSpecs
{
    public class ProductWithCountSpecification : BaseSpecifications<Product>

    {
        public ProductWithCountSpecification(ProductSpecification spec)
            : base(Product => (!spec.BrandId.HasValue || Product.BrandId == spec.BrandId.Value) &&
                              (!spec.TypeId.HasValue || Product.TypeId == spec.TypeId.Value) &&
                              (string.IsNullOrEmpty(spec.Search) || Product.Name.Trim().ToLower().Contains(spec.Search))



       )
        {

        }
    }
}
