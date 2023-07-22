using System.ComponentModel.Design;
using AutoMapper;
using CommandsService.Dtos;
using CommandsService.Models;
using CommandsService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [ApiController]
    [Route("api/c/platforms/{platformId}/[controller]")]
    public class CommandsController : ControllerBase
    {
        private readonly IPlatformRepository _platformRepository;
        private readonly ICommandRepository _commandRepository;
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepository commandRepository, IPlatformRepository platformRepository, IMapper mapper)
        {
            _platformRepository = platformRepository;
            _commandRepository = commandRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
        {
            Console.WriteLine($"=> Hit GetCommandsForPlatform: {platformId}");

            if (!_platformRepository.Exists(platformId))
                return NotFound();

            var commands = _commandRepository.GetCommandsForPlatform(platformId);

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, int commandId)
        {
            Console.WriteLine($"Hit GetCommandForPlatform: {platformId}, {commandId}");

            if (!_platformRepository.Exists(platformId))
                return NotFound();

            var command = _commandRepository.GetCommandForPlatform(platformId, commandId);

            if (command == null)
                return NotFound();

            return Ok(_mapper.Map<CommandReadDto>(command));
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, CommandCreateDto commandDto)
        {
            Console.WriteLine($"Hit CreateCommandForPlatform: {platformId}");

            if (!_platformRepository.Exists(platformId))
                return NotFound();

            var command = _mapper.Map<Command>(commandDto);

            _commandRepository.CreateCommandForPlatform(platformId, command);
            _commandRepository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(command);
            return CreatedAtRoute(nameof(GetCommandForPlatform), new { platformId, CommandID = commandReadDto.Id }, commandReadDto);
        }
    }
}