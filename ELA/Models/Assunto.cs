using ELA.Models.Heranca;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ELA.Models
{
    public class Assunto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

    }
}
