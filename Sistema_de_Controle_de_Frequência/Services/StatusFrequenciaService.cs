using Sistema_de_Controle_de_Frequência.Models;
using Sistema_de_Controle_de_Frequência.Repositories;
using SistemaDeControleDeFrequencia.DTOs.StatusFrequencia;
using SistemaDeControleDeFrequencia.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeControleDeFrequencia.Services
{
    public class StatusFrequenciaService
    {
        private readonly IStatusFrequenciaRepository _repository;

        public StatusFrequenciaService(IStatusFrequenciaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<StatusFrequenciaResponseDTO>> GetAllAsync()
        {
            var status = await _repository.GetAllAsync();
            return status.Select(s => new StatusFrequenciaResponseDTO
            {
                Id = s.Id,
                Nome = s.Nome,
                Descricao = s.Descricao
            });
        }

        public async Task<StatusFrequenciaResponseDTO> GetByIdAsync(int id)
        {
            var status = await _repository.GetByIdAsync(id);
            if (status == null) return null;

            return new StatusFrequenciaResponseDTO
            {
                Id = status.Id,
                Nome = status.Nome,
                Descricao = status.Descricao
            };
        }

        public async Task AddAsync(StatusFrequenciaCreateDTO dto)
        {
            var status = new StatusFrequencia
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao
            };
            await _repository.AddAsync(status);
        }

        public async Task UpdateAsync(StatusFrequenciaUpdateDTO dto)
        {
            var status = await _repository.GetByIdAsync(dto.Id);
            if (status == null)
                throw new Exception("Status não encontrado.");

            status.Nome = dto.Nome;
            status.Descricao = dto.Descricao;

            await _repository.UpdateAsync(status);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
