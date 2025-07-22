using Microsoft.EntityFrameworkCore;
using Sistema_de_Controle_de_Frequência.Models;

namespace Sistema_de_Controle_de_Frequência.Data {
    public class AppDbContext : DbContext{

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Nucleo> Nucleos { get; set; }
        public DbSet<Setor> Setores { get; set; }
        public DbSet<Servidor> Servidores { get; set; }
        public DbSet<Frequencia> Frequencias { get; set; }
        public DbSet<StatusFrequencia> StatusFrequencias { get; set; }
        public DbSet<FrequenciaServidor> FrequenciasServidores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            // Relacionamento FrequenciaServidor (Muitos-para-Muitos Manual)
            modelBuilder.Entity<FrequenciaServidor>()
                .HasOne(fs => fs.Frequencia)
                .WithMany(f => f.FrequenciasServidores)
                .HasForeignKey(fs => fs.FrequenciaId)
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<FrequenciaServidor>()
                .HasOne(fs => fs.Servidor)
                .WithMany(s => s.FrequenciasServidores)
                .HasForeignKey(fs => fs.ServidorId)
                .OnDelete(DeleteBehavior.NoAction); 

            // Configuração das entidades
            modelBuilder.Entity<Nucleo>().ToTable("Nucleo");
            modelBuilder.Entity<Setor>().ToTable("Setor");
            modelBuilder.Entity<Servidor>().ToTable("Servidor");
            modelBuilder.Entity<StatusFrequencia>().ToTable("Status_Frequencia");
            modelBuilder.Entity<Frequencia>().ToTable("Frequencia");
            modelBuilder.Entity<FrequenciaServidor>().ToTable("Frequencia_Servidor");
            modelBuilder.Entity<Setor>()
                .HasIndex(s => s.Nome)
                .IsUnique();

            modelBuilder.Entity<StatusFrequencia>().HasData(
                new StatusFrequencia { Id = 1, Nome = "Pendente", Descricao = "Frequência ainda não enviada ao RH." },
                new StatusFrequencia { Id = 2, Nome = "Recebido", Descricao = "Frequência recebida pelo RH." },
                new StatusFrequencia { Id = 3, Nome = "Regularizado", Descricao = "Frequência conferida e regularizada." },
                new StatusFrequencia { Id = 4, Nome = "Lançado", Descricao = "Frequência lançada oficialmente pelo RH." });
        }


    }
}
