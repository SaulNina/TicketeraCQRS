using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticketera.Application.UseCases.Queries.Tickets.GetTickets;

namespace Ticketera.API.Endpoints.Tickets;

[Route("api/tickets")]
[ApiController]
[Authorize] // Este endpoint requiere obligatoriamente el Token JWT
public class GetTicketsEndpoint : ControllerBase
{
    private readonly IMediator _mediator;

    public GetTicketsEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> HandleAsync()
    {
        // Se envía la consulta vacía para obtener la lista de tickets
        var tickets = await _mediator.Send(new GetTicketsQuery());
        return Ok(tickets);
    }
}