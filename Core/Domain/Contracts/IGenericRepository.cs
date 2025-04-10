﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity,TKay> where TEntity:BaseEntity<TKay>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool trackchanges=false);

        Task<TEntity> GetAsync(TKay id);

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);
        void Delete(TEntity entity);

    }
}
