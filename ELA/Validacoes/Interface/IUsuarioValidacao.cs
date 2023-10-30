using ELA.Models;
using ELA.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace ELA.Validacoes.Interface
{
    public interface IUsuarioValidacao
    {
        bool UsuarioExists(int id);
        void UsuarioValidation(Usuario usuario);
        Usuario PostUsuario(UsuarioRequest usuarioRequest);
        Task Login(LoginRequest loginRequest);
        Usuario RetornarUsuario(int id);
    }
}
