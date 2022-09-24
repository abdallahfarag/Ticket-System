using Microsoft.AspNetCore.Mvc;
using Ticket.Application.Features.Tickets.Commands.CreateTicket;
using Ticket.Application.Features.Tickets.Commands.UpdateTicketStatus;
using Ticket.Application.Features.Tickets.Queries.GetAllTickets;

namespace Ticket.API.Controllers
{
    public class TicketsController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTicketCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll([FromQuery] GetTicketsQuery request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateTicketStatusCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
