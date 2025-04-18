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

            }
            catch (Exception ex)
            {

                // log Exption
                logger.LogError(ex,ex.Message);

                //1- Set StatusCode
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                //2-Set ContentType (application/Josn)
                context.Response.ContentType= "application/json";

                //3-Response Object (Body>>(statuscode-Message))

                var Respose = new ErrorDetails()
                {
                    StatusCode= StatusCodes.Status500InternalServerError,
                    Message= ex.Message
                };
                 
                //4-Return Response

               await context.Response.WriteAsJsonAsync(Respose);

            }
        }

    }
}
