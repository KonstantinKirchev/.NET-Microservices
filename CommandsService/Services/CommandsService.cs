using AutoMapper;
using CommandsService.Data.UnitOfWork;
using CommandsService.Models;

namespace CommandsService.Services
{
    public class CommandsService : BaseService, ICommandsService
    {
        public CommandsService(ICommandsData data, IMapper mapper) 
            : base(data, mapper)
        {
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _data.Platforms.All().ToList();
        }

        public void CreatePlatform(Platform platform)
        {
            if (platform == null) 
            {
                throw new ArgumentNullException(nameof(platform));
            }

            _data.Platforms.Add(platform);
            _data.SaveChanges();
        }

        public bool PlatformExist(int platformId)
        {
            return _data.Platforms
                        .All()
                        .Any(p => p.Id == platformId);
        }

        public IEnumerable<Command> GetCommandsForPlatform(int platformId)
        {
            return _data.Commands
                .All()
                .Where(c => c.PlatformId == platformId)
                .OrderBy(c => c.Platform.Name)
                .ToList();
        }

        public Command GetCommand(int platformId, int commandId)
        {
            return _data.Commands
                        .All()
                        .FirstOrDefault(c => c.PlatformId == platformId && c.Id == commandId);
        }

        public void CreateCommand(int platformId, Command command)
        {
            if (command == null) {
                throw new ArgumentNullException(nameof(command));
            }

            command.PlatformId = platformId;

            _data.Commands.Add(command);
            _data.SaveChanges();
        }
    }
}