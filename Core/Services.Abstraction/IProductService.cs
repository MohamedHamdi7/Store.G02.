using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dto;

namespace Services.Abstraction
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResultDto>> GetAllProductAsync(int? brandId, int? typeId);

        Task<ProductResultDto?> GetProductByIdAsync(int id);

        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();


    }
}
