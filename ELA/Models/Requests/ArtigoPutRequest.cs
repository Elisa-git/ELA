namespace ELA.Models.Requests
{
    public class ArtigoPutRequest
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string SubTitulo { get; set; }
        public string Resumo { get; set; }
        public string Texto { get; set; }
        public IFormFile? Imagem { get; set; }
        public int UsuarioId { get; set; }
        public List<int> AssuntoId { get; set; }

    }
}
