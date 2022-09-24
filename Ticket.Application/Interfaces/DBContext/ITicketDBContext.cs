using Microsoft.EntityFrameworkCore;
namespace Ticket.Application.Interfaces.DBContext
{
    public interface ITicketDBContext
    {
        DbSet<Domain.Entities.Ticket> Tickets { get; set; }
        Task<int> SaveChanges();
    }
}
