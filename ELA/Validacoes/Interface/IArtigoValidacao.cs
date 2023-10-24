using ELA.Models;
using ELA.Models.Requests;

namespace ELA.Validacoes.Interface
{
    public interface IArtigoValidacao
    {
        Artigo ValidarArtigo(ArtigoRequest artigoRequest);
        bool ArtigoExists(int id);
    }
}
