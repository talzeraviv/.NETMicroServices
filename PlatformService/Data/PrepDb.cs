using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>()!);
        }

        private static void SeedData(AppDbContext context)
        {
            if (!context.Platforms.Any())
            {
                System.Console.WriteLine($"*** => Seeding Data... <= ***");
                context.Platforms.AddRange(
                    new Platform() { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Pammy", Publisher = "TalSoft", Cost = "Free" },
                    new Platform() { Name = "PacMan-Esque", Publisher = "TalSoft", Cost = "Free" }
                );

                context.SaveChanges();
            }
            else
            {
                System.Console.WriteLine("Data already present in in-memory database.");
            }
        }
    }
}