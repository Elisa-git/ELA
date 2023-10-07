using ELA.Models.Enums;
using ELA.Models.Heranca;

namespace ELA.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public TipoUsuarioEnum TipoUsuarioEnum { get; set; }
    }
}
