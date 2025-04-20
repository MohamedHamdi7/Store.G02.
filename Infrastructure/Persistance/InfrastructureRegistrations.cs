using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Data.Contexts;
using Persistance.Repositories;
using Persistance.Seedingclass;

namespace Persistance
{
    public static class InfrastructureRegistrations
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                //options.UseSqlServer("DefaultConnection");
                //or
                //options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
                //or
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

           services.AddScoped<IDbIntializer, DbIntializer>();  //Allow DI to create object DbIntializer
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

    }
}
