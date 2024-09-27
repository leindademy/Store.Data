using Microsoft.EntityFrameworkCore;
using Store.Data.Context;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using System.Collections.Generic;
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

        public async Task<IReadOnlyList<TEntity>> GetAllAsinc()
            => await _context.Set<TEntity>().ToListAsync();

        public async Task <IReadOnlyList<TEntity>> GetAllAsNoTrackingAsinc()
            => await _context.Set<TEntity>().AsNoTracking().ToListAsync();

     
        public async Task<IReadOnlyList<TEntity>> GetAllAsNotTrackAsinc()
            => await _context.Set<TEntity>().AsNoTracking().ToListAsync();

        public async Task<TEntity> GetByIdAsinc(TKey? id)
            => await _context.Set<TEntity>().FindAsync(id);

        public async Task TaskAddAsync(TEntity entity)
            => await _context.Set<TEntity>().AddAsync(entity);

        public void Update(TEntity entity)
            => _context.Set<TEntity>().Update(entity);
    }
}
