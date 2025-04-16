﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;

namespace Services.Specifications
{

    // Dessign Battern For Query
    public class BaseSpecifications<TEntity, Tkey> : ISpecification<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public Expression<Func<TEntity, bool>>? Criteria { get ; set ; }
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, object>>? OrderBy { get ; set ; }
        public Expression<Func<TEntity, object>>? OrderByDescending { get ; set ; }
        public int Skip { get ; set ; }
        public int Take { get ; set ; }
        public bool IsPagination { get; set ; }


        //ctor
        public BaseSpecifications(Expression<Func<TEntity, bool>>? expression)
        {
            Criteria=expression;
            
        }

        // Method
        protected void AddInclude(Expression<Func<TEntity, object>> expression)
        {
             IncludeExpressions.Add(expression);
        }
        protected void AddOrderBy(Expression<Func<TEntity, object>> expression)
        {
            OrderBy=expression;
        }

        protected void AddOrderByDescending(Expression<Func<TEntity, object>> expression)
        {
            OrderByDescending =expression;
        }

        protected void ApplyPagination(int Pageindex,int Pagesize)
        {
            IsPagination = true;
            Take = Pagesize;
            Skip= (Pageindex - 1)*Pagesize;
        }

    }
}
