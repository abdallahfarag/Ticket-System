using Ticket.Common;

namespace Ticket.Application.DTOs.Responses
{
    public class DTOGetTicketsResponse
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public TicketStatus Status { get; set; }
        public int GovernorateId { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
