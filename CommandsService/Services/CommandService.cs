using AutoMapper;
using CommandsService.Data.UnitOfWork;
using CommandsService.Dtos;
using CommandsService.Models;

namespace CommandsService.Services
{
    public class CommandService : BaseService, ICommandService
    {
        public CommandService(ICommandsData data, IMapper mapper) 
            : base(data, mapper)
        {
        }

        public IEnumerable<PlatformReadDto> GetAllPlatforms()
        {
            var platforms = _data.Platforms.All().ToList();
            return _mapper.Map<IEnumerable<PlatformReadDto>>(platforms);
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

        public IEnumerable<CommandReadDto> GetCommandsForPlatform(int platformId)
        {
            var commands = _data.Commands
                .All()
                .Where(c => c.PlatformId == platformId)
                .OrderBy(c => c.Platform.Name)
                .ToList();

            return _mapper.Map<IEnumerable<CommandReadDto>>(commands);
        }

        public CommandReadDto GetCommand(int platformId, int commandId)
        {
            var command =  _data.Commands
                        .All()
                        .FirstOrDefault(c => c.PlatformId == platformId && c.Id == commandId);

            return _mapper.Map<CommandReadDto>(command);
        }

        public CommandReadDto CreateCommand(int platformId, CommandCreateDto commandDto)
        {
            if (commandDto == null) {
                throw new ArgumentNullException(nameof(commandDto));
            }

            var command = _mapper.Map<Command>(commandDto);

            command.PlatformId = platformId;

            _data.Commands.Add(command);
            _data.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(command);

            return commandReadDto;
        }

        public bool ExternalPlatformExist(int externalPlatformId)
        {
            return _data.Platforms
                        .All()
                        .Any(p => p.ExternalId == externalPlatformId);
        }
    }
}