using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Persistance.Data.Contexts;

namespace Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext context;

        //private readonly Dictionary<string, object> repositories;

        private readonly ConcurrentDictionary<string, object> repositories;

        //clr
        public UnitOfWork(StoreDbContext _context)
        {

            context = _context;

            //repositories=new Dictionary<string, object>();

            repositories=new ConcurrentDictionary<string, object>();
        }

        //public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        //{
        //    var type=typeof(TEntity).Name;
        //    if (!repositories.ContainsKey(type))
        //    {
        //        var repository = new GenericRepository<TEntity, TKey>(context);
        //        repositories.Add(type, repository);
        //    }
        //    return (IGenericRepository<TEntity, TKey>) repositories[type];
        //}



       public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
       =>
         ((IGenericRepository<TEntity, TKey>) repositories.GetOrAdd(typeof(TEntity).Name,new GenericRepository<TEntity,TKey>(context)));
      

        public async Task<int> saveChngesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
