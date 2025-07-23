namespace SistemaDeControleDeFrequencia.DTOs.Frequencia
{
    public class FrequenciaFiltroPaginadoDTO
    {
        public string? MesReferencia { get; set; }
        public int? SetorId { get; set; }
        public int? StatusFrequenciaId { get; set; }

        public int Pagina { get; set; } = 1;
        public int TamanhoPagina { get; set; } = 10;
    }
}
