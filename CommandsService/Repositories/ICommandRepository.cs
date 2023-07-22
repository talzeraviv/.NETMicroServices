using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandsService.Models;
using CommandsService.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace CommandsService.Repositories
{
    public interface ICommandRepository : IRepository
    {
        IEnumerable<Command>? GetCommandsForPlatform(int PlatformId);
        Command? GetCommandForPlatform(int PlatformId, int commandId);
        void CreateCommandForPlatform(int PlatformId, Command command);
    }
}