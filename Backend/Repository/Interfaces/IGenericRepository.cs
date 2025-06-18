using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
        Task<T?> GetSingleOrDefaultAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> expression);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize);
        Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<T, bool>> expression);
        
        // Add methods for Include operations
        Task<IEnumerable<T>> FindWithIncludeAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        Task<T?> GetSingleWithIncludeAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllWithIncludeAsync(params Expression<Func<T, object>>[] includes);
    }
}
