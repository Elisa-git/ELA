using ELA.Models;

namespace ELA.Validacoes.Interface
{
    public interface IArtigoValidacao
    {
        Artigo ValidarArtigo(Artigo artigo);
        bool ArtigoExists(int id);
    }
}
