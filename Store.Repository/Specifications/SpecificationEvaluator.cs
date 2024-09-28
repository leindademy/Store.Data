using Microsoft.EntityFrameworkCore;
using Store.Data.Entities;

namespace Store.Repository.Specifications
{
    // Link Between Specification & Repository
    public class SpecificationEvaluator<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        // BaseQuery + Specifications --> IQuaryable

        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> BaseQuery, ISpecification<TEntity> spec)
        {

            var query = BaseQuery;
            if(spec.Criteria is not null)
                query = query.Where(spec.Criteria); // where( x => x.TypeId == 2)

            if(spec.OrderBy is not null )
                query = query.OrderBy(spec.OrderBy); // x => x.Name

            if(spec.OrderByDescending is not null )
                query = query.OrderByDescending(spec.OrderByDescending); 

            query = spec.Includes.Aggregate(query,(current,includeExpression) => current.Include(includeExpression));
            return query;

        }
        

    }
}
