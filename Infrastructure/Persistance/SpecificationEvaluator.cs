using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistance
{
    static class SpecificationEvaluator
    {
        // to Generate StaticQuery
        public static IQueryable<TEntity> GetQuery<TEntity,TKey>(IQueryable<TEntity> InputQuery,ISpecification<TEntity,TKey> Spec) 

            where TEntity : BaseEntity<TKey>
        {
            var query = InputQuery;
            if(Spec.Criteria is not null)
             query=  query.Where(Spec.Criteria);

            query=   Spec.IncludeExpressions.Aggregate(query ,(currentQuery, includExperstion) => currentQuery.Include(includExperstion));

            return query;
        }

    }
}
