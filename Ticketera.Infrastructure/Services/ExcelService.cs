using ClosedXML.Excel;
using System.Collections.Generic;
using System.IO;
using Ticketera.Application.Interfaces;
using Ticketera.Domain.Dtos;


namespace Ticketera.Infrastructure.Services;

public class ExcelService : IExcelService
{
    public byte[] GenerateTicketsReport(IEnumerable<TicketGeneralReportDto> data)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Reporte de Tickets");

        var headerRow = worksheet.Row(1);
        headerRow.Style.Font.Bold = true;
        headerRow.Style.Fill.BackgroundColor = XLColor.FromHtml("#1F4E78"); // Azul Oscuro Ejecutivo
        headerRow.Style.Font.FontColor = XLColor.White;
        headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

        worksheet.Cell(1, 1).Value = "ID TICKET";
        worksheet.Cell(1, 2).Value = "CLIENTE / USUARIO";
        worksheet.Cell(1, 3).Value = "ASUNTO / TÍTULO";
        worksheet.Cell(1, 4).Value = "ESTADO";
        worksheet.Cell(1, 5).Value = "FECHA CREACIÓN";
        worksheet.Cell(1, 6).Value = "N° RESPUESTAS";

        int currentRow = 2;
        foreach (var item in data)
        {
            worksheet.Cell(currentRow, 1).Value = item.TicketId.ToString();
            worksheet.Cell(currentRow, 2).Value = item.ClientName;
            worksheet.Cell(currentRow, 3).Value = item.Title;
            worksheet.Cell(currentRow, 4).Value = item.Status;
            worksheet.Cell(currentRow, 5).Value = item.CreatedAtFormatted;
            worksheet.Cell(currentRow, 6).Value = item.TotalResponses;
            currentRow++;
        }

        if (currentRow > 2)
        {
            var range = worksheet.Range(1, 1, currentRow - 1, 6);
            range.CreateTable();
        }

        worksheet.Columns().AdjustToContents(); 

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    public byte[] GenerateUserMetricsReport(IEnumerable<UserMetricsReportDto> data)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Métricas de Usuarios");

        var headerRow = worksheet.Row(1);
        headerRow.Style.Font.Bold = true;
        headerRow.Style.Fill.BackgroundColor = XLColor.FromHtml("#2E75B6"); 
        headerRow.Style.Font.FontColor = XLColor.White;
        headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

        worksheet.Cell(1, 1).Value = "ID USUARIO";
        worksheet.Cell(1, 2).Value = "USERNAME";
        worksheet.Cell(1, 3).Value = "EMAIL";
        worksheet.Cell(1, 4).Value = "TOTAL TICKETS";
        worksheet.Cell(1, 5).Value = "TICKETS ABIERTOS";
        worksheet.Cell(1, 6).Value = "TICKETS CERRADOS";

        int currentRow = 2;
        foreach (var item in data)
        {
            worksheet.Cell(currentRow, 1).Value = item.UserId.ToString();
            worksheet.Cell(currentRow, 2).Value = item.Username;
            worksheet.Cell(currentRow, 3).Value = item.Email;
            worksheet.Cell(currentRow, 4).Value = item.TotalTicketsCreated;
            worksheet.Cell(currentRow, 5).Value = item.OpenTickets;
            worksheet.Cell(currentRow, 6).Value = item.ClosedTickets;
            currentRow++;
        }

        if (currentRow > 2)
        {
            var range = worksheet.Range(1, 1, currentRow - 1, 6);
            range.CreateTable();
        }

        worksheet.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}