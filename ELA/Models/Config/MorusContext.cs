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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasMany<Artigo>()
                .WithOne();
                // .HasForeignKey(u => u.UsuarioId)
                // .IsRequired();

            modelBuilder.Entity<Artigo>()
                .HasMany(a => a.Assuntos)
                .WithMany();
        }

    }
}
