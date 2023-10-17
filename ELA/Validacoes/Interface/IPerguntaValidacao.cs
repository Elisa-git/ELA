using ELA.Models;

namespace ELA.Validacoes.Interface
{
    public interface IPerguntaValidacao
    {
        bool PerguntaExists(int id);
        Pergunta ValidarPergunta(Pergunta pergunta);
    }
}
