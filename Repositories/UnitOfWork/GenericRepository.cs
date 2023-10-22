using Amazon.Auth.AccessControlPolicy;
using BE_SOCIALNETWORK.Database;
using BE_SOCIALNETWORK.Database.Contracts;
using BE_SOCIALNETWORK.Extensions;
using BE_SOCIALNETWORK.Repositories.IRespositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using parking_center.Extensions;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
namespace BE_SOCIALNETWORK.Repositories.Contracts
{
    public class GenericRepository<T> : IGenericRespository<T> where T : BaseModel  
    {

        public Social_NetworkContext _context = null;
        public DbSet<T> table = null;

        public GenericRepository(Social_NetworkContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }

        public T Add(T entity)
        {
            EntityEntry<T> entityEntry = table.Add(entity);
            return entityEntry.Entity;
        }

        public async Task<bool> AddRangeAsync(IEnumerable<T> entity)
        {
            try
            {
                await table.AddRangeAsync(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public virtual async Task<T> AddAsync(T entity, bool ensureTransaction = false)
        {
            EntityEntry<T> entityEntry = null;
            if (ensureTransaction)
            {
                using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
                {
                    _context.Entry(entity);
                    entityEntry = await table.AddAsync(entity, new CancellationToken());
                    try
                    {
                        await _context.SaveChangesAsync(new CancellationToken());
                        await transaction.CommitAsync();
                    }
                    catch
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
                return entityEntry.Entity;
            }
            else
            {
                entityEntry = await table.AddAsync(entity, new CancellationToken());
              
            }
            return entityEntry.Entity;
        }

        public void Delete(T entity)
        {
            table.Remove(entity);
        }

        public void Delete(object id)
        {
            T entityToDelete = table.Find(id);
            if (entityToDelete != null)
            {
                table.Remove(entityToDelete);
            }
        }
        public  void DeleteRange(Expression<Func<T, bool>> filter)
        {
            var source =  table.Where(filter);
            table.RemoveRange(source);
        }

        public virtual async Task<T> Find(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>> includeProperties = null)
        {
            IQueryable<T> source = table.AsNoTracking();
            if (filter != null)
            {
                source = source.Where(filter);
            }
            if (includeProperties != null)
            {
                source = includeProperties(source);
            }
            return await source.FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> source = table.AsNoTracking();
            if (filter != null)
            {
                source = source.Where(filter);
            }

            if (orderBy != null)
            {
                return await orderBy(source).ToListAsync();
            }
            return await source.ToListAsync();
        }

        public async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, Func<IQueryable<T>, IIncludableQueryable<T, object>> includeProperties)
        {
            IQueryable<T> source = table.AsNoTracking();
            if (filter != null)
            {
                source = source.Where(filter);
            }
            if (includeProperties != null)
            {
                source = includeProperties(source);
            }
            if (orderBy != null)
            {
                return await orderBy(source).ToListAsync();
            }
            return await source.ToListAsync();
        }

        public void Update(T entity)
        {
           table.Update(entity);
        }
        public async Task<PaginatedItems<T>> PageAsync(int pageIndex, int pageSize, Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, Func<IQueryable<T>, IIncludableQueryable<T, object>> includeProperties)
        {
            IQueryable<T> source = table.AsNoTracking();
            if (filter != null)
            {
                source = source.Where(filter);
            }
            if (includeProperties != null)
            {
                source = includeProperties(source);
            }
            int projectTotal = source.Count();
            List<T> listAsync;
            if (orderBy != null)
            {
                listAsync = await orderBy(source).Paginate(pageIndex, pageSize).ToListAsync();
            }
            else
            {
                listAsync = await source.Paginate(pageIndex, pageSize).ToListAsync();
            }
            return new PaginatedItems<T> (pageIndex, pageSize, projectTotal,listAsync);
        }

        /// <summary>
        /// Creates a LINQ query for the query type based on a raw SQL query
        /// </summary>
        /// <typeparam name="TQuery">Query type</typeparam>
        /// <param name="sql">The raw SQL query</param>
        /// <param name="parameters">The values to be assigned to parameters</param>
        /// <returns>An IQueryable representing the raw SQL query</returns>
        public virtual IQueryable<TQuery> QueryFromSql<TQuery>(string sql, params object[] parameters) where TQuery : class
        {
            return _context.Set<TQuery>().FromSqlRaw(CreateSqlWithParameters(sql, parameters), parameters);
        }

        /// <summary>
        /// Modify the input SQL query by adding passed parameters
        /// </summary>
        /// <param name="sql">The raw SQL query</param>
        /// <param name="parameters">The values to be assigned to parameters</param>
        /// <returns>Modified raw SQL query</returns>
        public virtual string CreateSqlWithParameters(string sql, params object[] parameters)
        {
            //add parameters to sql
            for (int i = 0; i <= (parameters?.Length ?? 0) - 1; i++)
            {
                //if (parameters[i] is not DbParameter parameter)
                //{
                //    continue;
                //}
                foreach (DbParameter parameter in parameters)
                {
                    sql = $"{sql}{(i > 0 ? "," : string.Empty)} @{parameter.ParameterName}";
                    //whether parameter is output
                    if (parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Output)
                    {
                        sql = $"{sql} output";
                    }
                }
            }
            return sql;
        }

        public async Task<PaginatedItems<TQuery>> PageWithQueryAsync<TQuery>(int pageIndex, int pageSize, IQueryable<TQuery> query, Expression<Func<TQuery, bool>> filter, Func<IQueryable<TQuery>, IOrderedQueryable<TQuery>> orderBy, Func<IQueryable<TQuery>, IIncludableQueryable<TQuery, object>> includeProperties) where TQuery : BaseModel
        {
            var tableQuery = _context.Set<TQuery>().AsNoTracking();
            IQueryable<TQuery> source = null;
            if (query != null)
            {
               source = query;
            }
            else
            {
               source = tableQuery;
            }
            if (filter != null)
            {
                source = source.Where(filter);
            }
            if (includeProperties != null)
            {
                source = includeProperties(source);
            }
            int projectTotal = source.Count();
            List<TQuery> listAsync;
            if (orderBy != null)
            {
                listAsync = await orderBy(source).Paginate(pageIndex, pageSize).ToListAsync();
            }
            else
            {
                listAsync = await source.Paginate(pageIndex, pageSize).ToListAsync();
            }

            return new PaginatedItems<TQuery>(pageIndex, pageSize, projectTotal, listAsync);
        }
    }
}
