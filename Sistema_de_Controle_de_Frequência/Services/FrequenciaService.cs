using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using Sistema_de_Controle_de_Frequência.Data;
using Sistema_de_Controle_de_Frequência.Models;
using Sistema_de_Controle_de_Frequência.Repositories;
using SistemaDeControleDeFrequencia.DTOs.Frequencia;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
                StatusNome = frequencia.StatusFrequencia.Nome
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
                StatusNome = f.StatusFrequencia.Nome
            }).ToList();
        }

        public async Task AddAsync(FrequenciaCreateDTO dto)
        {
            if (await _repository.ExistsByMesReferenciaAndSetorAsync(dto.MesReferencia, dto.SetorId))
                throw new Exception("Já existe uma frequência cadastrada para este Setor e Mês/Ano.");

            var frequencia = new Frequencia
            {
                MesReferencia = dto.MesReferencia,
                SetorId = dto.SetorId,
                DataEnvio = DateTime.Now,
                StatusFrequenciaId = 1 // 'Pendente'
            };

            _context.Frequencias.Add(frequencia);
            await _context.SaveChangesAsync();

            var servidoresDoSetor = await _context.Servidores
                .Where(s => s.SetorId == dto.SetorId)
                .ToListAsync();

            var vinculos = servidoresDoSetor.Select(servidor => new FrequenciaServidor
            {
                FrequenciaId = frequencia.Id,
                ServidorId = servidor.Id
            }).ToList();

            _context.FrequenciasServidores.AddRange(vinculos);
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

        public byte[] GerarRelatorioPdf(IEnumerable<Frequencia> frequencias)
        {
            var documento = new FrequenciaReportDocument(frequencias);
            return documento.GeneratePdf();
        }

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

        public async Task AlterarStatusAsync(int frequenciaId, int novoStatusId)
        {
            var frequencia = await _repository.GetByIdAsync(frequenciaId);
            if (frequencia == null)
                throw new Exception("Frequência não encontrada.");

            if (frequencia.StatusFrequencia.Nome == "Lançado")
                throw new Exception("Não é possível alterar uma frequência já Lançada.");

            var novoStatus = await _statusRepository.GetByIdAsync(novoStatusId);
            if (novoStatus == null)
                throw new Exception("Status informado não existe.");

            frequencia.StatusFrequenciaId = novoStatusId;

            await _repository.UpdateAsync(frequencia);
        }

        public async Task UpdateStatusAsync(FrequenciaUpdateStatusDTO dto)
        {
            var frequencia = await _context.Frequencias.FindAsync(dto.FrequenciaId);
            if (frequencia == null)
                throw new Exception("Frequência não encontrada.");

            
            if (!Enum.IsDefined(typeof(StatusFrequencia), dto.StatusFrequenciaId))
                throw new Exception("Status inválido.");

            frequencia.StatusFrequenciaId = dto.StatusFrequenciaId;
            await _context.SaveChangesAsync();
        }

    }
}
