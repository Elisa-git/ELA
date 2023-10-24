using ELA.Models;

namespace ELA.Validacoes.Interface
{
    public interface IUsuarioValidacao
    {
        bool UsuarioExists(int id);
        Task PostValidation(Usuario usuario);
        Task Login(Usuario usuario);
    }
}
