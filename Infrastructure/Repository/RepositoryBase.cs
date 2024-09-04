using System.Linq.Expressions;
using Domain.Contracts.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T>
        where T : class
    {
        // protected DbContext Context { get; }
        protected DbSet<T> DbSet { get; }

        public RepositoryBase(MovieCardsDbContext db)
        {
            // Context = db;
            DbSet = db.Set<T>();
        }

        public async Task CreateAsync(T entity) => await DbSet.AddAsync(entity);

        public void Delete(T entity) => DbSet.Remove(entity);

        public IQueryable<T> GetAll(bool trackChanges = false) =>
            trackChanges ? DbSet : DbSet.AsNoTracking();

        public IQueryable<T> GetByCondition(
            Expression<Func<T, bool>> expression,
            bool trackChanges = false
        ) => trackChanges ? DbSet.Where(expression) : DbSet.Where(expression).AsNoTracking();

        public void Update(T entity) => DbSet.Update(entity);
    }
}
