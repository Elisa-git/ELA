using ELA.Models;
using ELA.Models.Config;
using ELA.Validacoes.Interface;
using Microsoft.EntityFrameworkCore;

namespace ELA.Validacoes
{
    public class UsuarioValidacao : IUsuarioValidacao
    {
        private readonly MorusContext context;

        public UsuarioValidacao(MorusContext context) 
        {
            this.context = context;
        }

        public bool UsuarioExists(int id)
        {
            return (context.Usuarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task PostValidation(Usuario usuario)
        {
            var listaUsuarios = context.Usuarios.ToList();
            string mensagem;

            if (listaUsuarios.Any(x => x.CPF.Equals(usuario.CPF)))
            {
                mensagem = "O CPF utilizado já possui um cadastro";
                throw new Exception(mensagem);
            }
            else if (listaUsuarios.Any(x => x.Email.Equals(usuario.Email)))
            {
                mensagem = "O Email utilizado já possui um cadastro";
                throw new Exception(mensagem);
            }

            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();
        }

        public async Task Login(Usuario usuario)
        {
            var listaUsuarios = context.Usuarios.ToList();
            var validaEmail = listaUsuarios.FirstOrDefault(x => x.Email.Equals(usuario.Email));

            if (usuario.Senha == null || usuario.Email == null)
                throw new Exception("Email e Senha precisam estar preenchidos");
                        
            if (validaEmail == null)
                throw new Exception("Email não cadastrado");

            if (!validaEmail.Senha.Equals(usuario.Senha))
                throw new Exception("Senha não corresponde");
        }
    }
}
