using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Data.Contexts
{
    public class StoreDbContext :DbContext
    {
        //make clr to make object automatic by active DI
        public StoreDbContext(DbContextOptions  options):base(options) 
        {
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetEx);                  //from the same solution
           modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly); //from the project persistance                                                             

            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Product>().HasData(new Product() { });

        }



    }
}
