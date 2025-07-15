using System.Threading.Tasks;

namespace Sistema_de_Controle_de_Frequência.Repositories
{
    public interface IStatusFrequenciaRepository
    {
        Task<bool> ExistsAsync(int id);
    }
}
