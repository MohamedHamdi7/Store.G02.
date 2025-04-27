using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Services.Abstraction;
using Shared.Dto;

namespace Services
{
    public class BasketServices(IBasketRepository basketRepository,IMapper mapper) : IBasketServices
    {

        public async Task<BasketDto?> GetAllBasketAsync(string id)
        {
          var basket= await basketRepository.GetBasketAsync(id);
            if (basket is null) throw new BasketNotFoundException(id);
          var result=  mapper.Map<BasketDto>(basket);
            return result;

        }

        public async Task<BasketDto?> UpdateBasketAsync(BasketDto basketDto)
        {
            var basket=  mapper.Map<CustomerBasket>(basketDto);
           basket= await basketRepository.UpdateBasketAsync(basket);
            if (basket is null) throw new BasketCreateOrUpdateBadRequestExceptions();
           var result= mapper.Map<BasketDto>(basket);
            return result;

        }
        public async Task<bool> DeleteBasketasync(string id)
        {
          var flag= await basketRepository.DeleteBasketAsync(id);
            if (flag == false) throw new BasketDeleteBadRequestExceptions();
            return flag;
        }

    }
}
