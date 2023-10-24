using ELA.Models.Requests;
using ELA.Models;

namespace ELA.Validacoes.Interface
{
    public interface IFiqueAtentoValidacao
    {
        bool FiqueAtentoExists(int id);
        FiqueAtento ValidarFiqueAtento(FiqueAtentoRequest fiqueAtentoRequest);
        FiqueAtento ValidarAtualizacao(FiqueAtentoPutRequest fiqueAtentoPutRequest);
        FiqueAtento RetornarFiqueAtento(int id);
    }
}
