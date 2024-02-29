using APLMatchMaker.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace Lexicon_LMS.Server.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task SeedDataAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var db = serviceProvider.GetRequiredService<ApplicationDbContext>();

                //db.Database.EnsureDeleted();
                // Tillfälligt låta bli att tömma databasen varje gång.
                // För att kunna behålla test data mellan sessioner.
                db.Database.Migrate();

                try
                {
                    await SeedData.InitAsync(db, serviceProvider);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}
