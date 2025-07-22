using Microsoft.EntityFrameworkCore;
using Sistema_de_Controle_de_Frequência.Data;
using Sistema_de_Controle_de_Frequência.Models;
using System.Threading.Tasks;

namespace Sistema_de_Controle_de_Frequência.Repositories
{
    public class StatusFrequenciaRepository : IStatusFrequenciaRepository
    {
        private readonly AppDbContext _context;

        public async Task<IEnumerable<StatusFrequencia>> GetAllAsync()
        {
            return await _context.StatusFrequencias.ToListAsync();
        }

        public async Task<StatusFrequencia> GetByIdAsync(int id)
        {
            return await _context.StatusFrequencias.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(StatusFrequencia status)
        {
            _context.StatusFrequencias.Add(status);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(StatusFrequencia status)
        {
            _context.StatusFrequencias.Update(status);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var status = await GetByIdAsync(id);
            if (status != null)
            {
                _context.StatusFrequencias.Remove(status);
                await _context.SaveChangesAsync();
            }
        }

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
