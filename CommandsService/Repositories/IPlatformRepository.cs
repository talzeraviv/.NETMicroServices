using CommandsService.Models;

namespace CommandsService.Repositories
{
    public interface IPlatformRepository : IRepository
    {
        IEnumerable<Platform> GetAllPlatforms();
        void CreatePlatform(Platform platform);
        bool ExternalPlatformExists(int externalPlatformId);
    }
}