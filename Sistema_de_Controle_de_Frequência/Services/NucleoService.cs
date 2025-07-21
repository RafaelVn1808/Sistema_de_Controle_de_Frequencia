using Sistema_de_Controle_de_Frequência.Models;
using SistemaDeControleDeFrequencia.Repositories;
using SistemaDeControleDeFrequencia.DTOs.Nucleo;

namespace SistemaDeControleDeFrequencia.Services {
    public class NucleoService {
        private readonly INucleoRepository _repository;

        public NucleoService(INucleoRepository repository) {
            _repository = repository;
        }

        public async Task<IEnumerable<NucleoResponseDTO>> GetAllAsync() {
            var nucleos = await _repository.GetAllAsync();
            return nucleos.Select(n => new NucleoResponseDTO {
                Id = n.Id,
                Nome = n.Nome,
                Cidade = n.Cidade
            });
        }

        public async Task<NucleoResponseDTO> GetByIdAsync(int id) {
            var nucleo = await _repository.GetByIdAsync(id);
            if (nucleo == null) return null;

            return new NucleoResponseDTO {
                Id = nucleo.Id,
                Nome = nucleo.Nome,
                Cidade = nucleo.Cidade
            };
        }

        public async Task AddAsync(NucleoCreateDTO dto) {
            var nucleo = new Nucleo {
                Nome = dto.Nome,
                Cidade = dto.Cidade
            };
            await _repository.AddAsync(nucleo);
        }

        public async Task UpdateAsync(NucleoUpdateDTO dto) {
            var nucleo = await _repository.GetByIdAsync(dto.Id);
            if (nucleo == null)
                throw new Exception("Núcleo não encontrado.");

            nucleo.Nome = dto.Nome;
            nucleo.Cidade = dto.Cidade;
            await _repository.UpdateAsync(nucleo);
        }

        public async Task DeleteAsync(int id) {
            await _repository.DeleteAsync(id);
        }
    }
}