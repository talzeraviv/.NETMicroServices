using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool IsDevelopment)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>()!, IsDevelopment);
        }

        private static void SeedData(AppDbContext context, bool IsDevelopment)
        {
            if (!IsDevelopment)
            {
                try
                {
                    Console.WriteLine("=> Attempting to apply migrations...");
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"=> Couldn't apply migrations. {ex}");
                }
            }

            if (!context.Platforms.Any())
            {
                Console.WriteLine($"*** => Seeding Data... <= ***");
                context.Platforms.AddRange(
                    new Platform() { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Pammy", Publisher = "TalSoft", Cost = "Free" },
                    new Platform() { Name = "PacMan-Esque", Publisher = "TalSoft", Cost = "Free" }
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Data already present in in-memory database.");
            }
        }
    }
}