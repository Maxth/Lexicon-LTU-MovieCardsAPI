namespace Domain.Contracts.Interfaces
{
    public interface IRepositoryCommon<T>
    {
        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
