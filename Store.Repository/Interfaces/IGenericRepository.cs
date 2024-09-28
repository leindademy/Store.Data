using Store.Data.Entities;
using Store.Repository.Specifications;
using Store.Repository.Specifications.ProductSpecs;
namespace Store.Repository.Interfaces
{
    public interface IGenericRepository<TEntity , TKey> where TEntity :BaseEntity<TKey>
    {
        Task <TEntity>GetByIdAsinc(TKey id);
        Task <IReadOnlyList<TEntity>> GetAllAsinc();
        Task <IReadOnlyList<TEntity>>GetAllAsNotTrackAsinc();
        Task TaskAddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<TEntity> GetWithSpecificationByIdAsinc(ISpecification<TEntity> spec); //Take specification
        Task<IReadOnlyList<TEntity>> GetAllWithSpecificationAsinc(ISpecification<TEntity> spec);  //Take specification
        Task<int> GetCountAsync(ISpecification<TEntity> spec); // --> Count

    }

   
}
