using CommandsService.Dtos;
using CommandsService.Models;

namespace CommandsService.Services
{
    public interface ICommandService
    {
        // Platforms
        IEnumerable<PlatformReadDto> GetAllPlatforms();
        void CreatePlatform(Platform platform);
        bool PlatformExist(int platformId);

        // Commands
        IEnumerable<CommandReadDto> GetCommandsForPlatform(int platformId);
        CommandReadDto GetCommand(int platformId, int commandId);
        CommandReadDto CreateCommand(int platformId, CommandCreateDto command);
    }
}