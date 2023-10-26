using ELA.Models;
using ELA.Models.Requests;

namespace ELA.Validacoes.Interface
{
    public interface IArtigoValidacao
    {
        bool ArtigoExists(int id);
        Artigo ValidarArtigo(ArtigoRequest artigoRequest);
        Artigo ValidarAtualizacao(ArtigoPutRequest artigoPutRequest);
        Artigo RetornarArtigo(int id);
    }
}
