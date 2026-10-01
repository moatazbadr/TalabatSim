using Talabat.Domain.Entities;
using Talabat.Domain.Specifications.OrderSpecification;

namespace Talabat.Domain.Specifications
{
    public class ProductWithFiltrationForCountAsync : BaseSpecifications<Product>
    {
        public ProductWithFiltrationForCountAsync(ProductSpecParams specParams) : base(
            p => string.IsNullOrEmpty(specParams.Search) || p.Name.ToLower().Contains(specParams.Search)
        )
        {

            if (specParams.BrandId.HasValue)
            {
                Criteria = p => p.productBrandId == specParams.BrandId;
            }
            if (specParams.TypeId.HasValue)
            {
                Criteria = p => p.productTypeId == specParams.TypeId;
            }
        }
    }
}
