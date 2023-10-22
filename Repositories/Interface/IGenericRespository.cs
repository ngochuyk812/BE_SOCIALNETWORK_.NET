using BE_SOCIALNETWORK.Database.Contracts;
using BE_SOCIALNETWORK.Extensions;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace BE_SOCIALNETWORK.Repositories.IRespositories
{
    public interface IGenericRespository<T>  where T : BaseModel
    {
        T Add(T entity);
        Task<bool> AddRangeAsync(IEnumerable<T> entity);
        void Delete(T entity);
        void Delete(object id);
        void DeleteRange(Expression<Func<T, bool>> filter);
        void Update(T entity);
        Task<T> Find(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>> includeProperties = null);
        Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, Func<IQueryable<T>, IIncludableQueryable<T, object>> includeProperties);
        Task<T> AddAsync(T entity, bool ensureTransaction = false);
        Task<PaginatedItems<T>> PageAsync(int pageIndex, int pageSize, Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, Func<IQueryable<T>, IIncludableQueryable<T, object>> includeProperties);
        Task<PaginatedItems<TQuery>> PageWithQueryAsync<TQuery>(int pageIndex, int pageSize, IQueryable<TQuery> query , Expression<Func<TQuery, bool>> filter, Func<IQueryable<TQuery>, IOrderedQueryable<TQuery>> orderBy, Func<IQueryable<TQuery>, IIncludableQueryable<TQuery, object>> includeProperties) where TQuery : BaseModel;
        IQueryable<TQuery> QueryFromSql<TQuery>(string sql, params object[] parameters) where TQuery : class;
    }
}
