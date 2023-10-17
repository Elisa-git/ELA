using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELA.Models.Heranca
{
    public class Postagem
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Resumo { get; set; }
        public DateTime DataPostagem { get; set; }

        // Relacionamento muitos -> muitos
        // public List<Assunto> Assuntos { get; set; }                

        // Relacionamento um -> muitos
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        // Relacionamento muitos -> muitos
        public List<Assunto> Assuntos { get; } = new();
    }
}