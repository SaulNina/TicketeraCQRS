using MediatR;
using Ticketera.Application.Interfaces;
using Ticketera.Domain.Dtos;
using Ticketera.Domain.Interfaces;

namespace Ticketera.Application.UseCases.Queries.Reports.GetTicketsReport;

public record GetTicketsReportQuery : IRequest<byte[]>;

internal sealed record GetTicketsReportQueryHandler : IRequestHandler<GetTicketsReportQuery, byte[]>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IExcelService _excelService;

    public GetTicketsReportQueryHandler(IUnitOfWork unitOfWork, IExcelService excelService)
    {
        _unitOfWork = unitOfWork;
        _excelService = excelService;
    }

    public async Task<byte[]> Handle(GetTicketsReportQuery request, CancellationToken cancellationToken)
    {
        var tickets = await _unitOfWork.Tickets.GetAllAsync();
        
        var reportData = tickets.Select(t => new TicketGeneralReportDto
        {
            TicketId = t.TicketId,
            ClientName = t.User?.Username ?? "Sistema / Anónimo",
            Title = t.Title,
            Status = t.Status,
            CreatedAtFormatted = t.CreatedAt?.ToString("dd/MM/yyyy HH:mm") ?? "N/A",
            TotalResponses = t.Responses?.Count ?? 0
        });

        return _excelService.GenerateTicketsReport(reportData);
    }
}