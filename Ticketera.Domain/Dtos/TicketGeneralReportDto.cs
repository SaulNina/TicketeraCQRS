namespace Ticketera.Domain.Dtos;

public class TicketGeneralReportDto
{
    public Guid TicketId { get; set; }
    public string ClientName { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string CreatedAtFormatted { get; set; } = null!;
    public int TotalResponses { get; set; }
}