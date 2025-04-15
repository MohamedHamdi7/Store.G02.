using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contracts
{
    public interface ISpecification<TEntity,TKey> where TEntity :BaseEntity<TKey>
    {
        public Expression<Func<TEntity,bool>>? Criteria { get; set; } //prop

        public List<Expression<Func<TEntity,object>>> IncludeExpressions { get; set; }  //prop
        public Expression<Func<TEntity, object>>? OrderBy { get; set; } //prop
        public Expression<Func<TEntity, object>>? OrderByDescending { get; set; } //prop

    }
}
