using Sistema_de_Controle_de_Frequência.Models;
using System.Threading.Tasks;

namespace Sistema_de_Controle_de_Frequência.Repositories
{
    public interface IStatusFrequenciaRepository
    {
        Task<IEnumerable<StatusFrequencia>> GetAllAsync();
        Task<StatusFrequencia> GetByIdAsync(int id);
        Task AddAsync(StatusFrequencia status);
        Task UpdateAsync(StatusFrequencia status);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
