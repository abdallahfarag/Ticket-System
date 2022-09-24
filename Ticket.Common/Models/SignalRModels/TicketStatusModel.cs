namespace Ticket.Common.Models.SignalRModels
{
    public class TicketStatusModel
    {
        public TicketStatusModel(int ticketId, TicketStatus status)
        {
            TicketId = ticketId;
            Status = status;
        }
        public int TicketId { get; set; }
        public TicketStatus Status { get; set; }
    }
}
