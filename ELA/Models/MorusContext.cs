using Microsoft.EntityFrameworkCore;

namespace ELA.Models
{
    public class MorusContext : DbContext
    {
        public MorusContext(DbContextOptions<MorusContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; } = null!;
    }
}
