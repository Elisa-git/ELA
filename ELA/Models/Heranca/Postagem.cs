using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ELA.Models.Heranca
{
    public abstract class Postagem
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Resumo { get; set; }
        public DateTime DataPostagem { get; set; }           

        // Relacionamento um -> muitos
        [ForeignKey("Usuario")]
        [Column(Order = 1)]
        public int UsuarioId { get; set; }
        
        [JsonIgnore]
        public Usuario? Usuario { get; set; }

        // Relacionamento muitos -> muitos
        public List<Assunto> Assuntos { get; set; }
    }
}