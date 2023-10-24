namespace ELA.Models.Requests
{
    public class FiqueAtentoPutRequest
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Resumo { get; set; }
        public string Texto { get; set; }
        public DateTime DataPostagem { get; set; }
        public int UsuarioId { get; set; }
        public List<int> AssuntoId { get; set; }
    }
}
