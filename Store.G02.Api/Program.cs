
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistance.Data.Contexts;
using Persistance.Repositories;
using Persistance.Seedingclass;
using Services;
using Services.Abstraction;
using Services.manger;
using Store.G02.Api.MiddleWare;

namespace Store.G02.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                //options.UseSqlServer("DefaultConnection");
                //or
                //options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
                //or
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IDbIntializer, DbIntializer>();  //Allow DI to create object DbIntializer
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork >();
            builder.Services.AddAutoMapper(typeof(MapperReference).Assembly);
            builder.Services.AddScoped<IServiceManger, ServiceManger>();
            
            var app = builder.Build();


            // code seeding >>>to update db
            #region Seeding
            using var scope = app.Services.CreateScope();

            var dbIntializer = scope.ServiceProvider.GetRequiredService<IDbIntializer>();  //Ask Clr To Create object DbIntializer

            await dbIntializer.IntializeAsync();

            #endregion


            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

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

            app.Run();
        }
    }
}
