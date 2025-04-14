using System;
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

    }
}
