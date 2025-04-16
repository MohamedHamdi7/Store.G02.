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
    //Static class >>class member method
    static class SpecificationEvaluator
    {
        // to Generate StaticQuery
        public static IQueryable <TEntity> GetQuery <TEntity,TKey> (IQueryable<TEntity> InputQuery,ISpecification<TEntity,TKey> Spec) 

            where TEntity : BaseEntity<TKey>
        {
            //Filtertion
            var query = InputQuery;
            if(Spec.Criteria is not null)
             query=  query.Where(Spec.Criteria);

            //Sorting
            if(Spec.OrderBy is not null)
                query=query.OrderBy(Spec.OrderBy);


            if(Spec.OrderByDescending is not null)
                query=query.OrderByDescending(Spec.OrderByDescending);
              
            //Pagination
            if(Spec.IsPagination)
                query=query.Skip(Spec.Skip).Take(Spec.Take);


            //Filtertion

            query =   Spec.IncludeExpressions.Aggregate(query ,(currentQuery, includExperstion) => currentQuery.Include(includExperstion));

            return query;
        }

    }
}
