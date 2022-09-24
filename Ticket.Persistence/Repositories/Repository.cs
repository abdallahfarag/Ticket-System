using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Ticket.Application.Interfaces.Repositories;
using Ticket.Application.Wrappers;
using Ticket.Persistence.DBContext;

namespace Ticket.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly TicketDBContext _context;
        private readonly DbSet<T> _entities;

        public Repository(TicketDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _entities = context.Set<T>();
        }

        public void Add(T entity)
        {
            _entities.Add(entity);
        }
        public void AddRange(List<T> entities)
        {
            _entities.AddRange(entities);
        }
        public async Task<T> FindByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null)
        {
            if (predicate is not null)
                return await _entities.Where(predicate).ToListAsync();
            return await _entities.ToListAsync();
        }

        public async Task<PagedResult<T>> GetAllAsync(int pageNumber, int pageSize, Expression<Func<T, bool>>? predicate = null)
        {
            IQueryable<T> query;

            if (predicate is not null)
                query = _entities
                .Where(predicate);

            query = _entities;

            return PagedResult<T>.ToPagedList(query, pageNumber, pageSize);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }
    }
}
