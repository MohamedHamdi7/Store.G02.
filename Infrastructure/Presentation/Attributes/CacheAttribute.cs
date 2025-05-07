using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction;

namespace Presentation.Attributes
{
    
    public class CacheAttribute (int durationInSec): Attribute, IAsyncActionFilter
    {
        
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
          var cacheService=  context.HttpContext.RequestServices.GetRequiredService<IServiceManger>().cacheServices;
          var cacheKey=Generatecache(context.HttpContext.Request);
          var result = await cacheService.GetCacheValueAsync(cacheKey);
          if (!string.IsNullOrEmpty(result))
          {
                //Return Response
                context.Result = new ContentResult()
                {
                    ContentType="application/json",
                    StatusCode=StatusCodes.Status200OK,
                    Content = result
                };
                return;
          }

            //Excute The End Point
           var contextResult= await next.Invoke();
            if(contextResult.Result is OkObjectResult okObject)
            {
               await  cacheService.SetCacheValueAsync(cacheKey, okObject.Value, TimeSpan.FromSeconds(durationInSec));
            }

        }


        private string Generatecache(HttpRequest Request)
        {
            var Key = new StringBuilder();
            Key.Append(Request.Path);
            foreach (var item in Request.Query.OrderBy(q => q.Key))
            {
                Key.Append($"|{item.Key}-{item.Value}");
            }
            return Key.ToString();
        }


        //api/Products? sort >>Key
        //api/Products|sort-nameasc|type-1|brand-1| >>value
    }
}
