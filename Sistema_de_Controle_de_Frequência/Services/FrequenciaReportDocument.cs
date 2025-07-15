using ClosedXML.Excel;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Sistema_de_Controle_de_Frequência.Models;

public class FrequenciaReportDocument : IDocument
{
    private readonly IEnumerable<Frequencia> _frequencias;

    public FrequenciaReportDocument(IEnumerable<Frequencia> frequencias)
    {
        _frequencias = frequencias;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(30);
            page.Header().Text("Relatório de Frequências").FontSize(20).SemiBold().AlignCenter();
            page.Content().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(3);
                    columns.RelativeColumn(2);
                    columns.RelativeColumn(2);
                });

                table.Header(header =>
                {
                    header.Cell().Text("Mês de Referência").SemiBold();
                    header.Cell().Text("Data Envio").SemiBold();
                    header.Cell().Text("Status").SemiBold();
                });

                foreach (var freq in _frequencias)
                {
                    table.Cell().Text(freq.MesReferencia);
                    table.Cell().Text(freq.DataEnvio.ToShortDateString());
                    table.Cell().Text(freq.StatusFrequencia?.Descricao ?? "-");
                }
            });
        });
    }

    
}
