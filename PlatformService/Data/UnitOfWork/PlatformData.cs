using PlatformService.Data.Repositories;
using PlatformService.Models;

namespace PlatformService.Data.UnitOfWork
{
    public class PlatformData : IPlatformData
    {
        private readonly AppDbContext _dbContext;

        private readonly IDictionary<Type, object> _repositories;

        public PlatformData(AppDbContext context)
        {
            _dbContext = context;
            _repositories = new Dictionary<Type, object>();
        }

        public IRepository<Platform> Platforms { 
            get { return this.GetRepository<Platform>(); }
        }

        public bool SaveChanges()
        {
            return (_dbContext.SaveChanges() >= 0);
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!_repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(Repository<T>);
                _repositories.Add(
                    typeof(T),
                    Activator.CreateInstance(type, _dbContext));
            }

            return (IRepository<T>)_repositories[typeof(T)];
        }
    }
}