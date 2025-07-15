using Sistema_de_Controle_de_Frequência.Models;
using Sistema_de_Controle_de_Frequência.Repositories;


namespace Sistema_de_Controle_de_Frequência.Services
{
    public class FrequenciaService
    {
        private readonly IFrequenciaRepository _repository;

        public FrequenciaService(IFrequenciaRepository repository)
        {
            _repository = repository;
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
            await _repository.AddAsync(frequencia);
        }

        public async Task UpdateFrequenciaAsync(Frequencia frequencia)
        {
            await _repository.UpdateAsync(frequencia);
        }

        public async Task DeleteFrequenciaAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
