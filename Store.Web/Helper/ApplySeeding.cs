using Microsoft.EntityFrameworkCore;
using Store.Data.Context;
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
                    await context.Database.MigrateAsync(); // If The DB is Not Created , It Will be Created
                    await StoreContextSeed.SeedAsync(context, loggerfactory);
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
