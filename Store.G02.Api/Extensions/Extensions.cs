using Microsoft.AspNetCore.Mvc;
using Services;
using Shared;
using Persistance;
using Domain.Contracts;
using Store.G02.Api.MiddleWare;

namespace Store.G02.Api.Extensions
{
    public static class Extensions
    {
       // Configuring Services
        public static IServiceCollection RegisterAllServices(this IServiceCollection services ,IConfiguration configuration)
        {

            services.AddBuiltInServices();

            services.AddSwaggerServices();

            // Database Services
            services.AddInfrastructureServices(configuration);
            
            services.AddApplicationServices();

            // To Handling ValidationError Use ModelStateServices
            services.ConfigureServices();



            return services;
        }
        private static IServiceCollection AddBuiltInServices(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        }
        private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
        private static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (actionContext) =>
                {

                    var error = actionContext.ModelState.Where(m => m.Value.Errors.Any())
                                             .Select(m => new ValidtionError()
                                             {
                                                 Field = m.Key,
                                                 Error = m.Value.Errors.Select(errors => errors.ErrorMessage)
                                             });

                    var Response = new ValidationErrorsResponse()
                    {
                        Errors = error
                    };
                    return new BadRequestObjectResult(Response);

                };

            });
            return services;
        }




        // Configuring Middleware
        public static async Task<WebApplication> ConfigurMiddlewaresAsync(this WebApplication app)
        {
           await app.IntializeDatabaseAsync();

            app.UseGlobalErrorHandlingMiddleware();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();  // to make server to return static file

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            return app;
        }

        private static async Task<WebApplication> IntializeDatabaseAsync(this WebApplication app)
        {
            #region Seeding
            // code seeding >>>to update db
            using var scope = app.Services.CreateScope();

            var dbIntializer = scope.ServiceProvider.GetRequiredService<IDbIntializer>();  //Ask Clr To Create object DbIntializer

            await dbIntializer.IntializeAsync();

            #endregion

            return app;
        }
        private static WebApplication UseGlobalErrorHandlingMiddleware(this WebApplication app)
        {

            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            return app;
        }
    }
}
