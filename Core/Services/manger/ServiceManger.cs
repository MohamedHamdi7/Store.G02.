using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Services.Abstraction;

namespace Services.manger
{
    public class ServiceManger(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IBasketRepository basketRepository
        ) : IServiceManger
    {
        public IProductService productService { get; } = new ProductService(unitOfWork, mapper); //prop

        public IBasketServices basketServices { get; } = new BasketServices(basketRepository, mapper);
    }
}
