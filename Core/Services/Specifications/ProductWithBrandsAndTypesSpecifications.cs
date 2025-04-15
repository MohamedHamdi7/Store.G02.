using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Services.Specifications
{
    public class ProductWithBrandsAndTypesSpecifications :BaseSpecifications<Product,int>
    {
        public ProductWithBrandsAndTypesSpecifications(int id) :base(P=>P.Id==id)
        {
            ApplyInclude();

        }


        public ProductWithBrandsAndTypesSpecifications() :base(null)
        {
            ApplyInclude();
        }

        private void ApplyInclude()
        {
            AddInclude(P => P.productBrand);
            AddInclude(P => P.productType);
        }

    }
}
