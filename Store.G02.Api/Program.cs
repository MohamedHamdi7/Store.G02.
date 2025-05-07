
using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance;
using Persistance.Data.Contexts;
using Persistance.Repositories;
using Persistance.Seedingclass;
using Services;
using Services.Abstraction;
using Services.manger;
using Shared;
using Store.G02.Api.Extensions;
using Store.G02.Api.MiddleWare;

namespace Store.G02.Api
{
    public class Program
    {
        //
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.RegisterAllServices(builder.Configuration);

           var app = builder.Build();
            
           await app.ConfigurMiddlewaresAsync();

            app.Run();
        }
    }
}
