using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using Ticket.Application.Helper;
using Ticket.Application.Interfaces.Background;
using Ticket.Application.Interfaces.Repositories;
using Ticket.Application.Interfaces.SignalR;
using Ticket.Application.SignalR;
using Ticket.Application.Wrappers;
using Ticket.Common;
using Ticket.Common.Models.SignalRModels;
using Ticket.Domain.Entities;

namespace Ticket.Application.Features.Tickets.Commands.UpdateTicketStatus
{
    public class UpdateTicketStatusCommand : IRequest<APIResponse<UpdateTicketStatusCommand>>
    {
        public int Id { get; set; }
        public TicketStatus Status { get; set; }
    }
    public class UpdateTicketStatusHandler : IRequestHandler<UpdateTicketStatusCommand, APIResponse<UpdateTicketStatusCommand>>
    {
        private readonly IRepository<Domain.Entities.Ticket> _repository;
        private readonly IRepository<ScheduledJob> _jobsRepository;
        private readonly IMapper _mapper;
        private readonly IBackgroundJobHandler _backgroundJobHandler;
        private readonly IHubContext<BroadcastHub, IHubClient> _hubContext;

        public UpdateTicketStatusHandler(IRepository<Domain.Entities.Ticket> repository
            , IRepository<ScheduledJob> jobsRepository
            , IMapper mapper
            , IBackgroundJobHandler backgroundJobHandler
            , IHubContext<BroadcastHub, IHubClient> hubContext)
        {
            _repository = repository;
            _jobsRepository = jobsRepository;
            _mapper = mapper;
            _backgroundJobHandler = backgroundJobHandler;
            _hubContext = hubContext;   
        }

        public async Task<APIResponse<UpdateTicketStatusCommand>> Handle(UpdateTicketStatusCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _repository.GetByIdAsync(request.Id);
            var timeDifferenceInMinutes = DatetimeHelper.CalculateDifferenceInMinutes(ticket.CreatedAt, DateTime.Now);

            if (timeDifferenceInMinutes > 60 && request.Status == TicketStatus.Handled)
                return new APIResponse<UpdateTicketStatusCommand>("Cannot be handled after 60 mins.");

            _mapper.Map(request, ticket);
            var result = await _repository.SaveChangesAsync();
            if (!result)
                return new APIResponse<UpdateTicketStatusCommand>("Error ocurred in updating status to DB.");

            if (result && request.Status == TicketStatus.Handled)
            {
                var ticketJobs = await _jobsRepository.GetAllAsync(x => x.TicketId == ticket.Id);
                var ticketJobsIds = ticketJobs.Select(x => x.Id).ToList();
                _backgroundJobHandler.DeleteScheduledJobs(ticketJobsIds);
            }
            else if(result && request.Status != TicketStatus.Handled)
            {
                await _hubContext.Clients.All.BroadcastMessage(new TicketStatusModel(ticket.Id, ticket.Status));
            }
            return new APIResponse<UpdateTicketStatusCommand>(request);
        }
    }

}
