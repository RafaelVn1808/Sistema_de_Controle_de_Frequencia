using Sistema_de_Controle_de_Frequência.Models;
using SistemaDeControleDeFrequencia.Repositories;

namespace SistemaDeControleDeFrequencia.Services
{
    public class NucleoService
    {
        private readonly INucleoRepository _repository;

        public NucleoService(INucleoRepository repository) => _repository = repository;

        public async Task<IEnumerable<Nucleo>> GetAllAsync() => await _repository.GetAllAsync();
        public async Task<Nucleo> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
        public async Task AddAsync(Nucleo nucleo) => await _repository.AddAsync(nucleo);
        public async Task UpdateAsync(Nucleo nucleo) => await _repository.UpdateAsync(nucleo);
        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);
    }
}
