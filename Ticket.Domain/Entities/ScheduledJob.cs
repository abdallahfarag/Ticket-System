namespace Ticket.Domain.Entities
{
    public class ScheduledJob
    {
        public string Id { get; set; }
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
