using MediatR;
using Ticketera.Application.Interfaces;
using Ticketera.Domain.Dtos;
using Ticketera.Domain.Interfaces;

namespace Ticketera.Application.UseCases.Queries.Reports.GetUserMetricsReport;

public record GetUserMetricsReportQuery : IRequest<byte[]>;

internal sealed record GetUserMetricsReportQueryHandler : IRequestHandler<GetUserMetricsReportQuery, byte[]>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IExcelService _excelService;

    public GetUserMetricsReportQueryHandler(IUnitOfWork unitOfWork, IExcelService excelService)
    {
        _unitOfWork = unitOfWork;
        _excelService = excelService;
    }

    public async Task<byte[]> Handle(GetUserMetricsReportQuery request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.Users.GetAllAsync();

        var reportData = users.Select(u => new UserMetricsReportDto
        {
            UserId = u.UserId,
            Username = u.Username,
            Email = u.Email ?? "Sin Correo",
            TotalTicketsCreated = u.Tickets?.Count ?? 0,
            OpenTickets = u.Tickets?.Count(t => t.Status.ToLower() != "closed" && t.Status.ToLower() != "resuelto") ?? 0,
            ClosedTickets = u.Tickets?.Count(t => t.Status.ToLower() == "closed" || t.Status.ToLower() == "resuelto") ?? 0
        });

        return _excelService.GenerateUserMetricsReport(reportData);
    }
}