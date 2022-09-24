using Microsoft.AspNetCore.SignalR;
using Ticket.Application.Interfaces.SignalR;

namespace Ticket.Application.SignalR
{
    public class BroadcastHub : Hub<IHubClient>
    {
    }
}
