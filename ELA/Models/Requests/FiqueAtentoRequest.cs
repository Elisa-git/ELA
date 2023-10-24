namespace ELA.Models.Requests
{
    public class FiqueAtentoRequest
    {
        public string Titulo { get; set; }
        public string Resumo { get; set; }
        public string Texto { get; set; }
        public int UsuarioId { get; set; }
        public List<int> AssuntoIds { get; set; }

    }
}
