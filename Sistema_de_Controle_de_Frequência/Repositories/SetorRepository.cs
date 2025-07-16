using Microsoft.EntityFrameworkCore;
using Sistema_de_Controle_de_Frequência.Data;
using Sistema_de_Controle_de_Frequência.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sistema_de_Controle_de_Frequência.Repositories
{
    public class SetorRepository : ISetorRepository
    {
        private readonly AppDbContext _context;

        public SetorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Setor>> GetAllAsync()
        {
            return await _context.Setores.Include(s => s.Nucleo).ToListAsync();
        }

        public async Task<Setor> GetByIdAsync(int id)
        {
            return await _context.Setores.Include(s => s.Nucleo)
                                         .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(Setor setor)
        {
            _context.Setores.Add(setor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Setor setor)
        {
            _context.Setores.Update(setor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var setor = await GetByIdAsync(id);
            if (setor != null)
            {
                _context.Setores.Remove(setor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Setores.AnyAsync(s => s.Id == id);
        }
    }
}
