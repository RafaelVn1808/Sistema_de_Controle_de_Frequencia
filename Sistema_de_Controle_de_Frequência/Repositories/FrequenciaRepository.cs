using Microsoft.EntityFrameworkCore;
using Sistema_de_Controle_de_Frequência.Data;
using Sistema_de_Controle_de_Frequência.Models;


namespace Sistema_de_Controle_de_Frequência.Repositories
{
    public class FrequenciaRepository : IFrequenciaRepository
    {
        private readonly AppDbContext _context;

        public FrequenciaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Frequencia>> GetAllAsync()
        {
            return await _context.Frequencias
        .Include(f => f.Setor)
        .Include(f => f.StatusFrequencia)
        .ToListAsync();
        }

        public async Task<Frequencia> GetByIdAsync(int id)
        {
            return await _context.Frequencias
        .Include(f => f.StatusFrequencia)
        .Include(f => f.Setor)
        .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task AddAsync(Frequencia frequencia)
        {
            _context.Frequencias.Add(frequencia);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Frequencia frequencia)
        {
            _context.Frequencias.Update(frequencia);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var frequencia = await _context.Frequencias.FindAsync(id);
            if (frequencia != null)
            {
                _context.Frequencias.Remove(frequencia);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsBySetorAndMesReferenciaAsync(int setorId, string mesReferencia)
        {
            return await _context.Frequencias
                .AnyAsync(f => f.SetorId == setorId && f.MesReferencia == mesReferencia);
        }
    }
}
