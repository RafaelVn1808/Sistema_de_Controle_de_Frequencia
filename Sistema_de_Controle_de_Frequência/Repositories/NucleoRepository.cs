using Microsoft.EntityFrameworkCore;
using Sistema_de_Controle_de_Frequência.Data;
using Sistema_de_Controle_de_Frequência.Models;

namespace SistemaDeControleDeFrequencia.Repositories
{
    public class NucleoRepository : INucleoRepository
    {
        private readonly AppDbContext _context;

        public NucleoRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Nucleo>> GetAllAsync() => await _context.Nucleos.ToListAsync();
        public async Task<Nucleo> GetByIdAsync(int id) => await _context.Nucleos.FindAsync(id);
        public async Task AddAsync(Nucleo nucleo)
        {
            _context.Nucleos.Add(nucleo);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Nucleo nucleo)
        {
            _context.Nucleos.Update(nucleo);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var nucleo = await _context.Nucleos.FindAsync(id);
            if (nucleo != null)
            {
                _context.Nucleos.Remove(nucleo);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Setor> GetByNomeAsync(string nome) {
            return await _context.Setores
                .Include(s => s.Nucleo)
                .FirstOrDefaultAsync(s => s.Nome == nome);
        }
    }
}