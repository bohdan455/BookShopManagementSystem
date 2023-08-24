using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace DataAccess.Repositories.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        void Attach(T entity);
        void Create(T entity);
        Task CreateAsync(T entity);
        void Delete(T entity);
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, T>> selector, Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        Task<T> GetFirstAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, T>> selector, Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        Task<T> GetLastAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        Task<T?> GetLastOrDefaultAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        Task<Tuple<IEnumerable<T>, int>> GetRangeAsync(Expression<Func<T, bool>>? predicate = null, Expression<Func<T, T>>? selector = null, Func<IQueryable<T>, IQueryable<T>>? sorting = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, int? pageNumber = null, int? pageSize = null);
        Task<T> GetSingleAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        Task<T?> GetSingleOrDefaultAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        void Update(T entity);
    }
}