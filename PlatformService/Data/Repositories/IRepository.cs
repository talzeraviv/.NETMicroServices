namespace PlatformService.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        bool SaveChanges();

        IEnumerable<T> GetAll();
        T? GetById(int id);
        void Create(T entity);
    }
}