using Azure;
using Domain.Exceptions;
using Shared.ErrorModels;

namespace Store.G02.Api.MiddleWare
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> logger; //Save The Exception

        public GlobalErrorHandlingMiddleware(RequestDelegate _next,ILogger<GlobalErrorHandlingMiddleware> _logger) //RequestDelegate >>Datatype carry Address next fun
        {
            next = _next;
            this.logger = _logger;
        }


        public async Task InvokeAsync(HttpContext context)  //HttpContext>>Datatype carry The Request And Respose current fun
        {
            try
            {
               await next.Invoke(context);

                // Handling EndPoint Not Found 

                if (context.Response.StatusCode == 404)
                {
                    await HandlingNotFoundEndPointAsync(context);
                }



            }
            catch (Exception ex)
            {

                // log Exption
                logger.LogError(ex, ex.Message);

                await HandlingErrorAsync(context, ex);

            }



        }


        // HandlingEndPoint (NotFound)
        private static async Task HandlingNotFoundEndPointAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            var Response = new ErrorDetails()
            {
                StatusCode = StatusCodes.Status404NotFound,
                Message = $"End Point{context.Request.Path} Is Not Found"
            };

            await context.Response.WriteAsJsonAsync(Response);
        }

        private static async Task HandlingErrorAsync(HttpContext context, Exception ex)
        {
            //1- Set StatusCode
            //context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            //2-Set ContentType (application/Josn)
            context.Response.ContentType = "application/json";

            //3-Response Object (Body>>(statuscode-Message))

            var Response = new ErrorDetails()
            {
                //StatusCode= StatusCodes.Status500InternalServerError,
                Message = ex.Message
            };

            // handling notfounderror if i send id not found
            Response.StatusCode = ex switch
            {
                NotFoundExceptions => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            //4-Return Response

            await context.Response.WriteAsJsonAsync(Response);
        }
        
    }
}
