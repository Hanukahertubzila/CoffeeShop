namespace CoffeeShop.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<bool> Create(T entity);

        Task<List<T>> GetAll();

        Task<bool> Delete(T entity);

        Task<T> Update(T entity);
    }
}
