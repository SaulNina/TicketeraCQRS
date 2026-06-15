using MediatR;
using Ticketera.Domain.Interfaces;
using Ticketera.Domain.Models.Entities;

namespace Ticketera.Application.UseCases.Queries.Tickets.GetTickets;

public record GetTicketsQuery : IRequest<IEnumerable<Ticket>>
{
}

internal sealed record GetTicketsQueryHandler : IRequestHandler<GetTicketsQuery, IEnumerable<Ticket>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTicketsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Ticket>> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Tickets.GetAllAsync();
    }
}