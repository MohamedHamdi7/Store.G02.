using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstraction;
using Services.Specifications;
using Shared.Dto;

namespace Services
{       
                                   // Primary Constructor
    public class ProductService(IUnitOfWork unitOfWork , IMapper mapper   ) : IProductService
    {

       
        public async Task<IEnumerable<ProductResultDto>> GetAllProductAsync(int? brandId, int? typeId, string? sort)
        {
            //Get All Products Throught ProductRepository(unit of work)

            var spec = new ProductWithBrandsAndTypesSpecifications( brandId ,  typeId , sort);
            
            var Products = await unitOfWork.GetRepository<Product,int>().GetAllAsync(spec);

            //convert IEnumerable<Product> To IEnumerable<ProductResultDto>

          var result=   mapper.Map<IEnumerable<ProductResultDto>>(Products);
            return result;
        }

        public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithBrandsAndTypesSpecifications(id);

          var product = await  unitOfWork.GetRepository<Product,int>().GetAsync(spec);

            if(product is null) return null;

          var result=  mapper.Map<ProductResultDto>(product);
            return result;
        }

        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            var Brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();

            var result = mapper.Map<IEnumerable<BrandResultDto>>(Brands);

            return result;
        }



        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
         var Types=  await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();

           var result= mapper.Map<IEnumerable<TypeResultDto>>(Types);
            return result;

        }

       
        // To Call This Services Make To Her Like Unit Of Work Called(ServerManger)

    }
}
