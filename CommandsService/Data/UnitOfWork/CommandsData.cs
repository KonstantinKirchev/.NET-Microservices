using CommandsService.Data.Repositories;
using CommandsService.Models;

namespace CommandsService.Data.UnitOfWork
{
    public class CommandsData : ICommandsData
    {
        private readonly AppDbContext _dbContext;

        private readonly IDictionary<Type, object> _repositories;

        public CommandsData(AppDbContext context)
        {
            _dbContext = context;
            _repositories = new Dictionary<Type, object>();
        }

        public IRepository<Platform> Platforms { 
            get { return this.GetRepository<Platform>(); }
        }

        public IRepository<Command> Commands { 
            get { return this.GetRepository<Command>(); }
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