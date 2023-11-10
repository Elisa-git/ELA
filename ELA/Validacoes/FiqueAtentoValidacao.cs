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
        private readonly ELAContext context;
        private readonly IUsuarioValidacao usuarioValidacao;
        private readonly IAssuntoValidacao assuntoValidacao;
        private readonly IMapper mapper;

        public FiqueAtentoValidacao(ELAContext context, IUsuarioValidacao usuarioValidacao, IAssuntoValidacao assuntoValidacao, IMapper mapper)
        {
            this.context = context;
            this.usuarioValidacao = usuarioValidacao;
            this.assuntoValidacao = assuntoValidacao;
            this.mapper = mapper;
        }

        public bool FiqueAtentoExists(int id)
        {
            return (context.FiqueAtentos.Include(p => p.Assuntos).Any(f => f.Id.Equals(id)));
        }

        public FiqueAtento ValidarFiqueAtento(FiqueAtentoRequest fiqueAtentoRequest)
        {
            var fiqueAtentoMapeado = mapper.Map<FiqueAtento>(fiqueAtentoRequest);
            FiqueAtento fiqueAtento = ValidarAssuntos(fiqueAtentoMapeado, fiqueAtentoRequest.AssuntosId);

            if (!usuarioValidacao.UsuarioExists(fiqueAtento.UsuarioId))
                throw new Exception("Usuário não cadastrado");

            fiqueAtento.DataPostagem = DateTime.Now;
            return fiqueAtento;
        }

        public FiqueAtento ValidarAtualizacao(FiqueAtentoPutRequest fiqueAtentoPutRequest)
        {
            var objExistente = RetornarFiqueAtento(fiqueAtentoPutRequest.Id);

            if (objExistente == null)
                throw new Exception("Objeto informado não existe");

            if (!usuarioValidacao.UsuarioExists(fiqueAtentoPutRequest.UsuarioId))
                throw new Exception("Usuário não cadastrado");

            context.Entry(objExistente).CurrentValues.SetValues(fiqueAtentoPutRequest);
            FiqueAtento fiqueAtento = ValidarAssuntos(objExistente, fiqueAtentoPutRequest.AssuntosId);

            return fiqueAtento;
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
            return context.FiqueAtentos.Include(a => a.Assuntos).FirstOrDefault(x => x.Id.Equals(id));
        }

    }
}
