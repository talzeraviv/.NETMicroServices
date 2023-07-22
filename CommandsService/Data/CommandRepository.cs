using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandsService.Models;
using CommandsService.Repositories;

namespace CommandsService.Data
{
    public class CommandRepository : ICommandRepository
    {
        private readonly AppDbContext _context;

        public CommandRepository(AppDbContext context) => _context = context;

        #region Creational Methods
        public void CreateCommandForPlatform(int PlatformId, Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            command.PlatformId = PlatformId;
            _context.Commands.Add(command);
        }
        #endregion

        #region Get Methods
        public Command? GetCommandForPlatform(int PlatformId, int commandId)
        {
            return _context.Commands.Where(c => c.PlatformId == PlatformId && c.Id == commandId).FirstOrDefault();
        }
        public IEnumerable<Command>? GetCommandsForPlatform(int PlatformId)
        {
            return _context.Commands.Where(c => c.PlatformId == PlatformId).OrderBy(c => c.Platform.Name);
        }
        #endregion

        public bool Exists(int Id) => _context.Commands.Any(c => c.Id == Id);
        public bool SaveChanges() => _context.SaveChanges() >= 0;
    }
}