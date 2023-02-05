using CommandsService.Data.Repositories;
using CommandsService.Models;

namespace CommandsService.Data.UnitOfWork
{
    public interface ICommandsData
    {
        IRepository<Platform> Platforms { get; }

        IRepository<Command> Commands { get; }

        bool SaveChanges();
    }
}