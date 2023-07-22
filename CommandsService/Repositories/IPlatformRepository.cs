using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandsService.Models;

namespace CommandsService.Repositories
{
    public interface IPlatformRepository : IRepository
    {
        IEnumerable<Platform> GetAllPlatforms();
        void CreatePlatform(Platform platform);
    }
}