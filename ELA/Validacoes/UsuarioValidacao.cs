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
    }
}
