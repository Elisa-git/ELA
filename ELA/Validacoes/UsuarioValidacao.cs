using AutoMapper;
using ELA.Models;
using ELA.Models.Config;
using ELA.Models.Requests;
using ELA.Validacoes.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ELA.Validacoes
{
    public class UsuarioValidacao : IUsuarioValidacao
    {
        private readonly MorusContext context;
        private readonly IMapper mapper;

        public UsuarioValidacao(MorusContext context, IMapper mapper) 
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool UsuarioExists(int id)
        {
            return (context.Usuarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public Usuario PostUsuario(UsuarioRequest usuarioRequest)
        {
            var usuario = mapper.Map<Usuario>(usuarioRequest);

            var newData = new DateTime();
            newData = usuario.DataNascimento.Date;
            usuario.DataNascimento = newData;

            UsuarioValidation(usuario);

            return usuario;
        }

        public void UsuarioValidation(Usuario usuario)
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
        }

        public async Task Login(LoginRequest loginRequest)
        {
            var listaUsuarios = context.Usuarios.ToList();
            var validaEmail = listaUsuarios.FirstOrDefault(x => x.Email.Equals(loginRequest.Email));

            if (loginRequest.Senha == null || loginRequest.Email == null)
                throw new Exception("Email e Senha precisam estar preenchidos");
                        
            if (validaEmail == null)
                throw new Exception("Email não cadastrado");

            if (!validaEmail.Senha.Equals(loginRequest.Senha))
                throw new Exception("Senha não corresponde");
        }

        public Usuario RetornarUsuario(int id)
        {
            return context.Usuarios.FirstOrDefault(x => x.Id.Equals(id));
        }
    }
}
