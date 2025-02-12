namespace Core.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T?> FindById(int id);
        Task<List<T>> FindAll();
        Task Remove(T model);
        Task Save();
        Task Save(T model);
        Task Save(List<T> model);
    }
}
