using ELA.Models.Heranca;

namespace ELA.Models
{
    public class Artigo : Postagem
    {
        public int Id { get; set; }
        public string SubTitulo { get; set; }
        public string Texto { get; set; }

    }
}
