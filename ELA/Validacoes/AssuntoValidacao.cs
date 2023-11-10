using ELA.Models;
using ELA.Models.Config;
using ELA.Validacoes.Interface;
using Microsoft.EntityFrameworkCore;

namespace ELA.Validacoes
{
    public class AssuntoValidacao : IAssuntoValidacao
    {
        private readonly ELAContext context;

        public AssuntoValidacao(ELAContext context)
        {
            this.context = context;
        }

        public bool AssuntoExists(int id)
        {
            return (context.Assuntos?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public Assunto RetornaAssunto(int id)
        {
            return (context.Assuntos?.FirstOrDefault(x => x.Id.Equals(id)));
        }
    }
}
