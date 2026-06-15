using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticketera.Application.UseCases.Queries.Reports.GetTicketsReport;
using Ticketera.Application.UseCases.Queries.Reports.GetUserMetricsReport;

namespace Ticketera.API.Endpoints.Reports;

[Route("api/reports")]
[ApiController]
[Authorize] 
public class ReportsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReportsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("tickets/excel")]
    public async Task<IActionResult> DownloadTicketsReportAsync()
    {
        var fileBytes = await _mediator.Send(new GetTicketsReportQuery());
        
        string fileName = $"Reporte_General_Tickets_{DateTime.Now:yyyyMMdd}.xlsx";
        return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
    }

    [HttpGet("users-metrics/excel")]
    public async Task<IActionResult> DownloadUserMetricsReportAsync()
    {
        var fileBytes = await _mediator.Send(new GetUserMetricsReportQuery());
        
        string fileName = $"Reporte_Metricas_Usuarios_{DateTime.Now:yyyyMMdd}.xlsx";
        return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
    }
}