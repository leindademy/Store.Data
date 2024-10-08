using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.Data.Entities.IdentityEntities;
namespace Store.Data.Context
{
    public class StoreIdentityDBContext : IdentityDbContext<AppUser>
    {
        public StoreIdentityDBContext(DbContextOptions<StoreIdentityDBContext> options) : base(options)
        {

        }
    }
}
