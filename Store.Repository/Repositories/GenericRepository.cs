using Microsoft.EntityFrameworkCore;
using Store.Data.Context;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Repository.Specifications;
using Store.Repository.Specifications.ProductSpecs;
namespace Store.Repository.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _context;
        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }
        public void Delete(TEntity entity)
            => _context.Set<TEntity>().Remove(entity);

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
            => await _context.Set<TEntity>().ToListAsync();

        public async  Task<IReadOnlyList<TEntity>> GetAllAsNotTrackAsinc()
            => await _context.Set<TEntity>().AsNoTracking().ToListAsync();

        public async Task<TEntity> GetByIdAsync(TKey? id)
            => await _context.Set<TEntity>().FindAsync(id);

        public async Task TaskAddAsync(TEntity entity)
            =>  await _context.Set<TEntity>().AddAsync(entity);

        public void Update(TEntity entity)
            => _context.Set<TEntity>().Update(entity);
        public async Task<IReadOnlyList<TEntity>> GetAllWithSpecificationAsync(ISpecification<TEntity> spec)
            => await SpecificationEvaluator<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), spec).ToListAsync();

        public async Task<TEntity> GetWithSpecificationByIdAsync(ISpecification<TEntity> spec)
            => await ApplySpecification(spec).FirstOrDefaultAsync()  ;
        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
            => SpecificationEvaluator<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), spec);

        public Task<int> GetCountAsync(ISpecification<TEntity> spec)
            => ApplySpecification(spec).CountAsync();

 
    }
}
