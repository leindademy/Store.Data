using Store.Data.Entities;
namespace Store.Repository.Interfaces
{
    public interface IUnitOFWork
    {
        IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
        Task<int> CompleteAsync();
    }
}
