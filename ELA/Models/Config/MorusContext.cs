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
            modelBuilder.Entity<Artigo>()
                .HasOne(e => e.Usuario)
                .WithMany()
                .HasForeignKey(e => e.UsuarioId)
                .IsRequired();

            modelBuilder.Entity<Artigo>()
                .HasMany(e => e.Assuntos)
                .WithMany();
            
            modelBuilder.Entity<Pergunta>()
                .HasMany(e => e.Assuntos)
                .WithMany();
            
            modelBuilder.Entity<FiqueAtento>()
                .HasMany(e => e.Assuntos)
                .WithMany();
        }

    }
}
