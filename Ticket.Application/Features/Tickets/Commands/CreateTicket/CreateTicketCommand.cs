using AutoMapper;
using MediatR;
using Ticket.Application.Features.Tickets.Commands.UpdateTicketStatus;
using Ticket.Application.Interfaces.Background;
using Ticket.Application.Interfaces.Repositories;
using Ticket.Application.Wrappers;
using Ticket.Common;
using Ticket.Domain.Entities;

namespace Ticket.Application.Features.Tickets.Commands.CreateTicket
{
    public class CreateTicketCommand : IRequest<APIResponse<CreateTicketCommand>>
    {
        public string Phone { get; set; }
        public TicketStatus Status { get; set; }
        public int GovernorateId { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
    }

    public class CreateTicketHandler : IRequestHandler<CreateTicketCommand, APIResponse<CreateTicketCommand>>
    {
        private readonly IRepository<Domain.Entities.Ticket> _repository;
        private readonly IRepository<ScheduledJob> _ticketJobRepository;
        private readonly IMapper _mapper;
        private readonly IBackgroundJobHandler _backgroundJobHandler;
        public CreateTicketHandler(IRepository<Domain.Entities.Ticket> repository
            , IMapper mapper
            , IBackgroundJobHandler backgroundJobHandler
            ,IRepository<ScheduledJob> ticketJobRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _backgroundJobHandler = backgroundJobHandler;
            _ticketJobRepository = ticketJobRepository;
        }

        public async Task<APIResponse<CreateTicketCommand>> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = _mapper.Map<CreateTicketCommand, Domain.Entities.Ticket>(request);
            _repository.Add(ticket);
            var result = await _repository.SaveChangesAsync();
            if (!result)
                return new APIResponse<CreateTicketCommand>("Error ocurred in creating ticket in DB.");

            //Schedule update statuses jobs
            List<ScheduledJob> ticketScheduledJobs = new List<ScheduledJob>();

            string yellowJobId = _backgroundJobHandler.ScheduleUpdateTicketStatus(new UpdateTicketStatusCommand { Id = ticket.Id, Status = TicketStatus.Yellow }, ticket.CreatedAt.AddMinutes(15));
            ticketScheduledJobs.Add(new ScheduledJob() { TicketId = ticket.Id, Id = yellowJobId });
            string greenJobId = _backgroundJobHandler.ScheduleUpdateTicketStatus(new UpdateTicketStatusCommand { Id = ticket.Id, Status = TicketStatus.Green }, ticket.CreatedAt.AddMinutes(30));
            ticketScheduledJobs.Add(new ScheduledJob() { TicketId = ticket.Id, Id = greenJobId });
            string blueJobId = _backgroundJobHandler.ScheduleUpdateTicketStatus(new UpdateTicketStatusCommand { Id = ticket.Id, Status = TicketStatus.Blue }, ticket.CreatedAt.AddMinutes(45));
            ticketScheduledJobs.Add(new ScheduledJob() { TicketId = ticket.Id, Id = blueJobId });
            string redJobId = _backgroundJobHandler.ScheduleUpdateTicketStatus(new UpdateTicketStatusCommand { Id = ticket.Id, Status = TicketStatus.Red }, ticket.CreatedAt.AddMinutes(60));
            ticketScheduledJobs.Add(new ScheduledJob() { TicketId = ticket.Id, Id = redJobId });

            _ticketJobRepository.AddRange(ticketScheduledJobs);
            await _ticketJobRepository.SaveChangesAsync();

            return new APIResponse<CreateTicketCommand>(request);
        }
    }
}
