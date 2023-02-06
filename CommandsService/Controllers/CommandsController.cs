using CommandsService.Dtos;
using CommandsService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("/api/c/platforms/{platformId}/[controller]")]
    [ApiController]
    public class CommandsController : BaseController
    {
        public CommandsController(ICommandService commandService) 
            : base(commandService)
        {
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform (int platformId) 
        {
            if (!_commandService.PlatformExist(platformId))
                return NotFound();

            return Ok(_commandService.GetCommandsForPlatform(platformId));
        }

        [HttpGet("{commandId:int}", Name="GetCommandForPlatform")]
        public ActionResult<CommandReadDto> GetCommandForPlatform (int platformId, int commandId) 
        {
            if (!_commandService.PlatformExist(platformId))
                return NotFound();
                
            var command = _commandService.GetCommand(platformId, commandId);

            if (command == null) 
                return NotFound();
            
            return Ok(command);
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForPlatform (int platformId, CommandCreateDto commandDto) {
            if (!_commandService.PlatformExist(platformId))
                return NotFound();
            
            var command = _commandService.CreateCommand(platformId, commandDto);
            
            return CreatedAtRoute(nameof(GetCommandForPlatform), new {platformId = platformId, commandId = command.Id} , command);
        }
    }
}