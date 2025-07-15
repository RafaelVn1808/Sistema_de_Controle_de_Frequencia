using Sistema_de_Controle_de_Frequência.Models;
using Sistema_de_Controle_de_Frequência.Repositories;

namespace Sistema_de_Controle_de_Frequência.Services
{
    public class FrequenciaService
    {
        private readonly IFrequenciaRepository _repository;
        private readonly ISetorRepository _setorRepository;
        private readonly IStatusFrequenciaRepository _statusRepository;

        public FrequenciaService(
            IFrequenciaRepository repository,
            ISetorRepository setorRepository,
            IStatusFrequenciaRepository statusRepository)
        {
            _repository = repository;
            _setorRepository = setorRepository;
            _statusRepository = statusRepository;
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
            ValidarFrequencia(frequencia);

            if (!await _setorRepository.ExistsAsync(frequencia.SetorId))
                throw new ArgumentException("Setor informado não existe.");

            if (!await _statusRepository.ExistsAsync(frequencia.StatusFrequenciaId))
                throw new ArgumentException("Status de frequência informado não existe.");

            await _repository.AddAsync(frequencia);
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
    }
}
