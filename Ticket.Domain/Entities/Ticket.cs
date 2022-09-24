using Ticket.Common;

namespace Ticket.Domain.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public TicketStatus Status { get; set; }
        public int GovernorateId { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<ScheduledJob> ScheduledJobs { get; set; }
    }
}
