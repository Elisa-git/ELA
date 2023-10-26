namespace ELA.Models.Requests
{
    public class PerguntaPutRequest
    {
        public int Id { get; set; }
        public string Resposta { get; set; }
        public string Titulo { get; set; }
        public string Resumo { get; set; }
        public List<int> AssuntoId { get; set; }
        public int UsuarioId { get; set; }

    }
}
