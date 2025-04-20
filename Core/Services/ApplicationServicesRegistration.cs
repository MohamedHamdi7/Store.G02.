using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction;
using Services.manger;

namespace Services
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapperReference).Assembly);
            services.AddScoped<IServiceManger, ServiceManger>();

            return services;
        }

    }
}
