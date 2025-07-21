using Sistema_de_Controle_de_Frequência.Models;
using Sistema_de_Controle_de_Frequência.Repositories;
using SistemaDeControleDeFrequencia.DTOs.Servidor;
using SistemaDeControleDeFrequencia.Repositories;

namespace SistemaDeControleDeFrequencia.Services {
    public class ServidorService {
        private readonly IServidorRepository _repository;
        private readonly ISetorRepository _setorRepository;

        public ServidorService(IServidorRepository repository, ISetorRepository setorRepository) {
            _repository = repository;
            _setorRepository = setorRepository;
        }

        public async Task<IEnumerable<ServidorResponseDTO>> GetAllAsync() {
            var servidores = await _repository.GetAllAsync();

            return servidores.Select(s => new ServidorResponseDTO {
                Id = s.Id,
                Nome = s.Nome,
                Matricula = s.Matricula,
                NomeSetor = s.Setor?.Nome
            });
        }

        public async Task<ServidorResponseDTO> GetByIdAsync(int id) {
            var servidor = await _repository.GetByIdAsync(id);
            if (servidor == null)
                return null;

            return new ServidorResponseDTO {
                Id = servidor.Id,
                Nome = servidor.Nome,
                Matricula = servidor.Matricula,
                NomeSetor = servidor.Setor?.Nome
            };
        }

        public async Task AddAsync(ServidorCreateDTO dto) {
            var setor = await _setorRepository.GetByNomeAsync(dto.NomeSetor);
            if (setor == null)
                throw new Exception("Setor informado não foi encontrado.");

            var servidor = new Servidor {
                Nome = dto.Nome,
                Matricula = dto.Matricula,
                Setor = setor
            };

            await _repository.AddAsync(servidor);
        }

        public async Task UpdateAsync(ServidorUpdateDTO dto) {
            var servidor = await _repository.GetByIdAsync(dto.Id);
            if (servidor == null)
                throw new Exception("Servidor não encontrado.");

            var setor = await _setorRepository.GetByNomeAsync(dto.NomeSetor);
            if (setor == null)
                throw new Exception("Setor informado não foi encontrado.");

            servidor.Nome = dto.Nome;
            servidor.Matricula = dto.Matricula;
            servidor.Setor = setor;

            await _repository.UpdateAsync(servidor);
        }

        public async Task DeleteAsync(int id) {
            await _repository.DeleteAsync(id);
        }
    }
}
