using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Http;
using Ticket.Application.Features.Tickets.Commands.UpdateTicketStatus;
using Ticket.Application.Interfaces.Background;

namespace Ticket.Application.Background
{
    public class BackgroundJobHandler : IBackgroundJobHandler
    {
        private IMediator _mediator;
        public BackgroundJobHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public string ScheduleUpdateTicketStatus(UpdateTicketStatusCommand command, DateTime executeAt)
        {
            string jobId = BackgroundJob.Schedule(() => UpdateTicketStatus(command), executeAt);
            return jobId;
        }

        public bool DeleteScheduledJobs(List<string> jobsIds)
        {
            bool result = true;

            jobsIds.ForEach(jobId =>
            {
                bool isDeleted = BackgroundJob.Delete(jobId);
                if (!isDeleted)
                    result = false;
            });

            return result;
        }

        public async Task UpdateTicketStatus(UpdateTicketStatusCommand command)
        {
            await _mediator.Send(command);
        }
    }
}
