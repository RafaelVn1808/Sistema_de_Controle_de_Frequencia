using Sistema_de_Controle_de_Frequência.Models;


namespace Sistema_de_Controle_de_Frequência.Repositories
{
    public interface IFrequenciaRepository
    {
        
     Task<IEnumerable<Frequencia>> GetAllAsync();
     Task<Frequencia> GetByIdAsync(int id);
     Task AddAsync(Frequencia frequencia);
     Task UpdateAsync(Frequencia frequencia);
     Task DeleteAsync(int id);
        
    }
}
