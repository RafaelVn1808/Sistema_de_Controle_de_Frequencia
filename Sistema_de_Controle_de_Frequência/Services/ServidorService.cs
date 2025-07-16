using Sistema_de_Controle_de_Frequência.Models;
using SistemaDeControleDeFrequencia.Repositories;

namespace SistemaDeControleDeFrequencia.Services {
    public class ServidorService {

        private readonly IServidorRepository _repository;
        public ServidorService(IServidorRepository repository) {
            _repository = repository;
        }
        public async Task<IEnumerable<Servidor>> GetAllAsync() {
            return await _repository.GetAllAsync();
        }
        public async Task<Servidor> GetByIdAsync(int id) {
            return await _repository.GetByIdAsync(id);
        }
        public async Task AddAsync(Servidor servidor) {
            await _repository.AddAsync(servidor);
        }
        public async Task UpdateAsync(Servidor servidor) {
            await _repository.UpdateAsync(servidor);
        }
        public async Task DeleteAsync(int id) {
            await _repository.DeleteAsync(id);
        }

    }
}
