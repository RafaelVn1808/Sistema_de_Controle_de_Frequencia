using Sistema_de_Controle_de_Frequência.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sistema_de_Controle_de_Frequência.Repositories
{
    public interface ISetorRepository
    {
        Task<IEnumerable<Setor>> GetAllAsync();
        Task<Setor> GetByIdAsync(int id);
        Task<Setor> GetByNomeAsync(string nome);

        Task AddAsync(Setor setor);
        Task UpdateAsync(Setor setor);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
