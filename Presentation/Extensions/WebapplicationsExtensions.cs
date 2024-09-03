using Infrastructure.Data;

namespace Presentation.Extensions
{
    public static class WebbapplicationsExtensions
    {
        public static async Task SeedDataAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var context = serviceProvider.GetRequiredService<MovieCardsDbContext>();

            // await context.Database.EnsureDeletedAsync();
            // await context.Database.MigrateAsync();

            try
            {
                await SeedData.InitAsync(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
