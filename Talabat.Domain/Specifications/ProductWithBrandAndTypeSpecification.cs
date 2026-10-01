using Talabat.Domain.Entities;
using Talabat.Domain.Specifications.OrderSpecification;

namespace Talabat.Domain.Specifications;

public class ProductWithBrandAndTypeSpecification : BaseSpecifications<Product>
{

    public ProductWithBrandAndTypeSpecification(ProductSpecParams specParams) : base(
        p => string.IsNullOrEmpty(specParams.Search) || p.Name.ToLower().Contains(specParams.Search)
    )
    {
        Includes.Add(p => p.productType);
        Includes.Add(p => p.productBrand);
        if (!string.IsNullOrEmpty(specParams.SortBy))
        {
            switch (specParams.SortBy)
            {
                case "priceAsc":
                    AddOrderBy(p => p.Price);
                    break; 
                case "priceDesc":
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    AddOrderBy(p => p.Name);
                    break;
            }
        }
        //skip and take for paging
        ApplyPaging((specParams.PageIndex - 1) * specParams.PageSize, specParams.PageSize);
        
        if (specParams.BrandId.HasValue)
        {
            Criteria = p => p.productBrandId == specParams.BrandId;
        }

        if (specParams.TypeId.HasValue)
        {
            Criteria = p => p.productTypeId == specParams.TypeId;
        }

    }

    public ProductWithBrandAndTypeSpecification (int id):base(p=>p.Id==id)
    {
        Includes.Add(p => p.productType);
        Includes.Add(p => p.productBrand);


    }

}
