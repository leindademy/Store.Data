using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.Data.Context;
using Store.Data.Entities.IdentityEntities;
using Store.Repository;

namespace Store.Web.Helper
{
    public class ApplySeeding
    {
        public static async Task ApplySeedingAsync(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerfactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = services.GetRequiredService<StoreDbContext>();
                    var context2 = services.GetRequiredService<StoreIdentityDBContext>();

                    var UserManager = services.GetRequiredService<UserManager<AppUser>>();
                    
                    await context.Database.MigrateAsync(); // If The DB is Not Created , It Will be Created
                    await context2.Database.MigrateAsync();

                    await StoreContextSeed.SeedAsync(context, loggerfactory);
                    await StoreIdentityContextSeed.SeedUsersAsync(UserManager); 
                }
                catch (Exception Ex)
                {
                    var logger = loggerfactory.CreateLogger<ApplySeeding>();
                    logger.LogError(Ex.Message);
                }
            }
        }

    }
}
