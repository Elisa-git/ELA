using ELA.Models.Heranca;
using System.Reflection.Metadata;

namespace ELA.Models
{
    public class Artigo : Postagem
    {
        public string SubTitulo { get; set; }
        public string Texto { get; set; }

    }
}
