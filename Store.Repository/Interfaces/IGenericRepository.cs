using Store.Data.Entities;
using Store.Repository.Specifications;
using Store.Repository.Specifications.ProductSpecs;
namespace Store.Repository.Interfaces
{
    public interface IGenericRepository<TEntity , TKey> where TEntity :BaseEntity<TKey>
    {
        Task <TEntity>GetByIdAsync(TKey id);
        Task <IReadOnlyList<TEntity>> GetAllAsync();
        Task TaskAddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<TEntity> GetWithSpecificationByIdAsync(ISpecification<TEntity> spec); //Take specification
        Task<IReadOnlyList<TEntity>> GetAllWithSpecificationAsync(ISpecification<TEntity> spec);  //Take specification
        Task<int> GetCountAsync(ISpecification<TEntity> spec); // --> Count

    }

   
}
