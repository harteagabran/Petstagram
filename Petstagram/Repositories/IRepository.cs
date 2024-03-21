namespace Petstagram.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByID(int id);
        Task<IEnumerable<T>> GetAll();
        Task<T> Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        Task Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
