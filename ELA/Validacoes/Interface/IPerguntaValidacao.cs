using ELA.Models;
using ELA.Models.Requests;

namespace ELA.Validacoes.Interface
{
    public interface IPerguntaValidacao
    {
        bool PerguntaExists(int id);
        Pergunta ValidarPergunta(PerguntaRequest perguntaRequest);
        Pergunta ValidarAtualizacao(PerguntaPutRequest perguntaPutRequest);
        Pergunta RetornarPergunta(int id);
    }
}
