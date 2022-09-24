using AutoMapper;
using Ticket.Application.DTOs.Responses;
using Ticket.Application.Features.Tickets.Commands.CreateTicket;
using Ticket.Application.Features.Tickets.Commands.UpdateTicketStatus;

namespace Ticket.Application.Profiles
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<Domain.Entities.Ticket, DTOGetTicketsResponse>();
            CreateMap<CreateTicketCommand, Domain.Entities.Ticket>()
                .ForMember(dest => dest.CreatedAt, act => act.MapFrom(src => DateTime.Now));
            CreateMap<UpdateTicketStatusCommand, Domain.Entities.Ticket>();
        }
    }
}
