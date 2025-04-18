﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Services.Abstraction;
using Shared;
using Shared.Dto;

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

        //public async Task<IActionResult> GetAllProducts(int? brandId, int? typeId, string? sort, int Pageindex = 1, int Pagesize = 1)
        public async Task<IActionResult> GetAllProducts([FromQuery]ProductSpecificationParamters specparams)
        {
            //var result = await serviceManger.productService.GetAllProductAsync(brandId,typeId,sort,Pageindex,Pagesize);

            var result = await serviceManger.productService.GetAllProductAsync(specparams);

            if (result is null) return BadRequest();  //StutasCode 400
            return Ok(result); //200 
        }


        [HttpGet("{id}")] // Get>> api/products/id
        public async Task<IActionResult> GetProductById(int id)
        {
          var result= await serviceManger.productService.GetProductByIdAsync(id);
            //if (result is null) return BadRequest();
            return Ok(result);
        }

        [HttpGet("Brands")] //Get:> /api/products/Brands
        public async Task<IActionResult> GetAllBrandAsync()
        {
          var result= await serviceManger.productService.GetAllBrandsAsync();
            if(result is null) return BadRequest();
            return Ok(result);
        }


        [HttpGet("Types")]  // Get:> api/products/Types
        public async Task<IActionResult> GetAllTypesAsync()
        {
          var result= await serviceManger.productService.GetAllTypesAsync();
            if (result is null) return BadRequest();
            return Ok(result);
        }

    }
}
