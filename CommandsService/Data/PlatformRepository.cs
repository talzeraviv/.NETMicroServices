using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandsService.Models;
using CommandsService.Repositories;

namespace CommandsService.Data
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly AppDbContext _context;

        public PlatformRepository(AppDbContext context) => _context = context;

        public void CreatePlatform(Platform platform)
        {
            if (platform == null)
                throw new ArgumentNullException(nameof(platform));

            _context.Platforms.Add(platform);
        }
        public bool Exists(int Id) => _context.Platforms.Any(p => p.Id == Id);
        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.Platforms.ToList();
        }

        public bool SaveChanges() => _context.SaveChanges() >= 0;
    }
}