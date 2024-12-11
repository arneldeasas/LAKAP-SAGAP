using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using static LAKAPSAGAP.BlazorServer.Pages.Stocks.ReceiveStockForm;

namespace LAKAPSAGAP.BlazorServer.QuestPDF;

public class ReliefRequestForm
{
    public readonly ReliefRequestDetailViewModel _data;

    public ReliefRequestForm(ReliefRequestDetailViewModel data)
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
            column.Item().Text("Relief Request Form")
                .FontSize(20)
                .Bold()
                .FontColor(Color.FromHex("#1151F3"));

            column.Item().Text($"Requested on {_data.DateRequested:MM/dd/yyyy HH:mm} and Expected to arrive on {_data.TargetDateToReceive:MM/dd/yyyy HH:mm}")
                .FontSize(10)
                .FontColor(Color.FromHex("#4F4F50"));
        });
    }

    void ComposeContent(IContainer container)
    {
        container.PaddingVertical(10).Column(column =>
        {
            column.Item().PaddingBottom(5).Text("Request Details")
                .FontSize(16)
                .Bold();

            column.Item().Text(text =>
            {
                text.Line($"Reason for Request: {_data.Reason}; {_data.SpecificReason}");
                text.Line($"Requested By: {_data.RequestedBy}");
                text.Line($"Expected Number of Recipients: {_data.NumberOfRecipients}");
                text.Line($"Additional Notes: {_data.AdditionalNotes}");
            });
            column.Item().PaddingBottom(10).Text("Receiver Details")
                .FontSize(16)
                .Bold();

            column.Item().Text(text =>
            {
                text.Line($"Name: {_data.ReceiverName}");
                text.Line($"Address: {_data.ReceiverAddress}");
                text.Line($"Contact Detail: {_data.ContactNumber}");
            });

            column.Item().PaddingVertical(20).Text("Requested Items")
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
                    header.Cell().Border(1).BorderColor(Color.FromHex("#4F4F50")).Element(CellContent => CellContent.Padding(4)).Text("Item Id").Bold().AlignCenter().FontColor(Color.FromHex("#1151F3"));
                    header.Cell().Border(1).BorderColor(Color.FromHex("#4F4F50")).Element(CellContent => CellContent.Padding(4)).Text("Item Name").Bold().AlignCenter().FontColor(Color.FromHex("#1151F3"));
                    header.Cell().Border(1).BorderColor(Color.FromHex("#4F4F50")).Element(CellContent => CellContent.Padding(4)).Text("Quantity").Bold().AlignCenter().FontColor(Color.FromHex("#1151F3"));
                });

                // Add table rows dynamically
                foreach (var item in _data.RequestList.Where(x => x.UnitId is not null && x.Quantity > 0).ToList())
                {
                    table.Cell().Border(1).BorderColor(Color.FromHex("#4F4F50")).Element(CellContent => CellContent.Padding(3)).Text(item.UnitId).FontSize(8);
                    table.Cell().Border(1).BorderColor(Color.FromHex("#4F4F50")).Element(CellContent => CellContent.Padding(3)).Text(item.UnitName).FontSize(8);
                    table.Cell().Border(1).BorderColor(Color.FromHex("#4F4F50")).Element(CellContent => CellContent.Padding(3)).Text(item.Quantity.ToString()).FontSize(8);
                }
            });
        });
    }

}
