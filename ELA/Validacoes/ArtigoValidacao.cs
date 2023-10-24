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
            Artigo artigo = this.MapearArtigo(artigoRequest);

            if (!usuarioValidacao.UsuarioExists(artigo.UsuarioId))
                throw new Exception("Usuário não cadastrado");

            return artigo;
        }

        private Artigo MapearArtigo(ArtigoRequest arigoRequest)
        {
            var artigoMapeado = mapper.Map<Artigo>(arigoRequest);

            foreach (var id in arigoRequest.AssuntoId)
            {
                var assunto = assuntoValidacao.RetornaAssunto(id);

                if (assunto == null)
                    throw new Exception("O assunto" + assunto.Id + "não existe");
                
                List<Assunto> assuntos = new List<Assunto>();
                assuntos.Add(assunto);
                artigoMapeado.Assuntos = assuntos;
            }

            artigoMapeado.DataPostagem = DateTime.Now;
            return artigoMapeado;
        }
    }
}
