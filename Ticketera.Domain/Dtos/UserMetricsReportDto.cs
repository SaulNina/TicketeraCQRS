namespace Ticketera.Domain.Dtos;

public class UserMetricsReportDto
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int TotalTicketsCreated { get; set; }
    public int OpenTickets { get; set; }
    public int ClosedTickets { get; set; }
}