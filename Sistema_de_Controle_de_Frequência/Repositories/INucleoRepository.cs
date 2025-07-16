using Sistema_de_Controle_de_Frequência.Models;

namespace SistemaDeControleDeFrequencia.Repositories
{
    public interface INucleoRepository
    {
        Task<IEnumerable<Nucleo>> GetAllAsync();
        Task<Nucleo> GetByIdAsync(int id);
        Task AddAsync(Nucleo nucleo);
        Task UpdateAsync(Nucleo nucleo);
        Task DeleteAsync(int id);
    }
}
