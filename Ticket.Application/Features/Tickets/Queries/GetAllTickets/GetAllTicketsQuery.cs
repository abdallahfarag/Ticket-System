using AutoMapper;
using MediatR;
using Ticket.Application.DTOs.Responses;
using Ticket.Application.Interfaces.Repositories;
using Ticket.Application.Wrappers;

namespace Ticket.Application.Features.Tickets.Queries.GetAllTickets
{
    public class GetTicketsQuery : IRequest<APIResponse<PagedResponse<DTOGetTicketsResponse>>>
    {
        public int? Page { get; set; } = 1;
        public int? PageSize { get; set; } = 5;
    }
    public class GetTicketsHandler : IRequestHandler<GetTicketsQuery, APIResponse<PagedResponse<DTOGetTicketsResponse>>>
    {
        private readonly IRepository<Domain.Entities.Ticket> _repository;
        private readonly IMapper _mapper;
        public GetTicketsHandler(IRepository<Domain.Entities.Ticket> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<APIResponse<PagedResponse<DTOGetTicketsResponse>>> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
        {
            var tickets = await _repository.GetAllAsync(request.Page.Value, request.PageSize.Value);
            var itemsResult = _mapper.Map<List<Domain.Entities.Ticket>, List<DTOGetTicketsResponse>>(tickets.Items);
            return new APIResponse<PagedResponse<DTOGetTicketsResponse>>(new PagedResponse<DTOGetTicketsResponse>(tickets.CurrentPage, tickets.TotalPages, tickets.PageSize, tickets.TotalCount, tickets.HasPrevious, tickets.HasNext, itemsResult));
        }
    }
}
