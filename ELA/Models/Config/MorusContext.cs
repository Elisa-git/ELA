using ELA.Models.Enums;
using ELA.Models.Heranca;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;

namespace ELA.Models.Config
{
    public class MorusContext : DbContext
    {
        public MorusContext(DbContextOptions<MorusContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Assunto> Assuntos { get; set; } = null!;
        public DbSet<Artigo> Artigos { get; set; } = null!;
        public DbSet<Pergunta> Perguntas { get; set; } = null!;
        public DbSet<FiqueAtento> FiqueAtentos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pergunta>()
                .HasMany(u => u.Assuntos)
                .WithMany()
                .UsingEntity(
                    l => l.HasOne(typeof(Assunto)).WithMany().OnDelete(DeleteBehavior.Restrict)
                );

            modelBuilder.Entity<Artigo>()
                .HasMany(u => u.Assuntos)
                .WithMany()
                .UsingEntity(
                    l => l.HasOne(typeof(Assunto)).WithMany().OnDelete(DeleteBehavior.Restrict)
                );

            modelBuilder.Entity<FiqueAtento>()
                .HasMany(u => u.Assuntos)
                .WithMany();

            this.SeedDatabaseInicial(modelBuilder);
        }

        private void SeedDatabaseInicial(ModelBuilder builder)
        {
            builder.Entity<Usuario>().HasData
                (new Usuario { Id = 1, CPF = "123.123.123-12", DataNascimento = new DateTime(1987, 09, 13), Email = "bella.swan@email.com", Nome = "Isabella Swan", Senha = "edwardJacob", TipoUsuarioEnum = TipoUsuarioEnum.Responsavel });

            builder.Entity<Assunto>().HasData
                (
                    new Assunto { Id = 1, Descricao = "Infantil" },
                    new Assunto { Id = 2, Descricao = "Meninas" },
                    new Assunto { Id = 3, Descricao = "Meninos" }
                );
        }
    }
}
