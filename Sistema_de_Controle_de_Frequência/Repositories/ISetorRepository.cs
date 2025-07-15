using Sistema_de_Controle_de_Frequência.Models;
using System.Threading.Tasks;

namespace Sistema_de_Controle_de_Frequência.Repositories
{
    public interface ISetorRepository
    {
        Task<bool> ExistsAsync(int id);
    }
}