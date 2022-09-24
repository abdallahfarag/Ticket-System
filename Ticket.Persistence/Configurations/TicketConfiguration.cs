using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ticket.Persistence.Configurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Domain.Entities.Ticket>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Ticket> builder)
        {
            builder
            .HasMany(c => c.ScheduledJobs)
            .WithOne(e => e.Ticket);
        }
    }
}
