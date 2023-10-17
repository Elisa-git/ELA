using ELA.Models;
using ELA.Models.Config;
using ELA.Validacoes.Interface;
using Microsoft.EntityFrameworkCore;

namespace ELA.Validacoes
{
    public class ArtigoValidacao : IArtigoValidacao
    {
        private readonly MorusContext context;
        private readonly IUsuarioValidacao usuarioValidacao;

        public ArtigoValidacao(MorusContext context, IUsuarioValidacao usuarioValidacao) 
        {
            this.context = context;
            this.usuarioValidacao = usuarioValidacao;
        }

        public bool ArtigoExists(int id)
        {
            return (context.Artigos?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public Artigo ValidarArtigo(Artigo artigo)
        {
            if (!usuarioValidacao.UsuarioExists(artigo.UsuarioId))
                throw new ArgumentException();

            return artigo;
        }
    }
}
