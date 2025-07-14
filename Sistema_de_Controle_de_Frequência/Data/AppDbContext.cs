using Microsoft.EntityFrameworkCore;
using Sistema_de_Controle_de_Frequência.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sistema_de_Controle_de_Frequência.Data {
    public class AppDbContext : DbContext{

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Nucleo> Nucleos { get; set; }
        public DbSet<Setor> Setores { get; set; }
        public DbSet<Servidor> Servidores { get; set; }
        public DbSet<StatusFrequencia> StatusFrequencias { get; set; }
        public DbSet<Frequencia> Frequencias { get; set; }
        public DbSet<FrequenciaServidor> FrequenciasServidores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            // Relacionamento FrequenciaServidor (Muitos-para-Muitos Manual)
            modelBuilder.Entity<FrequenciaServidor>()
                .HasOne(fs => fs.Frequencia)
                .WithMany(f => f.FrequenciasServidores)
                .HasForeignKey(fs => fs.FrequenciaId);

            modelBuilder.Entity<FrequenciaServidor>()
                .HasOne(fs => fs.Servidor)
                .WithMany(s => s.FrequenciasServidores)
                .HasForeignKey(fs => fs.ServidorId);

            // Configuração das entidades (opcionalmente você pode usar FluentAPI aqui)
            modelBuilder.Entity<Nucleo>().ToTable("Nucleo");
            modelBuilder.Entity<Setor>().ToTable("Setor");
            modelBuilder.Entity<Servidor>().ToTable("Servidor");
            modelBuilder.Entity<StatusFrequencia>().ToTable("Status_Frequencia");
            modelBuilder.Entity<Frequencia>().ToTable("Frequencia");
            modelBuilder.Entity<FrequenciaServidor>().ToTable("Frequencia_Servidor");
        }

    }
}
