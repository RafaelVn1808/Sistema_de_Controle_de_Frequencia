using Sistema_de_Controle_de_Frequência.Models;

namespace SistemaDeControleDeFrequencia.Repositories
{
    public interface IServidorRepository
    {
        Task<IEnumerable<Servidor>> GetAllAsync();
        Task<Servidor> GetByIdAsync(int id);
        Task AddAsync(Servidor servidor);
        Task UpdateAsync(Servidor servidor);
        Task DeleteAsync(int id);

    }
}
