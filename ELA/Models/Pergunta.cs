using ELA.Models.Heranca;

namespace ELA.Models
{
    public class Pergunta : Postagem
    {
        public int Id { get; set; }
        public string PerguntaTitulo { get; set; }
        public string Resposta { get; set; }

    }
}
