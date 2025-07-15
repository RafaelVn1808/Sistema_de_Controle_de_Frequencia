using Microsoft.EntityFrameworkCore;
using Sistema_de_Controle_de_Frequência.Data;
using System.Threading.Tasks;

namespace Sistema_de_Controle_de_Frequência.Repositories
{
    public class StatusFrequenciaRepository : IStatusFrequenciaRepository
    {
        private readonly AppDbContext _context;

        public StatusFrequenciaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.StatusFrequencias.AnyAsync(s => s.Id == id);
        }
    }
}
