using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using Sistema_de_Controle_de_Frequência.Data;
using Sistema_de_Controle_de_Frequência.Models;
using Sistema_de_Controle_de_Frequência.Repositories;
using SistemaDeControleDeFrequencia.DTOs.Frequencia;
using SistemaDeControleDeFrequencia.DTOs.Servidor;


namespace Sistema_de_Controle_de_Frequência.Services
{
    public class FrequenciaService
    {
        private readonly IFrequenciaRepository _repository;
        private readonly ISetorRepository _setorRepository;
        private readonly IStatusFrequenciaRepository _statusRepository;
        private readonly AppDbContext _context;


        public FrequenciaService(
            IFrequenciaRepository repository,
            ISetorRepository setorRepository,
            IStatusFrequenciaRepository statusRepository,
            AppDbContext context)
        {
            _repository = repository;
            _setorRepository = setorRepository;
            _statusRepository = statusRepository;
            _context = context;
        }

        public async Task<IEnumerable<Frequencia>> GetAllFrequenciasAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<FrequenciaResponseDTO> GetByIdAsync(int id)
        {
            var frequencia = await _repository.GetByIdAsync(id);

            if (frequencia == null) return null;

            return new FrequenciaResponseDTO
            {
                Id = frequencia.Id,
                MesReferencia = frequencia.MesReferencia,
                DataEnvio = frequencia.DataEnvio,
                SetorNome = frequencia.Setor.Nome,
                StatusFrequenciaNome = frequencia.StatusFrequencia.Nome
            };
        }

        public async Task<List<FrequenciaResponseDTO>> GetAllAsync()
        {

            var frequencias = await _repository.GetAllAsync();

            return frequencias.Select(f => new FrequenciaResponseDTO
            {
                Id = f.Id,
                MesReferencia = f.MesReferencia,
                DataEnvio = f.DataEnvio,
                SetorNome = f.Setor.Nome,
                StatusFrequenciaNome = f.StatusFrequencia.Nome
            }).ToList();
        }

        public async Task AddAsync(FrequenciaCreateDTO dto) {
            var frequencia = new Frequencia {
                MesReferencia = dto.MesReferencia,
                SetorId = dto.SetorId,
                
            };

            _context.Frequencias.Add(frequencia);
            await _context.SaveChangesAsync();
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
