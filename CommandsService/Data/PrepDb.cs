using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandsService.Models;
using CommandsService.Repositories;
using CommandsService.SyncDataServices.Grpc;

namespace CommandsService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();
            var platforms = grpcClient!.ReturnAllPlatforms();

            SeedData(serviceScope.ServiceProvider.GetService<IPlatformRepository>()!, platforms);
        }
        private static void SeedData(IPlatformRepository repository, IEnumerable<Platform> platforms)
        {
            System.Console.WriteLine("=> Seeding new platforms <=");

            foreach (var platform in platforms)
            {
                if (!repository.Exists(platform.ExternalId))
                {
                    repository.CreatePlatform(platform);
                }
                repository.SaveChanges();
            }
        }
    }

}