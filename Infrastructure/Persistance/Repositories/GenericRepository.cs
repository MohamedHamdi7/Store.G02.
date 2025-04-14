using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Persistance.Data.Contexts;

namespace Persistance.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext context;

        public GenericRepository(StoreDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackchanges=false )
        {
            //if (trackchange) return await context.Set<TEntity>().ToListAsync();
            //return await context.Set<TEntity>().AsNoTracking().ToListAsync();

            //or

            if(typeof(TEntity)==typeof(Product))
            {
                return trackchanges ?

                 await context.Products.Include(P=>P.productBrand).Include(P=>P.productType).ToListAsync() as IEnumerable<TEntity>
               : await context.Products.Include(P => P.productBrand).Include(P => P.productType).AsNoTracking().ToListAsync() as IEnumerable<TEntity>;
            }
            return trackchanges?
                  await context.Set<TEntity>().ToListAsync()
                : await context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetAsync(TKey id)
        {
            if(typeof(TEntity)==typeof(Product))
            {
                //return await context.Products.Include(p=>p.productBrand).Include(P=>P.productType).FirstOrDefaultAsync(P=>P.Id==id as int?) as TEntity;
                return await context.Products.Where(p=>p.Id==id as int?).Include(p => p.productBrand).Include(P => P.productType).FirstOrDefaultAsync() as TEntity;

            }
            return await context.Set<TEntity>().FindAsync(id);
        }
        public async Task AddAsync(TEntity entity)
        {
              await context.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
           context.Update(entity);
            
        }

        public void Delete(TEntity entity)
        {
            context.Remove(entity);
           
        }

       

       

       
    }
}
