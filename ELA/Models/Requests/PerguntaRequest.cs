namespace ELA.Models.Requests
{
    public class PerguntaRequest
    {
        public string Titulo { get; set; }
        public string Resumo { get; set; }
        public string Resposta { get; set; }
        public int UsuarioId { get; set; }
        public List<int> AssuntoIds { get; set; }
    }
}
