using QuestPDF.Fluent;
using Sistema_de_Controle_de_Frequência.Models;
using Sistema_de_Controle_de_Frequência.Repositories;
using ClosedXML.Excel;

namespace Sistema_de_Controle_de_Frequência.Services
{
    public class FrequenciaService
    {
        private readonly IFrequenciaRepository _repository;
        private readonly ISetorRepository _setorRepository;
        private readonly IStatusFrequenciaRepository _statusRepository;

        public FrequenciaService(
            IFrequenciaRepository repository,
            ISetorRepository setorRepository,
            IStatusFrequenciaRepository statusRepository)
        {
            _repository = repository;
            _setorRepository = setorRepository;
            _statusRepository = statusRepository;
        }

        public async Task<IEnumerable<Frequencia>> GetAllFrequenciasAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Frequencia> GetFrequenciaByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddFrequenciaAsync(Frequencia frequencia)
        {
            ValidarFrequencia(frequencia);

            if (!await _setorRepository.ExistsAsync(frequencia.SetorId))
                throw new ArgumentException("Setor informado não existe.");

            if (!await _statusRepository.ExistsAsync(frequencia.StatusFrequenciaId))
                throw new ArgumentException("Status de frequência informado não existe.");

            await _repository.AddAsync(frequencia);
        }

        public async Task UpdateFrequenciaAsync(Frequencia frequencia)
        {
            ValidarFrequencia(frequencia);

            if (!await _setorRepository.ExistsAsync(frequencia.SetorId))
                throw new ArgumentException("Setor informado não existe.");

            if (!await _statusRepository.ExistsAsync(frequencia.StatusFrequenciaId))
                throw new ArgumentException("Status de frequência informado não existe.");

            await _repository.UpdateAsync(frequencia);
        }

        public async Task DeleteFrequenciaAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        private void ValidarFrequencia(Frequencia frequencia)
        {
            if (frequencia.DataEnvio.Date > DateTime.Today)
                throw new ArgumentException("Data de envio não pode ser uma data futura.");
        }

        //Método para gerar o relatório PDF
        public byte[] GerarRelatorioPdf(IEnumerable<Frequencia> frequencias)
        {
            var documento = new FrequenciaReportDocument(frequencias);
            return documento.GeneratePdf();
        }

        //étodo para gerar o relatório Excel
        public byte[] GerarRelatorioExcel(IEnumerable<Frequencia> frequencias)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Relatório de Frequência");

            worksheet.Cell(1, 1).Value = "Mês de Referência";
            worksheet.Cell(1, 2).Value = "Data Envio";
            worksheet.Cell(1, 3).Value = "Status";

            int linha = 2;
            foreach (var freq in frequencias)
            {
                worksheet.Cell(linha, 1).Value = freq.MesReferencia;
                worksheet.Cell(linha, 2).Value = freq.DataEnvio.ToShortDateString();
                worksheet.Cell(linha, 3).Value = freq.StatusFrequencia?.Descricao ?? "-";
                linha++;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }

    }
}
