using System.Collections.Generic;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using static LAKAPSAGAP.BlazorServer.Pages.Stocks.ReceiveStockForm;

namespace LAKAPSAGAP.BlazorServer.QuestPDF;

public class ReceivingReceipt
{
    public readonly ReceiveReliefVM _data;

    public ReceivingReceipt(ReceiveReliefVM data)
    {
        _data = data;
    }


    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(30);
            page.Header().Element(ComposeHeader);
            page.Content().Element(ComposeContent);
        });
    }

    void ComposeHeader(IContainer container)
    {
        container.Column(column =>
        {
            column.Item().Text("Relief Received Report")
                .FontSize(20)
                .Bold()
                .FontColor(Color.FromHex("#1151F3"));

            column.Item().Text($"Generated on: {_data.ReceivedDate:MM/dd/yyyy HH:mm}")
                .FontSize(10)
                .FontColor(Color.FromHex("#4F4F50"));
        });
    }

    void ComposeContent(IContainer container)
    {
        container.PaddingVertical(10).Column(column =>
        {
            column.Item().PaddingBottom(10).Text("Delivery & Acquisition Details")
                .FontSize(16)
                .Bold();

            column.Item().Text(text =>
            {
                text.Line($"Acquisition Type: {_data.AcquisitionType}");
                text.Line($"Received By: {_data.ReceivedBy}");
                text.Line($"Plate No.: {_data.PlateNo}");
                text.Line($"Driver Name: {_data.DriverName}");
                text.Line($"Received From: {_data.ReceivedFrom ?? "N/A"}");
                text.Line($"Received Date: {_data.ReceivedDate:MM/dd/yyyy}");
                text.Line($"Warehouse ID: {_data.WarehouseId}");
            });

            column.Item().PaddingVertical(20).Text("Received Items")
                .FontSize(16)
                .Bold();

            column.Item().Border(1).BorderColor(Color.FromHex("#4F4F50")).Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                // Table header
                table.Header(header =>
                {
                    header.Cell().Border(1).BorderColor(Color.FromHex("#4F4F50")).Element(CellContent => CellContent.Padding(4)).Text("Item ID").Bold().AlignCenter().FontColor(Color.FromHex("#1151F3"));
                    header.Cell().Border(1).BorderColor(Color.FromHex("#4F4F50")).Element(CellContent => CellContent.Padding(4)).Text("Name").Bold().AlignCenter().FontColor(Color.FromHex("#1151F3"));
                    header.Cell().Border(1).BorderColor(Color.FromHex("#4F4F50")).Element(CellContent => CellContent.Padding(4)).Text("Category").Bold().AlignCenter().FontColor(Color.FromHex("#1151F3"));
                    header.Cell().Border(1).BorderColor(Color.FromHex("#4F4F50")).Element(CellContent => CellContent.Padding(4)).Text("Quantity").Bold().AlignCenter().FontColor(Color.FromHex("#1151F3"));
                    header.Cell().Border(1).BorderColor(Color.FromHex("#4F4F50")).Element(CellContent => CellContent.Padding(4)).Text("Expiry Date").Bold().AlignCenter().FontColor(Color.FromHex("#1151F3"));
                    header.Cell().Border(1).BorderColor(Color.FromHex("#4F4F50")).Element(CellContent => CellContent.Padding(4)).Text("Location").Bold().AlignCenter().FontColor(Color.FromHex("#1151F3"));
                });

                // Add table rows dynamically
                foreach (var item in _data.StockDetailList.Where(x => x.StockItemId is not null ).ToList())
                {
                    table.Cell().Border(1).BorderColor(Color.FromHex("#4F4F50")).Element(CellContent => CellContent.Padding(3)).Text(item.StockItemId).FontSize(8);
                    table.Cell().Border(1).BorderColor(Color.FromHex("#4F4F50")).Element(CellContent => CellContent.Padding(3)).Text(item.StockItemName).FontSize(8);
                    table.Cell().Border(1).BorderColor(Color.FromHex("#4F4F50")).Element(CellContent => CellContent.Padding(3)).Text(item.CategoryName).FontSize(8);
                    table.Cell().Border(1).BorderColor(Color.FromHex("#4F4F50")).Element(CellContent => CellContent.Padding(3)).Text($"{item.Quantity.ToString()} {item.UomName}").FontSize(8);
                    table.Cell().Border(1).BorderColor(Color.FromHex("#4F4F50")).Element(CellContent => CellContent.Padding(3)).Text(item.ExpiryDate != DateTime.MinValue
                        ? item.ExpiryDate.ToString("MM/dd/yyyy")
                        : "N/A").FontSize(8);
                    table.Cell().Border(1).BorderColor(Color.FromHex("#4F4F50")).Element(CellContent => CellContent.Padding(3)).Text($"{item.FloorName}, {item.RackName}").FontSize(8);
                }
            });
        });
    }

}
