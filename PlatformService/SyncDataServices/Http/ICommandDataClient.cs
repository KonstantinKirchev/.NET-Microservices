using PlatformService.Dtos;

namespace PlatformService.SyncDataservices.Http
{
    public interface ICommandDataClient
    {
        Task SendPlatformToCommand(PlatformReadDto plat);
    }
}