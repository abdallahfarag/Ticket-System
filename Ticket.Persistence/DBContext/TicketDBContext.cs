using Microsoft.EntityFrameworkCore;
using Ticket.Application.Interfaces.DBContext;

namespace Ticket.Persistence.DBContext
{
    public class TicketDBContext : DbContext, ITicketDBContext
    {
        public TicketDBContext(DbContextOptions<TicketDBContext> options)
        : base(options)
        {
        }
        public DbSet<Domain.Entities.Ticket> Tickets { get; set; }

        public async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }
    }
}
