using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Shared;

namespace Services.Specifications
{
    public class ProductWithBrandsAndTypesSpecifications :BaseSpecifications<Product,int>
    {
        public ProductWithBrandsAndTypesSpecifications(int id) :base(P=>P.Id==id)
        {
            ApplyInclude();

        }


        public ProductWithBrandsAndTypesSpecifications(ProductSpecificationParamters specparams) 
            :base(
                 P=>
                 (!specparams.BrandId.HasValue||P.BrandId== specparams.BrandId) &&
                 (!specparams.TypeId.HasValue || P.TypeId==specparams.TypeId)
                 )

            
        {
            ApplyInclude();

            ApplySorting(specparams.Sort);
            ApplyPagination(specparams.Pageindex, specparams.Pagesize);
        }

        private void ApplyInclude()
        {
            AddInclude(P => P.productBrand);
            AddInclude(P => P.productType);
        }

        private void ApplySorting(string? sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {
                  
                    case "namedesc":
                        AddOrderByDescending(P=>P.Name);
                        break;
                    case "priceasc":
                        AddOrderBy(P=>P.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(P => P.Price);
                        break;

                    default:
                    
                        AddOrderBy(p => p.Name);
                        break;
                        

                }
            }
            else
            {
                AddOrderBy(p => p.Name);

            }
        }

    }
}
