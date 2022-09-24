using Ticket.Application.Features.Tickets.Commands.UpdateTicketStatus;

namespace Ticket.Application.Interfaces.Background
{
    public interface IBackgroundJobHandler
    {
        string ScheduleUpdateTicketStatus(UpdateTicketStatusCommand command, DateTime executeAt);
        bool DeleteScheduledJobs(List<string> jobsIds);
    }
}
