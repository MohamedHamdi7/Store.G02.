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
using Services.Specifications;
using Shared;
using Shared.Dto;

namespace Services
{       
                                   // Primary Constructor
    public class ProductService(IUnitOfWork unitOfWork , IMapper mapper   ) : IProductService
    {


        //public async Task<IEnumerable<ProductResultDto>> GetAllProductAsync(int? brandId, int? typeId, string? sort, int Pageindex = 1, int Pagesize = 1)
        public async Task<PaginationResponse<ProductResultDto>> GetAllProductAsync(ProductSpecificationParamters specparams)

        {
            //Get All Products Throught ProductRepository(unit of work)

            //var spec = new ProductWithBrandsAndTypesSpecifications( brandId ,  typeId , sort, Pageindex,Pagesize);
            var spec = new ProductWithBrandsAndTypesSpecifications(specparams);


            var Products = await unitOfWork.GetRepository<Product,int>().GetAllAsync(spec);

            var SpecCount = new ProductWithCountSpecification(specparams);

            var Count = await unitOfWork.GetRepository<Product,int>().CountAsync(SpecCount);

            //convert IEnumerable<Product> To IEnumerable<ProductResultDto>

          var result=   mapper.Map<IEnumerable<ProductResultDto>>(Products);

            return new PaginationResponse<ProductResultDto>(specparams.Pageindex, specparams.Pagesize, Count, result);
        }

        public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithBrandsAndTypesSpecifications(id);

          var product = await  unitOfWork.GetRepository<Product,int>().GetAsync(spec);

            if(product is null) throw new ProductNotFoundException( id);

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
