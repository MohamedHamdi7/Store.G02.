using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dto;

namespace Services.Abstraction
{
    public interface IBasketServices
    {
        Task<BasketDto?> GetAllBasketAsync(string id);
        Task<BasketDto?> UpdateBasketAsync(BasketDto basketDto);
        Task<bool> DeleteBasketasync(string id);

    }
}
