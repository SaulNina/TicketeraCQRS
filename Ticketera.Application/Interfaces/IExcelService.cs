using Ticketera.Domain.Dtos;

namespace Ticketera.Application.Interfaces;

public interface IExcelService
{
    byte[] GenerateTicketsReport(IEnumerable<TicketGeneralReportDto> data);
    byte[] GenerateUserMetricsReport(IEnumerable<UserMetricsReportDto> data);
}