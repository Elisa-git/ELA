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

        // Relacionamento com a classe Assunto
        public List<Assunto> Assuntos { get; set; }


    }
}