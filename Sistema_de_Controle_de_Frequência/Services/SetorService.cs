using Sistema_de_Controle_de_Frequência.Models;
using Sistema_de_Controle_de_Frequência.Repositories;
using SistemaDeControleDeFrequencia.DTOs.Setor;
using SistemaDeControleDeFrequencia.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeControleDeFrequencia.Services {
    public class SetorService {
        private readonly ISetorRepository _repository;
        private readonly INucleoRepository _nucleoRepository;

        public SetorService(ISetorRepository repository, INucleoRepository nucleoRepository) {
            _repository = repository;
            _nucleoRepository = nucleoRepository;
        }

        public async Task<IEnumerable<SetorResponseDTO>> GetAllAsync() {
            var setores = await _repository.GetAllAsync();
            return setores.Select(s => new SetorResponseDTO {
                Id = s.Id,
                Nome = s.Nome,
                NomeNucleo = s.Nucleo?.Nome
            }).ToList();
        }

        public async Task<SetorResponseDTO> GetByIdAsync(int id) {
            var setor = await _repository.GetByIdAsync(id);
            if (setor == null) return null;

            return new SetorResponseDTO {
                Id = setor.Id,
                Nome = setor.Nome,
                NomeNucleo = setor.Nucleo?.Nome
            };
        }

        public async Task AddAsync(SetorCreateDTO dto) {
            var nucleo = await _nucleoRepository.GetByNomeAsync(dto.NomeNucleo);
            if (nucleo == null)
                throw new ArgumentNullException(nameof(dto.NomeNucleo), "Núcleo informado não foi encontrado.");

            var setor = new Setor {
                Nome = dto.Nome,
                NucleoId = nucleo.Id
            };

            await _repository.AddAsync(setor);
        }

        public async Task UpdateAsync(SetorUpdateDTO dto) {
            var setor = await _repository.GetByIdAsync(dto.Id);
            if (setor == null)
                throw new ArgumentNullException(nameof(dto.Id), "Setor não encontrado.");

            var nucleo = await _nucleoRepository.GetByNomeAsync(dto.NomeNucleo);
            if (nucleo == null)
                throw new ArgumentNullException(nameof(dto.NomeNucleo), "Núcleo informado não foi encontrado.");

            setor.Nome = dto.Nome;
            setor.NucleoId = nucleo.Id;

            await _repository.UpdateAsync(setor);
        }

        public async Task DeleteAsync(int id) {
            await _repository.DeleteAsync(id);
        }
    }
}
