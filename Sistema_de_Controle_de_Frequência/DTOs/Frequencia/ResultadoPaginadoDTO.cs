namespace SistemaDeControleDeFrequencia.DTOs.Frequencia
{
    public class ResultadoPaginadoDTO<T>
    {
        public List<T> Dados { get; set; }
        public int TotalPaginas { get; set; }
        public int PaginaAtual { get; set; }
        public int TotalRegistros { get; set; }
    }
}
