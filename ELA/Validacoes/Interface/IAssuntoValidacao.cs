using ELA.Models;

namespace ELA.Validacoes.Interface
{
    public interface IAssuntoValidacao
    {
        bool AssuntoExists(int id);
        Assunto RetornaAssunto(int id);
    }
}
