using AutoMapper;
using ELA.Models;
using ELA.Models.Config;
using ELA.Models.Requests;
using ELA.Validacoes.Interface;
using Microsoft.EntityFrameworkCore;

namespace ELA.Validacoes
{
    public class PerguntaValidacao : IPerguntaValidacao
    {
        private readonly ELAContext context;
        private readonly IUsuarioValidacao usuarioValidacao;
        private readonly IAssuntoValidacao assuntoValidacao;
        private readonly IMapper mapper;


        public PerguntaValidacao(ELAContext context, IUsuarioValidacao usuarioValidacao, IAssuntoValidacao assuntoValidacao, IMapper mapper)
        {
            this.context = context;
            this.usuarioValidacao = usuarioValidacao;
            this.assuntoValidacao = assuntoValidacao;
            this.mapper = mapper;
        }

        public bool PerguntaExists(int id)
        {
            return (context.Perguntas?.Include(p => p.Assuntos).Any(p => p.Id == id)).GetValueOrDefault();
        }

        public Pergunta ValidarPergunta(PerguntaRequest perguntaRequest)
        {
            var perguntaMapeada = mapper.Map<Pergunta>(perguntaRequest);
            Pergunta pergunta = ValidarAssuntos(perguntaMapeada, perguntaRequest.AssuntoId);

            if (!usuarioValidacao.UsuarioExists(pergunta.UsuarioId))
                throw new Exception("Usuário não cadastrado");

            pergunta.DataPostagem = DateTime.Now;
            return pergunta;
        }


        public Pergunta ValidarAtualizacao(PerguntaPutRequest perguntaPutRequest)
        {
            var objExistente = RetornarPergunta(perguntaPutRequest.Id);

            if (objExistente == null)
                throw new Exception("Id informado não existe");

            if (!usuarioValidacao.UsuarioExists(perguntaPutRequest.UsuarioId))
                throw new Exception("Usuário não cadastrado");

            context.Entry(objExistente).CurrentValues.SetValues(perguntaPutRequest);
            Pergunta pergunta = ValidarAssuntos(objExistente, perguntaPutRequest.AssuntoId);

            return pergunta;
        }

        private Pergunta ValidarAssuntos(Pergunta pergunta, List<int> assuntosId)
        {
            List<Assunto> assuntos = new List<Assunto>();

            foreach (var id in assuntosId)
            {
                var assunto = assuntoValidacao.RetornaAssunto(id);

                if (assunto == null)
                    throw new Exception("O assunto" + assunto.Id + "não existe");

                assuntos.Add(assunto);
            }

            pergunta.Assuntos = assuntos;
            return pergunta;
        }

        public Pergunta RetornarPergunta(int id)
        {
            return context.Perguntas.Include(a => a.Assuntos).FirstOrDefault(x => x.Id.Equals(id));
        }

    }
}
