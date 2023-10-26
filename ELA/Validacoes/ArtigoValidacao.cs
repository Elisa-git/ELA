using AutoMapper;
using ELA.Models;
using ELA.Models.Config;
using ELA.Models.Requests;
using ELA.Validacoes.Interface;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;

namespace ELA.Validacoes
{
    public class ArtigoValidacao : IArtigoValidacao
    {
        private readonly MorusContext context;
        private readonly IUsuarioValidacao usuarioValidacao;
        private readonly IAssuntoValidacao assuntoValidacao;
        private readonly IMapper mapper;

        public ArtigoValidacao(MorusContext context, IUsuarioValidacao usuarioValidacao, IAssuntoValidacao assuntoValidacao, IMapper mapper)
        {
            this.context = context;
            this.usuarioValidacao = usuarioValidacao;
            this.assuntoValidacao = assuntoValidacao;
            this.mapper = mapper;
        }

        public bool ArtigoExists(int id)
        {
            return (context.Artigos?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public Artigo ValidarArtigo(ArtigoRequest artigoRequest)
        {
            var artigoMapeado = mapper.Map<Artigo>(artigoRequest);
            Artigo artigo = ValidarAssuntos(artigoMapeado, artigoRequest.AssuntoId);

            if (!usuarioValidacao.UsuarioExists(artigo.UsuarioId))
                throw new Exception("Usuário não cadastrado");

            artigo.DataPostagem = DateTime.Now;
            return artigo;
        }

        public Artigo ValidarAtualizacao(ArtigoPutRequest artigoPutRequest)
        {
            if (!ArtigoExists(artigoPutRequest.Id))
                throw new Exception("Id informado não existe");
            if (!usuarioValidacao.UsuarioExists(artigoPutRequest.Id)) 
                throw new Exception("Usuário não cadastrado");

            var retorno = RetornarArtigo(artigoPutRequest.Id);
            Artigo artigo = ValidarAssuntos(retorno, artigoPutRequest.AssuntoId);

            mapper.Map(artigo, retorno);

            return retorno;
        }

        private Artigo ValidarAssuntos(Artigo artigo, List<int> assuntosId)
        {
            List<Assunto> assuntos = new List<Assunto>();

            foreach (var id in assuntosId)
            {
                var assunto = assuntoValidacao.RetornaAssunto(id);

                if (assunto == null)
                    throw new Exception("O assunto" + assunto.Id + "não existe");

                assuntos.Add(assunto);
            }

            artigo.Assuntos = assuntos;
            return artigo;
        }

        public Artigo RetornarArtigo(int id)
        {
            return context.Artigos.Include(a => a.Assuntos).FirstOrDefault(x => x.Id.Equals(id));
        }
    }
}
