using ELA.Models;
using ELA.Models.Requests;

namespace ELA.Validacoes.Interface
{
    public interface IArtigoValidacao
    {
        bool ArtigoExists(int id);
        IEnumerable<Artigo> GetArtigos(ArtigoGetRequest artigoGetRequest);
        Artigo ValidarArtigo(ArtigoRequest artigoRequest, IFormFile file);
        Artigo ValidarAtualizacao(ArtigoPutRequest artigoPutRequest);
        Artigo RetornarArtigo(int id);
    }
}
