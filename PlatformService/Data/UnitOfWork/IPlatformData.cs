using PlatformService.Data.Repositories;
using PlatformService.Models;

namespace PlatformService.Data.UnitOfWork
{
    public interface IPlatformData
    {
        IRepository<Platform> Platforms { get; }

        bool SaveChanges();
    }
}