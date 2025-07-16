using System.Collections.Generic;
using System.Linq; 
using System.Threading.Tasks;
using Sistema_de_Controle_de_Frequência.Models;
using Sistema_de_Controle_de_Frequência.Repositories;

namespace SistemaDeControleDeFrequencia.Services
{
    public class SetorService
    {
        private readonly ISetorRepository _repository;

        public SetorService(ISetorRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Setor>> GetAllAsync()
        {
            var setores = await _repository.GetAllAsync();
            return setores.ToList();
        }

        
        public async Task<Setor> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
        public async Task AddAsync(Setor setor) => await _repository.AddAsync(setor);
        public async Task UpdateAsync(Setor setor) => await _repository.UpdateAsync(setor);
        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);
    }
}
