using PlatformService.Models;

namespace PlatformService.Data.Repositories
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly AppDbContext _context;

        public PlatformRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Create(Platform platform)
        {
            if (platform == null) throw new ArgumentNullException(nameof(platform));
            _context.Platforms.Add(platform);
        }

        public IEnumerable<Platform> GetAll() => _context.Platforms.ToList();
        public Platform? GetById(int id) => _context.Platforms.FirstOrDefault(p => p.Id == id);
        public bool SaveChanges() => _context.SaveChanges() >= 0;
    }
}