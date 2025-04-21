using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Services.Abstraction;
using Shared;
using Shared.Dto;
using Shared.ErrorModels;

namespace Presentation.Controllers
{
    //Sorting
    //sort:nameasc[default]
    //sort:namedesc
    //sort:priceasc
    //sort:pricedesc



    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManger serviceManger) :ControllerBase
    {
        //Endpoint Public Non-Static Method


        [HttpGet]  //Get >> /api/Products
        [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(PaginationResponse<ProductResultDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError,Type=typeof(ValidationErrorsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest,Type=typeof(ErrorDetails))]
        public async Task<ActionResult<PaginationResponse<ProductResultDto>>> GetAllProducts([FromQuery]ProductSpecificationParamters specparams)
            //public async Task<IActionResult> GetAllProducts(int? brandId, int? typeId, string? sort, int Pageindex = 1, int Pagesize = 1)
        {
            //var result = await serviceManger.productService.GetAllProductAsync(brandId,typeId,sort,Pageindex,Pagesize);

            var result = await serviceManger.productService.GetAllProductAsync(specparams);

            //if (result is null) return BadRequest();  //StutasCode 400
            return Ok(result); //200 
        }


        [HttpGet("{id}")] // Get>> api/products/id
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductResultDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<ProductResultDto>> GetProductById(int id)
        {
          var result= await serviceManger.productService.GetProductByIdAsync(id);
            //if (result is null) return BadRequest();
            return Ok(result);
        }

        [HttpGet("Brands")] //Get:> /api/products/Brands
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BrandResultDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrandAsync()
        {
          var result= await serviceManger.productService.GetAllBrandsAsync();
            if(result is null) return BadRequest();
            return Ok(result);
        }


        [HttpGet("Types")]  // Get:> api/products/Types
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TypeResultDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypesAsync()
        {
          var result= await serviceManger.productService.GetAllTypesAsync();
            if (result is null) return BadRequest();
            return Ok(result);
        }

    }
}
