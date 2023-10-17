using ELA.Models;
using ELA.Models.Config;
using ELA.Validacoes.Interface;

namespace ELA.Validacoes
{
    public class PerguntaValidacao : IPerguntaValidacao
    {
        private readonly MorusContext context;
        private readonly IUsuarioValidacao usuarioValidacao;

        public PerguntaValidacao(MorusContext context, IUsuarioValidacao usuarioValidacao)
        {
            this.context = context;
            this.usuarioValidacao = usuarioValidacao;
        }

        public bool PerguntaExists(int id)
        {
            return (context.Perguntas?.Any(p => p.Id == id)).GetValueOrDefault();
        }

        public Pergunta ValidarPergunta(Pergunta pergunta)
        {
            if (!usuarioValidacao.UsuarioExists(pergunta.UsuarioId))
                throw new ArgumentException();

            return pergunta;
        }
    }
}
