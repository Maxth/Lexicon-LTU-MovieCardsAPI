using System.Linq.Expressions;

namespace Domain.Contracts.Interfaces
{
    public interface IRepositoryBase<T> : IRepositoryCommon<T>
    {
        IQueryable<T> GetAll(bool trackChanges = false);
        IQueryable<T> GetByCondition(
            Expression<Func<T, bool>> expression,
            bool trackChanges = false
        );
    }
}
