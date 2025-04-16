using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using Shared.Dto;

namespace Services.Abstraction
{
    public interface IProductService
    {
        //Task<IEnumerable<ProductResultDto>> GetAllProductAsync(int? brandId, int? typeId , string? sort, int Pageindex = 1, int Pagesize = 1);
        Task<IEnumerable<ProductResultDto>> GetAllProductAsync(ProductSpecificationParamters specparams);

        Task<ProductResultDto?> GetProductByIdAsync(int id);

        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();


    }
}
