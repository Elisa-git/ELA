using AutoMapper;
using ELA.Models;
using ELA.Models.Config;
using ELA.Models.Requests;
using ELA.Validacoes.Interface;

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
            FiqueAtento fiqueAtento = MapearFiqueAtento(fiqueAtentoRequest);

            if (!usuarioValidacao.UsuarioExists(fiqueAtento.UsuarioId))
                throw new Exception("Usuário não cadastrado");

            return fiqueAtento;
        }

        private FiqueAtento MapearFiqueAtento(FiqueAtentoRequest fiqueAtentoRequest)
        {
            var fiqueAtentoMapeado = mapper.Map<FiqueAtento>(fiqueAtentoRequest);

            foreach (var id in fiqueAtentoRequest.AssuntoIds)
            {
                var assunto = assuntoValidacao.RetornaAssunto(id);

                if (assunto == null)
                    throw new Exception("O assunto" + assunto.Id + "não existe");

                List<Assunto> assuntos = new List<Assunto>();
                assuntos.Add(assunto);
                fiqueAtentoMapeado.Assuntos = assuntos;
            }

            fiqueAtentoMapeado.DataPostagem = DateTime.Now;
            return fiqueAtentoMapeado;
        }

        public void ValidarAtualizacao(FiqueAtento fiqueAtento)
        {
            if (fiqueAtento.Id == null)
                throw new Exception("Id não informado");
            
            if (!FiqueAtentoExists(fiqueAtento.Id))
                throw new Exception("Id informado não existe");

            foreach (var assunto in fiqueAtento.Assuntos)
            {
                if (!assuntoValidacao.AssuntoExists(assunto.Id))
                    throw new Exception("O assunto" + assunto.Id + "não existe");
            }

            if (!usuarioValidacao.UsuarioExists(fiqueAtento.UsuarioId))
                throw new Exception("Usuário não cadastrado");
        }
    }
}
