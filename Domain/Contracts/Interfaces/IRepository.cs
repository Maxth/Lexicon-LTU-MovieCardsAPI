namespace Domain.Contracts.Interfaces
{
    public interface IRepository
    {
        Task SaveChangesAsync();
    }
}
