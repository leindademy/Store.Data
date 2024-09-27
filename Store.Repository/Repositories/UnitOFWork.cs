using Store.Data.Context;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using System.Collections;
namespace Store.Repository.Repositories
{

    public class UnitOFWork : IUnitOFWork
    
    {
        private readonly StoreDbContext _context;
        private Hashtable _Repositories; 

        public UnitOFWork (StoreDbContext context)
        {
            _context = context;
        }
        public async Task<int> CompleteAsync()
            => await _context.SaveChangesAsync();

        public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            if(_Repositories == null) 
                _Repositories = new Hashtable();

            var entitykey = typeof(TEntity).Name; // product as string

            if(!_Repositories.ContainsKey(entitykey))
            {
                //HashTable
                var repositoryType = typeof(GenericRepository<,>); //GenericRepository <product,int>

                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity) ,typeof(TKey)),_context);

                _Repositories.Add(entitykey,repositoryInstance);
            }

            return (IGenericRepository<TEntity, TKey>) _Repositories[entitykey]; 

        }
    }
}
