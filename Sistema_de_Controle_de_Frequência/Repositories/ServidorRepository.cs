using Microsoft.EntityFrameworkCore;
using Sistema_de_Controle_de_Frequência.Data;
using Sistema_de_Controle_de_Frequência.Models;

namespace SistemaDeControleDeFrequencia.Repositories {
    public class ServidorRepository : IServidorRepository {

        private readonly AppDbContext _context;
        public ServidorRepository(AppDbContext context) => _context = context;
        public async Task<IEnumerable<Servidor>> GetAllAsync() => await _context.Servidores
            .Include(s => s.Setor)
            .ToListAsync();
        public async Task<Servidor> GetByIdAsync(int id) => await _context.Servidores
            .Include(s => s.Setor)
            .FirstOrDefaultAsync(s => s.Id == id);
        public async Task AddAsync(Servidor servidor) {
            _context.Servidores.Add(servidor);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Servidor servidor) {
            _context.Servidores.Update(servidor);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id) {
            var servidor = await _context.Servidores.FindAsync(id);
            if (servidor != null) {
                _context.Servidores.Remove(servidor);
                await _context.SaveChangesAsync();
            }
        }

    }
}
