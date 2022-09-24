using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.Wrappers;

namespace Ticket.Application.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null);
        Task<PagedResult<T>> GetAllAsync(int pageNumber, int pageSize, Expression<Func<T, bool>>? predicate = null);
        Task<T> GetByIdAsync(int id);
        void Add(T entity);
        Task<bool> SaveChangesAsync();
        Task<T> FindByConditionAsync(Expression<Func<T, bool>> predicate);
        void AddRange(List<T> entities);
    }
}
