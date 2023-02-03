namespace PlatformService.Data.Repositories
{
    public interface IRepository<T> 
    {
        IQueryable<T> All();

        T Find(object id);

        T Add(T entity);

        T Update(T entity);

        T Remove(object id);

        void Remove(T entity);

        bool SaveChanges();
    }
}