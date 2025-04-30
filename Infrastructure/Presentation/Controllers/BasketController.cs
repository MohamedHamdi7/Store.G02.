using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.Dto;

namespace Presentation.Controllers
{
    //
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController(IServiceManger serviceManger) :ControllerBase
    {
        
        [HttpGet]
        public async Task<IActionResult>GetAllBasketAsync(string id)
        {
          var result= await serviceManger.basketServices.GetAllBasketAsync(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBasketAsync(BasketDto basket)
        {
            var result = await serviceManger.basketServices.UpdateBasketAsync(basket);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult>DeleteBasketAsync(string id)
        {
            serviceManger.basketServices.DeleteBasketasync(id);
            return NoContent();//204
        }
    }
}
