using Ticket.Common.Models.SignalRModels;

namespace Ticket.Application.Interfaces.SignalR
{
    public interface IHubClient
    {
        Task BroadcastMessage(TicketStatusModel ticketStatusModel);
    }
}
