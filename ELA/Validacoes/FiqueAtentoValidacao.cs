using AutoMapper;
using ELA.Models;
using ELA.Models.Config;
using ELA.Models.Requests;
using ELA.Validacoes.Interface;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

namespace ELA.Validacoes
{
    public class FiqueAtentoValidacao : IFiqueAtentoValidacao
    {
        private readonly MorusContext context;
        private readonly IUsuarioValidacao usuarioValidacao;
        private readonly IAssuntoValidacao assuntoValidacao;
        private readonly IMapper mapper;

        public FiqueAtentoValidacao(MorusContext context, IUsuarioValidacao usuarioValidacao, IAssuntoValidacao assuntoValidacao, IMapper mapper)
        {
            this.context = context;
            this.usuarioValidacao = usuarioValidacao;
            this.assuntoValidacao = assuntoValidacao;
            this.mapper = mapper;
        }

        public bool FiqueAtentoExists(int id)
        {
            return (context.FiqueAtentos?.Any(f => f.Id.Equals(id))).GetValueOrDefault();
        }

        public FiqueAtento ValidarFiqueAtento(FiqueAtentoRequest fiqueAtentoRequest)
        {
            var fiqueAtentoMapeado = mapper.Map<FiqueAtento>(fiqueAtentoRequest);
            FiqueAtento fiqueAtento = ValidarAssuntos(fiqueAtentoMapeado, fiqueAtentoRequest.AssuntoId);

            if (!usuarioValidacao.UsuarioExists(fiqueAtento.UsuarioId))
                throw new Exception("Usuário não cadastrado");

            fiqueAtento.DataPostagem = DateTime.Now;
            return fiqueAtento;
        }

        public FiqueAtento ValidarAtualizacao(FiqueAtentoPutRequest fiqueAtentoPutRequest)
        {         
            if (!FiqueAtentoExists(fiqueAtentoPutRequest.Id))
                throw new Exception("Id informado não existe");

            if (!usuarioValidacao.UsuarioExists(fiqueAtentoPutRequest.UsuarioId))
                throw new Exception("Usuário não cadastrado");

            var retorno = RetornarFiqueAtento(fiqueAtentoPutRequest.Id);
            FiqueAtento fiqueAtento = ValidarAssuntos(retorno, fiqueAtentoPutRequest.AssuntoId);

            mapper.Map(fiqueAtento, retorno);

            return retorno;
        }

        private FiqueAtento ValidarAssuntos(FiqueAtento fiqueAtento, List<int> assuntosId)
        {
            List<Assunto> assuntos = new List<Assunto>();

            foreach (var id in assuntosId)
            {
                var assunto = assuntoValidacao.RetornaAssunto(id);

                if (assunto == null)
                    throw new Exception("O assunto" + assunto.Id + "não existe");

                assuntos.Add(assunto);
            }

            fiqueAtento.Assuntos = assuntos;
            return fiqueAtento;
        }

        public FiqueAtento RetornarFiqueAtento(int id)
        {
            return context.FiqueAtentos.Include(a => a.Assuntos).FirstOrDefault();
        }

    }
}
