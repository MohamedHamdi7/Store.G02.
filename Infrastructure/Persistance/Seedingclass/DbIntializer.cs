using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistance.Data.Contexts;

namespace Persistance.Seedingclass
{
    public class DbIntializer  : IDbIntializer
    {
        private readonly StoreDbContext context;

        //make clr to create object to achive DI
        public DbIntializer(StoreDbContext _context)
        {
            context = _context;
        }

        public StoreDbContext Context { get; }

        public async Task IntializeAsync()
        {
            // create Db if it doesn't exist

            if (context.Database.GetPendingMigrations().Any())
            {
                await context.Database.MigrateAsync();
            }

            //Data Seeding

            //1- Seeding producttype from JsonFile
            if (!context.ProductTypes.Any())
            {
                //1-1- Read All Data From JosnFile

                var typesdata = await File.ReadAllTextAsync(@"..\Infrastructure\Persistance\seeding\types.json");

                //2-1-Convert string To C# Objects [List<ProductType>]

                var types = JsonSerializer.Deserialize<List<ProductType>>(typesdata);

                //3-1-Add List<ProductType> To Database

                if (types is not null && types.Any())
                {
                    await context.ProductTypes.AddRangeAsync(types);
                    await context.SaveChangesAsync();

                }

            }

            //2- Seeding productBrand from JsonFile

            if (!context.ProductBrands.Any())
            {
                //1-1- Read All Data From JosnFile
                var brandsdata = await File.ReadAllTextAsync(@"..\\Infrastructure\\Persistance\\seeding\\brands.json");

                //2-1-Convert string To C# Objects [List<ProductBrand>]

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsdata);

                //3-1-Add List<ProductBrand> To Database

                if (brands is not null && brands.Any())
                {
                    await context.ProductBrands.AddRangeAsync(brands);
                    await context.SaveChangesAsync();
                }



            }

            //3- Seeding product from JsonFile

            if (!context.Products.Any())
            {
                //1-1- Read All Data From JosnFile

                var productdata = await File.ReadAllTextAsync(@"..\Infrastructure\Persistance\seeding\products.json");

                //2-1-Convert string To C# Objects [List<Product>]

                var product = JsonSerializer.Deserialize<List<Product>>(productdata);

                //3-1-Add List<Product> To Database

                if (product is not null && product.Any())
                {
                    await context.Products.AddRangeAsync(product);
                    await context.SaveChangesAsync();
                }
            }








        }
    }
}
