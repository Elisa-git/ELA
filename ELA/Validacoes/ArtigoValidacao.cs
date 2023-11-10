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
        private readonly ELAContext context;
        private readonly IUsuarioValidacao usuarioValidacao;
        private readonly IAssuntoValidacao assuntoValidacao;
        private readonly IMapper mapper;

        public ArtigoValidacao(ELAContext context, IUsuarioValidacao usuarioValidacao, IAssuntoValidacao assuntoValidacao, IMapper mapper)
        {
            this.context = context;
            this.usuarioValidacao = usuarioValidacao;
            this.assuntoValidacao = assuntoValidacao;
            this.mapper = mapper;
        }

        public bool ArtigoExists(int id)
        {
            return (context.Artigos?.Include(p => p.Assuntos).Any(e => e.Id == id)).GetValueOrDefault();
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
            var objExistente = RetornarArtigo(artigoPutRequest.Id);

            if (objExistente == null)
                throw new Exception("Id informado não existe");
            if (!usuarioValidacao.UsuarioExists(artigoPutRequest.Id)) 
                throw new Exception("Usuário não cadastrado");

            context.Entry(objExistente).CurrentValues.SetValues(artigoPutRequest);
            Artigo artigo = ValidarAssuntos(objExistente, artigoPutRequest.AssuntoId);

            return artigo;
        }

        //public IFormFileCollection UploadImage(IFormFileCollection arquivos)
        //{
        //     foreach (IFormFile file in arquivos)
        //    {

        //}
        //}

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
