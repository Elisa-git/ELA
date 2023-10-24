using AutoMapper;
using ELA.Models;
using ELA.Models.Config;
using ELA.Models.Requests;
using ELA.Validacoes.Interface;

namespace ELA.Validacoes
{
    public class PerguntaValidacao : IPerguntaValidacao
    {
        private readonly MorusContext context;
        private readonly IUsuarioValidacao usuarioValidacao;
        private readonly IAssuntoValidacao assuntoValidacao;
        private readonly IMapper mapper;


        public PerguntaValidacao(MorusContext context, IUsuarioValidacao usuarioValidacao, IAssuntoValidacao assuntoValidacao, IMapper mapper)
        {
            this.context = context;
            this.usuarioValidacao = usuarioValidacao;
            this.assuntoValidacao = assuntoValidacao;
            this.mapper = mapper;
        }

        public bool PerguntaExists(int id)
        {
            return (context.Perguntas?.Any(p => p.Id == id)).GetValueOrDefault();
        }

        public Pergunta ValidarPergunta(PerguntaRequest perguntaRequest)
        {
            Pergunta pergunta = MapearPergunta(perguntaRequest);

            if (!usuarioValidacao.UsuarioExists(pergunta.UsuarioId))
                throw new Exception("Usuário não cadastrado");

            return pergunta;
        }

        private Pergunta MapearPergunta(PerguntaRequest perguntaRequest)
        {
            var perguntaMapeada = mapper.Map<Pergunta>(perguntaRequest);

            foreach (var id in perguntaRequest.AssuntoId)
            {
                var assunto = assuntoValidacao.RetornaAssunto(id);

                if (assunto == null)
                    throw new Exception("O assunto" + assunto.Id + "não existe");

                List<Assunto> assuntos = new List<Assunto>();
                assuntos.Add(assunto);
                perguntaMapeada.Assuntos = assuntos;
            }

            perguntaMapeada.DataPostagem = DateTime.Now;
            return perguntaMapeada;
        }
    }
}
