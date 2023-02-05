using Microsoft.EntityFrameworkCore;

namespace CommandsService.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<TEntity> _entitySet;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _entitySet = dbContext.Set<TEntity>();
        }

        public DbSet<TEntity> EntitySet
        {
            get { return _entitySet; }
        }

        public IQueryable<TEntity> All()
        {
            return _entitySet;
        }

        public TEntity Find(object id)
        {
            return _entitySet.Find(id);
        }

        public TEntity Add(TEntity entity)
        {
            return this.ChangeState(entity, EntityState.Added);
        }

        public TEntity Update(TEntity entity)
        {
            return this.ChangeState(entity, EntityState.Modified);
        }

        public void Remove(TEntity entity)
        {
            this.ChangeState(entity, EntityState.Deleted);
        }

        public TEntity Remove(object id)
        {
            var entity = this.Find(id);
            this.Remove(entity);
            return entity;
        }

        public bool SaveChanges()
        {
          return (_dbContext.SaveChanges() >= 0);
        }

        private TEntity ChangeState(TEntity entity, EntityState state)
        {
            var entry = _dbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                _entitySet.Attach(entity);
            }

            entry.State = state;
            return entity;
        }
    }
}